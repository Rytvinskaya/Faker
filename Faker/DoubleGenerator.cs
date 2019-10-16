using System;

namespace Faker
{
    public class DoubleGenerator : IGenerator
    {
        private static readonly Random Random = new Random();

        public object Generate(Type type)
        {
            if (type != GetGenerationType())
            {
                throw new ArgumentException("Returned types does not match");
            }
            double result;
            do
            {
                result = Random.NextDouble();
            } while (Math.Abs(Math.Abs(result - 0)) < 0.0000001);
            return Random.NextDouble();
        }

        public Type GetGenerationType()
        {
            return typeof(double);
        }
    }
}