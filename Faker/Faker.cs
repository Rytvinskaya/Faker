using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public class Faker : IFaker
    {
        public Faker()
        {
            _generators = new Dictionary<Type, IGenerator>();
            var currentAssembly = Assembly.GetExecutingAssembly();
            LoadTypesFromAssembly(currentAssembly);
        }

        public Faker(string path) : this()
        {
            if (Directory.Exists(path))
            {
                _pluginPath = path;
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            LoadTypesFromDirectory();
        }
        private string _pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

        private void LoadTypesFromDirectory()
        {
            var pluginDirectory = new DirectoryInfo(_pluginPath);
            var pluginFiles = Directory.GetFiles(_pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                var assembly = Assembly.LoadFrom(file);
                LoadTypesFromAssembly(assembly);
            }
        }
        private void LoadTypesFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => typeof(IGenerator).IsAssignableFrom(type));
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                var generator = Activator.CreateInstance(type) as IGenerator;
                _generators.Add(generator.GetGenerationType(), generator);
            }
        }

        private static readonly List<Type> _exludedTypes = new List<Type>();
        private readonly Dictionary<Type, IGenerator> _generators;

        internal object Create(Type type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsPointer)
            {
                return null;
            }         
            var method = typeof(Faker).GetMethod("Create");
            if (method == null) return null;
            var genericMethod = method.MakeGenericMethod(type);
            return genericMethod.Invoke(this, null);
        }

        public object Create<T>()
        {
            var type = typeof(T);
            if (_exludedTypes.Contains(type))
            {
                return null;
            }
            _exludedTypes.Add(type);
            if (type.IsAbstract || type.IsInterface || type.IsPointer)
            {
                return null;
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var listGenerator = new ListGenerator();
                _exludedTypes.Remove(type);
                return (T) listGenerator.Generate(type.GetGenericArguments()[0]);
            }
            if (_generators.TryGetValue(type, out var generator))
            {
                _exludedTypes.Remove(type);
                return (T) generator.Generate(type);
            }
            if (type.IsClass && !type.IsGenericType)
            {
                var instance = InitializeWithConstructor(type);
                if (instance != null)
                {
                    FillObject(instance);
                }
                _exludedTypes.Remove(type);
                return instance;
            }
            if (type.IsValueType && !type.IsGenericType)
            {
                var instance = default(T);
                if (instance != null)
                {
                    FillObject(instance);
                    _exludedTypes.Remove(type);
                    return instance;
                }
            }
            _exludedTypes.Remove(type);
            return default(T);
        }

        private void FillObject(object instance)
        {
            var type = instance.GetType();
            var members = type.GetMembers();
            foreach (var member in members)
            {
                if (member.MemberType == MemberTypes.Property)
                {
                    var property = member as PropertyInfo;
                    if (!property.CanWrite) continue;
                    var propertyType = property.PropertyType;
                    var value = Create(propertyType);
                    property.SetValue(instance, value);
                }
                else if (member.MemberType == MemberTypes.Field)
                {
                    var field = member as FieldInfo;
                    if (field.IsLiteral) continue;
                    var fieldType = field.FieldType;
                    var value = Create(fieldType);
                    field.SetValue(instance, value);
                }
            }
        }

        private static ConstructorInfo GetValidConstructor(ConstructorInfo[] constructors)
        {
            if (constructors == null || constructors.Length <= 0) return null;

            var constructorInfo = constructors[0];
            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Length > constructorInfo.GetParameters().Length)
                {
                    constructorInfo = constructor;
                }
            }

            return constructorInfo;
        }

        private object InitializeWithConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            var constructor = GetValidConstructor(constructors);
            if (constructor == null)
            {
                return null;
            }
            var values = new List<object>();
            var parameters = constructor.GetParameters();
            foreach (var parameter in parameters)
            {
                var parameterType = parameter.ParameterType;
                var value = Create(parameterType);
                values.Add(value);
            }
            object instance;
            try
            {
                instance = constructor.Invoke(values.ToArray());
            }
            catch (Exception)
            {
                instance = default;
            }
            return instance;
        }
    }
}
