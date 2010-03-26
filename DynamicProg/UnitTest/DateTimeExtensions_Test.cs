using System;
using DynamicProg.Extensions;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class DateTimeExtensions_Test
    {
        [Test]
        public void Get_the_Unix_time_for_a_date()
        {
            var result = new DateTime(1970, 1, 2, 0, 0, 0, 0).ToUnixTime();
            var control = (29 * 60 * 60);

            Assert.AreEqual(control, result);
        }

        [Test]
        public void Gets_the_date_from_a_Unix_time()
        {
            double result = (24 * 60 * 60);
            var control = new DateTime(1970, 1, 2, 0, 0, 0, 0);

            Assert.AreEqual(control, result.FromUnixTime());
        }

        [Test]
        public void Checks_if_a_given_date_is_a_weekday()
        {
            Assert.IsTrue(new DateTime(2008,10,15).IsWeekDay());
        }

        [Test]
        public void Checks_if_a_given_date_is_a_weekend()
        {
            Assert.IsTrue(new DateTime(2008, 10, 11).IsWeekEnd());
        }

        [Test]
        public void Call_Tomorrow_Returns_today_plus_one()
        {
            Assert.That(DateTime.Today.AddDays(1),Is.EqualTo(new DateTime().Tomorrow()));
        }

        [Test]
        public void Call_Yesterday_Returns_today_minus_one()
        {
            Assert.That(DateTime.Today.AddDays(1), Is.EqualTo(new DateTime().Yesterday()));
        }
    }
}