using System;
using System.Text;

namespace Faker
{
    public class StringGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate(Type type)
        {
            if (type != GetGenerationType())
            {
                throw new ArgumentException("Returned types does not match");
            }
            var builder = new StringBuilder();
            var size = Random.Next(1, 100);
            for (var i = 0; i < size; i++)
            {
                var symbol = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(symbol);
            }

            return builder.ToString();
        }

        public Type GetGenerationType()
        {
            return typeof(string);
        }
    }
}