using System;
using Faker;

namespace Plugins
{
    public class DateTimeGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate(Type type)
        {
            if (type != GetGenerationType())
            {
                throw new ArgumentException("Returned types does not match");
            }
            var buf = new byte[8];
            Random.NextBytes(buf);
            var ticks = BitConverter.ToInt64(buf, 0);
            return new DateTime(Math.Abs(ticks % (DateTime.MaxValue.Ticks - DateTime.MinValue.Ticks)) + DateTime.MinValue.Ticks);
        }

        public Type GetGenerationType()
        {
            return typeof(DateTime);
        }
    }
}
