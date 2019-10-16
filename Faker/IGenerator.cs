using System;

namespace Faker
{
    public interface IGenerator
    {
        object Generate(Type type);
        Type GetGenerationType();
    }
}