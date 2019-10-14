using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    class ListGenerator : IGenerator
    {
        private static readonly Random _random = new Random();

        public object Generate(Type type)
        {
            var listType = typeof(List<>);
            var listGenericType = listType.MakeGenericType(type);
            var list = Activator.CreateInstance(listGenericType) as IList;
            var size = _random.Next(1, 30);
            var faker = new Faker();
            for (int i = 0; i < size; i++)
            {
                var item = faker.Create(type);
                list.Add(item);
            }
            return list;
        }

        public Type GetGenerationType()
        {
            return typeof(List<>);
        }
    }
}
