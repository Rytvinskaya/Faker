using System;
using System.Collections.Generic;
using Faker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Faker.Tests
{
    [TestClass]
    public class FakerTests
    {
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
            var str = faker.Create<List<Dictionary<int, int>>>();
            Assert.AreNotEqual(str, default(string));
        }
    }
}
