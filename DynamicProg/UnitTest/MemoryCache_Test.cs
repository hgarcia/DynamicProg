using System;
using DynamicProg.Caching;
using DynamicProg.Logging;
using NUnit.Framework;
using Rhino.Mocks;

namespace UnitTest
{
    [TestFixture]
    public class MemoryCache_Test
    {
        private ILogger mockLogger;
        [SetUp]
        public void SetUp()
        {
            mockLogger = MockRepository.GenerateMock<ILogger>();
        }

        [Test]
        public void Adding_an_object_to_the_cache_by_key()
        {
            ICache caching = new MemoryCache(mockLogger);

            var mc = new MyClass();
            bool added = caching.Add("firstItem", mc);

            Assert.IsTrue(added);
        }

        [Test]
        public void Gets_the_object_from_the_cache()
        {
            ICache caching = new MemoryCache(mockLogger);

            MyClass mc = caching.Get<MyClass>("firstItem");

            Assert.IsNotNull(mc);
        }

        [Test]
        public void Adding_an_object_with_an_absolute_expiration_date()
        {
            ICache caching = new MemoryCache(mockLogger);

            var mc = new MyClass();

            bool added = caching.AddAndKeepFor("secondItem", mc, new TimeSpan(0, 2, 0));

            Assert.IsTrue(added);
        }

        [Test]
        public void Adding_an_object_with_an_sliding_expiration_date()
        {
            ICache caching = new MemoryCache(mockLogger);

            var mc = new MyClass();

            bool added = caching.AddAndKeepWhileUsed("thirdItem", mc, new TimeSpan(0, 2, 0));

            Assert.IsTrue(added);
        }

        [Test]
        public void Removes_an_object_from_the_cache_by_its_key()
        {
            ICache caching = new MemoryCache(mockLogger);
            bool removed = caching.Remove("firstItem");

            Assert.IsTrue(removed);
        }

        [Test]
        public void Clear_the_cache()
        {
            ICache caching = new MemoryCache(mockLogger);
            bool clear = caching.Clear();
            Assert.IsTrue(clear);
        }
    }
}