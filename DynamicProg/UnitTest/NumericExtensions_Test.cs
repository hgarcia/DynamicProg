using DynamicProg.Extensions;
using NUnit.Framework;
using LaTrompa.Extensions;

namespace UnitTest
{
    [TestFixture]
    public class NumericExtensions_Test
    {
        [Test]
        public void Returns_percentage_of_an_int()
        {
            int result = 10.PercentOf(100);
            var control = 10;
            Assert.AreEqual(control,result);
        }

        [Test]
        public void Returns_percentage_of_a_decimal()
        {
            decimal result = 10.PercentOf(100.1M);
            decimal control = 10.01m;
            Assert.AreEqual(control, result);
        }

        [Test]
        public void Apply_a_percentage_discount_on_an_int()
        {
            int result = 10.PercentDiscount(100);
            var control = 90;
            Assert.AreEqual(control, result);
        }

        [Test]
        public void Apply_a_percentage_discount_on_a_decimal()
        {
            decimal result = 10.PercentDiscount(100.1M);
            decimal control = 90.09m;
            Assert.AreEqual(control, result);
        }

        [Test]
        public void Apply_a_percentage_surcharge_on_an_int()
        {
            int result = 10.PercentDiscount(100);
            var control = 90;
            Assert.AreEqual(control, result);
        }

        [Test]
        public void Apply_a_percentage_surcharge_on_a_decimal()
        {
            decimal result = 10.PercentDiscount(100.1M);
            decimal control = 90.09m;
            Assert.AreEqual(control, result);
        }


    }

}