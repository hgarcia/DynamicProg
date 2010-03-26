using System;
using System.Linq;
using DynamicProg.Validation;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class CheckThat_Object_Test
    {
        private MyClass myClass = new MyClass();
        [Test]
        public void Given_an_object_check_for_all_possible_errors_and_return_a_list_of_Exceptions()
        {
            var list = new Validator().CheckThat(() => myClass)
                .IsNotNull().IsNull()
                .IsInstanceOf(typeof (Exception))
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(2));
        }

        [Test]
        public void Given_an_object_check_for_all_possible_errors_and_return_a_list_of_custom_Exceptions()
        {
            var list = new Validator().CheckThat(() => myClass)
                .IsNotNull<NotImplementedException>(null).IsNull<NotImplementedException>(null)
                .IsInstanceOf<NotImplementedException>(typeof(Exception),null)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(2));
            foreach (var exception in list.ErrorsCollection.First().Value)
            {
                Assert.That(exception,Is.InstanceOfType(typeof(NotImplementedException)));
            }
        }
    }

}