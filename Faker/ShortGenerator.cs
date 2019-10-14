using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    class ShortGenerator : IGenerator
    {
        private static readonly Random _random = new Random();
        public object Generate(Type type)
        {
            if (type != GetGenerationType())
            {
                throw new ArgumentException("Returned types does not match");
            }
            return (short)_random.Next(1, short.MaxValue);
        }

        public Type GetGenerationType()
        {
            return typeof(short);
        }
    }
}
