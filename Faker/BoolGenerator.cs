using System;

namespace Faker
{
    public class BoolGenerator : IGenerator
    {
        public object Generate(Type type)
        {
            if (type != GetGenerationType())
            {
                throw new ArgumentException("Returned types does not match");
            }
            return true;
        }

        public Type GetGenerationType()
        {
            return typeof(bool);
        }
    }
}