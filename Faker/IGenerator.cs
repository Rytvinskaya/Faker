using System;

namespace Faker
{
    internal interface IGenerator
    {
        object Generate(Type type);
        Type GetGenerationType();
    }
}