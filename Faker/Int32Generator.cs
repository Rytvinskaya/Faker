using System;

namespace Faker
{
    public class Int32Generator : IGenerator
    {
        private static readonly Random Random = new Random();

        public object Generate(Type type)
        {
            if (type != GetGenerationType())
            {
                throw new ArgumentException("Returned types does not match");
            }
            return Random.Next(1, int.MaxValue);
        }

        public Type GetGenerationType()
        {
            return typeof(int);
        }
    }
}