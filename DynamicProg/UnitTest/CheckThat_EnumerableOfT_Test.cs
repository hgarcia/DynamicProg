using System;
using System.Collections.Generic;
using System.Linq;
using DynamicProg.Validation;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class CheckThat_EnumerableOfT_Test
    {
        private IEnumerable<string> collection = new List<string>
                                                     {
                                                         "first value",
                                                         "second value",
                                                         "third value",
                                                         "fourth value",
                                                         "fifth value"
                                                     };


        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Check_that_an_enumeration_contains_a_list_of_elelemts_custom_exception()
        {
            new Validator().CheckThat(() => collection).Contains <ArgumentOutOfRangeException>(new[]
                                                                     {
                                                                         "first", "second value"
                                                                     }, null).ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Check_that_an_enumeration_contains_a_given_elelemts_with_a_custom_exception()
        {
            new Validator().CheckThat(() => collection).Contains<ArgumentOutOfRangeException>("first",null).ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Check_that_an_enumeration_contains_a_given_elelemts()
        {
            new Validator().CheckThat(() => collection).Contains("first").ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Check_that_an_enumeration_contains_a_list_of_elelemts()   
        {
            new Validator().CheckThat(() => collection).Contains(new[]
                                                                     {
                                                                         "first", "second value"
                                                                     }).ThrowFirst();
        }

        [Test]
        public void Check_all_posibilites_and_return_a_list_or_errors()
        {
            var list = new Validator().CheckThat(() => collection)
                .CountIsLessThan(5)
                .IsNotNull()
                .IsNull()
                .CountIsMoreThan(5)
                .CountIsEqualTo(6)
                .CountIsBetween(3, 4)
                .CountIsMoreOrEqualTo(6)
                .CountIsLessOrEqualTo(4)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(7));
        }

        [Test]
        public void Use_all_possible_checks_and_return_a_list_of_custom_errors()
        {
            var list = new Validator().CheckThat(() => collection)
                .CountIsLessThan<IndexOutOfRangeException>(5, null)
                .IsNotNull<ArgumentNullException>(null)
                .IsNull<InvalidCastException>(null)
                .CountIsMoreThan<IndexOutOfRangeException>(5, null)
                .CountIsEqualTo<IndexOutOfRangeException>(6, null)
                .CountIsBetween<InvalidSizeException>(3, 4, new object[] { "my object", "custom", (decimal)3, "and", (decimal)4 })
                .CountIsMoreOrEqualTo<IndexOutOfRangeException>(6, null)
                .CountIsLessOrEqualTo<IndexOutOfRangeException>(4, null)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(7));
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["collection"][0]);
            Assert.IsInstanceOfType(typeof(InvalidCastException), list.ErrorsCollection["collection"][1]);
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["collection"][2]);
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["collection"][3]);
            Assert.IsInstanceOfType(typeof(InvalidSizeException), list.ErrorsCollection["collection"][4]);
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["collection"][5]);
            Assert.IsInstanceOfType(typeof(IndexOutOfRangeException), list.ErrorsCollection["collection"][6]);

        }
    }

}