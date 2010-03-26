using System;
using DynamicProg.Caching;
using DynamicProg.Logging;
using NUnit.Framework;
using Rhino.Mocks;

namespace UnitTest
{
    [TestFixture]
    [Ignore]
    public class DbCache_Test
    {
        private ILogger mockLogger;
        [SetUp]
        public void SetUp()
        {
            mockLogger = MockRepository.GenerateMock<ILogger>();
        }

        [Test]
        [Ignore]
        public void Adding_an_object_to_the_cache_by_key()
        {
            ICache caching = new DbCache(mockLogger);

            var mc = new MyClass();
            bool added = caching.Add("firstItem", mc);

            Assert.IsTrue(added);
        }

        [Test]
        [Ignore]
        public void Gets_the_object_from_the_cache()
        {
            ICache caching = new DbCache(mockLogger);

            MyClass mc = caching.Get<MyClass>("firstItem");

            Assert.IsNotNull(mc);
        }

        [Test]
        [Ignore]
        public void Adding_an_object_with_an_absolute_expiration_date()
        {
            ICache caching = new DbCache(mockLogger);

            var mc = new MyClass();

            bool added = caching.AddAndKeepFor("secondItem", mc, new TimeSpan(0, 2, 0));

            Assert.IsTrue(added);
        }

        [Test]
        [Ignore]
        public void Adding_an_object_with_an_sliding_expiration_date()
        {
            ICache caching = new DbCache(mockLogger);

            var mc = new MyClass();

            bool added = caching.AddAndKeepWhileUsed("thirdItem", mc, new TimeSpan(0, 2, 0));

            Assert.IsTrue(added);
        }

        [Test]
        [Ignore]
        public void Removes_an_object_from_the_cache_by_its_key()
        {
            ICache caching = new DbCache(mockLogger);
            bool removed = caching.Remove("firstItem");

            Assert.IsTrue(removed);
        }

        [Test]
        [Ignore]
        public void Clear_the_cache()
        {
            ICache caching = new DbCache(mockLogger);
            bool clear = caching.Clear();
            Assert.IsTrue(clear);
        }
    }
}