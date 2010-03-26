using System;
using System.Linq;
using DynamicProg.Validation;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class CheckThat_String_Test
    {
        private string _stringToTest = "My string to test";

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void If_i_cant_convert_a_string_to_a_numeric_value_throw_an_exception()
        {
            new Validator().CheckThat(() => _stringToTest).CanConvertToInt32().ThrowFirst();
        }


        [Test]
        public void If_i_can_convert_a_string_to_a_numeric_value_dont_throw_an_exception()
        {
            new Validator().CheckThat(()=>"23").CanConvertToInt32().ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void If_i_cant_convert_a_string_to_boolean_an_exception_is_thrown()
        {
            new Validator().CheckThat(() => _stringToTest).CanConvertToBool().ThrowFirst();
        }
        [Test]
        public void If_i_can_convert_a_false_string_to_boolean_no_exception_is_thrown()
        {
            new Validator().CheckThat(() => "false").CanConvertToBool().ThrowFirst();
        }
        [Test]
        public void If_i_can_convert_a_true_string_to_boolean_no_exception_is_thrown()
        {
            new Validator().CheckThat(()=> "true").CanConvertToBool().Throw();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void If_string_is_not_one_of_a_series_of_options_throws_a_custom_exception()
        {
            var options = new[] { "0056", "0256", "0512", "0752", "1100" };
            new Validator().CheckThat(() => _stringToTest).IsOneOf<ArgumentException>(options, null)
                .ThrowFirst();
        }

        [Test]
        public void If_string_is_one_of_a_series_of_options_no_exception_is_thrown()
        {
            var options = new[] { "0056", "0256", "0512", "0752", "1100" };
            new Validator().CheckThat(() => "1100").IsOneOf(options)
                .ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(MatchNotFoundException))]
        public void If_string_is_not_one_of_a_series_of_options_throws_exception()
        {
            var options = new[] { "0056", "0256", "0512", "0752", "1100" };
            new Validator().CheckThat(() => _stringToTest).IsOneOf(options)
                .ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Check_that_length_is_between_two_limits_throws_a_custom_exception()
        {
            new Validator().CheckThat(() => _stringToTest)
                .LengthIsBetween<ArgumentOutOfRangeException>(20, 100, null)
                .ThrowFirst();         

        }

        [Test]
        [ExpectedException(typeof (InvalidSizeException))]
        public void Check_that_a_string_length_is_between_two_limits_lower_bound_error()
        {
            new Validator().CheckThat(() => _stringToTest).LengthIsBetween(20, 100).ThrowFirst();         
        }

        [Test]
        [ExpectedException(typeof(InvalidSizeException))]
        public void Check_that_a_string_length_is_between_two_limits_upper_bound_error()
        {
            new Validator().CheckThat(() => _stringToTest).LengthIsBetween(4, 10).ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(InvalidFormatException))]
        public void Check_that_a_string_is_an_email()
        {
            new Validator().CheckThat(() => _stringToTest).IsEmail().ThrowFirst();
        }
        [Test]
        [ExpectedException(typeof (InvalidFormatException))]
        public void Check_that_a_string_is_an_url()
        {
            new Validator().CheckThat(() => _stringToTest).IsUrl().ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void I_want_to_throw_an_exception_of_a_given_type_no_matter_how_many_exceptions_occur()
        {
            new Validator().CheckThat(()=>_stringToTest)
                .HasNoSpaces().LengthLessThan(4).Throw<ArgumentNullException>(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void I_want_to_throw_an_exception_of_a_given_type_when_checking_multiple_variables()
        {
            var secondVariable = 45;
            var validator = new Validator();
            validator.CheckThat(() => _stringToTest).HasNoSpaces().LengthLessThan(4);
            validator.CheckThat(() => secondVariable).IsBetween(56, 100);
            validator.Throw<ArgumentException>(new[]{"Please check the data entered and try again."});
        }

        [Test]
        [ExpectedException(typeof(InvalidFormatException))]
        public void I_want_to_throw_the_first_exception_from_a_collection()
        {
            var secondVariable = 45;
            var validator = new Validator();
            validator.CheckThat(() => _stringToTest).HasNoSpaces().LengthLessThan(4);
            validator.CheckThat(() => secondVariable).IsBetween(56, 100);
            validator.ThrowFirst();
        }

        [Test]
        [ExpectedException(typeof(InvalidSizeException))]
        public void When_checking_for_only_one_thing_I_should_throw_the_exception_of_what_I_am_throwing()
        {
            new Validator().CheckThat(()=>_stringToTest).LengthEqualTo(10).ThrowFirst();
        }
 
        [Test]
        public void Do_all_possible_checks_and_return_a_list()
        {
            var list = new Validator().CheckThat(() => _stringToTest)
                .IsNotNull()
                .IsNull()
                .IsNotEmpty()
                .IsEmpty()
                .IsNullOrEmpty()
                .IsNotNullOrEmpty()
                .LengthEqualTo(5)
                .LengthMoreThan(50)
                .LengthLessThan(5)
                .LengthMoreOrEqualTo(50)
                .LengthLessOrEqualTo(10)
                .Match("/abc/")
                .HasNoSpaces()
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(10));

            list = new Validator().CheckThat(() => _stringToTest)
                .IsNullOrEmpty()
                .LengthLessOrEqualTo(10)
                .HasNoSpaces()
                .List();

            foreach (var exception in list.ErrorsCollection.First().Value)
            {
                Console.WriteLine("Exception type: " + exception.GetType() + " - Message: " + exception.Message );
            }
        }

        [Test]
        public void Do_all_possible_checks_and_return_a_list_of_custom_Exceptions()
        {
            var list = new Validator().CheckThat(() => _stringToTest)
                .IsNotNull<ArgumentException>(null)
                .IsNull<ArgumentException>(null)
                .IsNotEmpty<ArgumentException>(null)
                .IsEmpty<ArgumentException>(null)
                .IsNullOrEmpty<ArgumentException>(null)
                .IsNotNullOrEmpty<ArgumentException>(null)
                .LengthEqualTo<ArgumentException>(5, null)
                .LengthMoreThan<ArgumentException>(50, null)
                .LengthLessThan<ArgumentException>(5, null)
                .LengthMoreOrEqualTo<ArgumentException>(50, null)
                .LengthLessOrEqualTo<ArgumentException>(10, null)
                .Match<ArgumentException>("/abc/", null)
                .HasNoSpaces<ArgumentException>(null)
                .List();

            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(10));
            foreach (var exception in list.ErrorsCollection.First().Value)
            {
                Assert.IsInstanceOfType(typeof(ArgumentException),exception);
            }
        }

        [Test]
        public void Check_Null_and_empty_strings()
        {
            string empty = null;
            var list = new Validator().CheckThat(() => empty)
                .IsNotNull()
                .IsNotEmpty()
                .IsNotNullOrEmpty()
                .List();
            Assert.That(list.ErrorsCollection.First().Value.Count, Is.EqualTo(3));
        }
    }

}