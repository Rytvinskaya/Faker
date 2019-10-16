using System;
using Faker;

namespace Plugins
{
    public class FloatGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate(Type type)
        {
            if (type != GetGenerationType())
            {
                throw new ArgumentException("Returned types does not match");
            }
            var mantissa = (Random.NextDouble() * 2.0) - 1.0;
            var exponent = Math.Pow(2.0, Random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }

        public Type GetGenerationType()
        {
            return typeof(float);
        }
    }
}
