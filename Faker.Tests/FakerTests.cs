using System;
using System.Collections.Generic;
using Faker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Faker.Tests
{
    [TestClass]
    public class FakerTests
    {

        class MyClass
        {
            public Boo Boo;
        }

        class Boo
        {
            public Boo(int prop)
            {
                Prop = prop;
            }
            public int Prop { get; private set; }
            public MyClass MyClass;
        }

        [TestMethod]
        public void Loop()
        {
            var faker = new Faker();
            var MyClass = (MyClass)faker.Create<MyClass>();
            Assert.IsNull(MyClass.Boo.MyClass);
        
        }

        [TestMethod]
        public void ListTest()
        {
            var faker = new Faker();

            var list = faker.Create<List<int>>() as List<int>;

            Assert.IsNotNull(list);
            Assert.AreNotEqual(list, default);
        }

        [TestMethod]
        public void ShortGenerator()
        {
            var faker = new Faker();
            var sh = faker.Create<short>();
            Assert.AreNotEqual(sh, default(short));
        }

        [TestMethod]
        public void Constructor()
        {
            var faker = new Faker();
            var foo = (Foo)faker.Create<Foo>();
            Assert.AreNotEqual(foo.Field, default);
        }

        [TestMethod]
        public void StringGenerator()
        {
            var faker = new Faker();
            var str = faker.Create<string>();
            Assert.AreNotEqual(str, default(string));
        }

        [TestMethod]
        public void Plugin()
        {
            var faker = new Faker(@"C:\Users\Hp Pavilion\Documents\5 сем\CПП\laba2\Faker\PluginsDir");
            var dateTime = faker.Create<DateTime>();
            Assert.AreNotEqual(default(DateTime), dateTime);
        }

        [TestMethod]
        public void DefaultInt()
        {
            var faker = new Faker();
            var int32Value = faker.Create<int>();
            Assert.AreNotEqual(int32Value, default);
        }

        [TestMethod]
        public void DefaultBool()
        {
            var faker = new Faker();
            var boolValue = faker.Create<bool>();
            Assert.AreNotEqual(boolValue, default);
        }

        [TestMethod]
        public void DefaultDouble()
        {
            var faker = new Faker();
            var doubleValue = faker.Create<double>();
            Assert.AreNotEqual(doubleValue, default);
        }

        [TestMethod]
        public void DefaultLong()
        {
            var faker = new Faker();
            var longValue = faker.Create<long>();
            Assert.AreNotEqual(longValue, default);
        }

        [TestMethod]
        public void DefaultString()
        {
            var faker = new Faker();
            var stringValue = faker.Create<string>();
            Assert.AreNotEqual(stringValue, default);
        }

    }
}
