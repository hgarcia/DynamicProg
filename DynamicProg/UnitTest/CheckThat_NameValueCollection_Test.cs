using System;
using System.Collections.Specialized;
using System.Linq;
using DynamicProg.Validation;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class CheckThat_NameValueCollection_Test
    {
        private readonly NameValueCollection _collection = new NameValueCollection()
                                 {
                                     {"first", "element"},
                                     {"second", "element"},
                                     {"third", "element"},
                                     {"fourth", "element"},
                                     {"fifth", "element"}
                                 };
        [Test]
        public void Check_count_less_than_x_elements_and_gets_a_list_of_errors()
        {       
            var list = new Validator().CheckThat(() => _collection).CountIsLessThan(5).List();
            Assert.That(list.ErrorsCollection.Count() > 0);
        }

        [Test]
        [ExpectedException(typeof(ErrorsCollectionException))]
        public void Check_count_less_than_x_elements_and_throws_an_ErrorsCollectionException()
        {
            new Validator().CheckThat(() => _collection).CountIsLessThan(5).Throw();
        }

        [Test]
        public void Check_isnotnull_and_count_less_than_x_elements()
        {
            var list = new Validator().CheckThat(() => _collection)
                .CountIsLessThan(5).IsNotNull().List();

            Assert.That(list.ErrorsCollection.Count() > 0);
        }

        [Test]
        public void Check_isnull_and_count_less_than_x_elements()
        {
            var list = new Validator().CheckThat(() => _collection)
                .CountIsLessThan(5).IsNull().List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(2));
        }

        [Test]
        public void Use_all_possible_checks_and_return_a_list_of_errors()
        {
            var list = new Validator().CheckThat(() => _collection)
                .CountIsLessThan(5).IsNotNull().IsNull()
                .CountIsMoreThan(5).CountIsEqualTo(6)
                .CountIsBetween(3,4).CountIsMoreOrEqualTo(6)
                .CountIsLessOrEqualTo(4).ContainsKeys(new[] { "secondKey", "thirdKey" })
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(9));
        }

        [Test]
        public void Use_all_possible_checks_and_return_a_list_of_custom_errors()
        {
            var list = new Validator().CheckThat(() => _collection)
                .CountIsLessThan<IndexOutOfRangeException>(5,null)
                .IsNotNull <ArgumentNullException>(null)
                .IsNull<InvalidCastException>(null)
                .CountIsMoreThan<IndexOutOfRangeException>(5, null)
                .CountIsEqualTo<IndexOutOfRangeException>(6, null)
                .CountIsBetween<InvalidSizeException>(3, 4, new object[]{"my object", "custom", (decimal)3, "and", (decimal)4 })
                .CountIsMoreOrEqualTo<IndexOutOfRangeException>(6, null)
                .CountIsLessOrEqualTo<IndexOutOfRangeException>(4, null)
                .ContainsKeys<ArgumentException>(new[] { "secondKey", "thirdKey" }, null)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(9));
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["_collection"][0]);
            Assert.IsInstanceOfType(typeof(InvalidCastException), list.ErrorsCollection["_collection"][1]);
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["_collection"][2]);
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["_collection"][3]);
            Assert.IsInstanceOfType(typeof(InvalidSizeException), list.ErrorsCollection["_collection"][4]);
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["_collection"][5]);
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["_collection"][6]);
            Assert.IsInstanceOfType(typeof(ArgumentException), list.ErrorsCollection["_collection"][7]);
            Assert.IsInstanceOfType(typeof(ArgumentException), list.ErrorsCollection["_collection"][8]);
        }
    }
}