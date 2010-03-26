using System;
using System.Linq;
using DynamicProg.Validation;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class CheckThat_DateTime_Test
    {
        private readonly DateTime _dateToCheck = new DateTime(2000,10,20);
        private readonly DateTime _beforeThis = new DateTime(1990,1,1);
        private readonly DateTime _afterThis = new DateTime(2010,1,1);

        [Test]
        public void Do_all_possible_checks_and_get_a_list_of_errors()
        {
            var list = new Validator().CheckThat(() => _dateToCheck)
                .IsBefore(_dateToCheck)
                .IsBefore(_beforeThis)
                .IsAfter(_dateToCheck)
                .IsAfter(_afterThis)
                .IsBeforeOrEqualTo(_beforeThis)
                .IsAfterOrEqualTo(_afterThis)
                .IsBeforeOrEqualTo(_dateToCheck)
                .IsAfterOrEqualTo(_dateToCheck)
                .IsBetween(_afterThis,_beforeThis)
                .IsEqualTo(_beforeThis)
                .IsEqualTo(_dateToCheck)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(8));
        }

        [Test]
        public void Do_all_possible_checks_and_get_a_list_of_custom_Exceptions()
        {
            var list = new Validator().CheckThat(() => _dateToCheck)
                .IsBefore<ArgumentException>(_dateToCheck,null)
                .IsBefore<ArgumentException>(_beforeThis, null)
                .IsAfter<ArgumentException>(_dateToCheck, null)
                .IsAfter<ArgumentException>(_afterThis, null)
                .IsBeforeOrEqualTo<ArgumentException>(_beforeThis, null)
                .IsAfterOrEqualTo<ArgumentException>(_afterThis, null)
                .IsBeforeOrEqualTo<ArgumentException>(_dateToCheck, null)
                .IsAfterOrEqualTo<ArgumentException>(_dateToCheck, null)
                .IsBetween<ArgumentException>(_afterThis, _beforeThis, null)
                .IsEqualTo<ArgumentException>(_beforeThis, null)
                .IsEqualTo<ArgumentException>(_dateToCheck, null)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(8));
            foreach (var exception in list.ErrorsCollection.First().Value)
            {
                Assert.IsInstanceOfType(typeof(ArgumentException),exception);
            }
        }
    }

}