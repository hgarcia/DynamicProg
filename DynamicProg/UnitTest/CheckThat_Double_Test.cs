using System;
using System.Linq;
using DynamicProg.Validation;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class CheckThat_Double_Test
    {
        private double _doubleToCheck = 14;

        [Test]
        public void Check_all_possible_errors_and_get_and_errors_list()
        {
            var list = new Validator().CheckThat(() => _doubleToCheck)
                .IsEqualTo(17)
                .IsEqualTo(_doubleToCheck)
                .IsLessThan(17)
                .IsLessThan(12)
                .IsMoreThan(12)
                .IsMoreThan(17)
                .IsLessOrEqualTo(13)
                .IsLessOrEqualTo(14)
                .IsLessOrEqualTo(17)
                .IsMoreOrEqualTo(13)
                .IsMoreOrEqualTo(14)
                .IsMoreOrEqualTo(17)
                .IsBetween(3, 9)
                .IsBetween(8, 17)
                .IsBetween(15, 18)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(7));
        }
        [Test]
        public void Check_all_possible_errors_and_get_a_list_of_custom_exceptions()
        {
            var list = new Validator().CheckThat(() => _doubleToCheck)
                .IsEqualTo<ArgumentException>(17, null)
                .IsEqualTo<ArgumentException>(_doubleToCheck, null)
                .IsLessThan<ArgumentException>(17, null)
                .IsLessThan<ArgumentException>(12, null)
                .IsMoreThan<ArgumentException>(12, null)
                .IsMoreThan<ArgumentException>(17, null)
                .IsLessOrEqualTo<ArgumentException>(13, null)
                .IsLessOrEqualTo<ArgumentException>(14, null)
                .IsLessOrEqualTo<ArgumentException>(17, null)
                .IsMoreOrEqualTo<ArgumentException>(13, null)
                .IsMoreOrEqualTo<ArgumentException>(14, null)
                .IsMoreOrEqualTo<ArgumentException>(17, null)
                .IsBetween<ArgumentException>(3, 9, null)
                .IsBetween<ArgumentException>(8, 17, null)
                .IsBetween<ArgumentException>(15, 18, null)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(7));
            foreach (var exception in list.ErrorsCollection.First().Value)
            {
                Assert.IsInstanceOfType(typeof(ArgumentException), exception);
            }
        }
    }

}