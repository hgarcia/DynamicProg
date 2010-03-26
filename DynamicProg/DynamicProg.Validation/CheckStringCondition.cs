using System;
using System.Text.RegularExpressions;

namespace DynamicProg.Validation
{
    public class CheckStringCondition : CheckConditionBase
    {
        private string _stringToValidate;

        public CheckStringCondition(Func<string> stringToValidate, ErrorsCollectionException errorsCollectionException) : base(errorsCollectionException)
        {
            _stringToValidate = stringToValidate();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(stringToValidate);
        }

        private void madeNotNull()
        {
            if (_stringToValidate == null) _stringToValidate = string.Empty;
        }

        /// <exception cref="NullReferenceException"></exception>
        public CheckStringCondition IsNotNull()
        {
            IsNotNull(_stringToValidate);
            madeNotNull();
            return this;
        }

        /// <exception cref="NotNullException"></exception>
        public CheckStringCondition IsNull()
        {
            IsNull(_stringToValidate);
            madeNotNull();
            return this;
        }

        public CheckStringCondition IsNotNull<E>(object[] exceptionArguments) where E : Exception
        {
            IsNotNull<E>(_stringToValidate, exceptionArguments);
            madeNotNull();
            return this;
        }
        public CheckStringCondition IsNull<E>(object[] exceptionArguments) where E : Exception
        {
            IsNull<E>(_stringToValidate, exceptionArguments);
            madeNotNull();
            return this;
        }

        /// <exception cref="NotEmptyStringException"></exception>
        public CheckStringCondition IsEmpty()
        {
            if (_stringToValidate.Length > 0)
            {
                HandleException(new NotEmptyStringException(_variableProperties.Name));
            }
            return this;
        }
        public CheckStringCondition IsEmpty<E>(object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Length > 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="EmptyStringException"></exception>
        public CheckStringCondition IsNotEmpty()
        {
            if (_stringToValidate.Length == 0)
            {
                HandleException(new EmptyStringException(_variableProperties.Name));
            }
            return this;
        }
        public CheckStringCondition IsNotEmpty<E>(object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Length == 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="NotNullOrEmptyStringException"></exception>
        public CheckStringCondition IsNullOrEmpty()
        {
            if (!String.IsNullOrEmpty(_stringToValidate))
            {
                HandleException(new NotNullOrEmptyStringException(_variableProperties.Name));
            }
            return this;
        }
        public CheckStringCondition IsNullOrEmpty<E>(object[] exceptionArguments) where E : Exception
        {
            if (!String.IsNullOrEmpty(_stringToValidate))
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="NullOrEmptyStringException"></exception>
        public CheckStringCondition IsNotNullOrEmpty()
        {
            if (String.IsNullOrEmpty(_stringToValidate))
            {
                HandleException(new NullOrEmptyStringException(_variableProperties.Name));
            }
            return this;
        }
        public CheckStringCondition IsNotNullOrEmpty<E>(object[] exceptionArguments) where E : Exception
        {
            if (String.IsNullOrEmpty(_stringToValidate))
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckStringCondition LengthLessThan(int size)
        {
            if (_stringToValidate.Length >= size)
            {
                HandleException(GetLessThanInvalidSizeException("length", _stringToValidate.Length, size));
            }
            return this;
        }
        public CheckStringCondition LengthLessThan<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Length >= size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckStringCondition LengthMoreThan(int size)
        {
            if (_stringToValidate.Length <= size)
            {
                HandleException(GetMoreThanInvalidSizeException("length", _stringToValidate.Length, size));
            }
            return this;
        }
        public CheckStringCondition LengthMoreThan<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Length <= size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckStringCondition LengthLessOrEqualTo(int size)
        {
            if (_stringToValidate.Length > size)
            {
                HandleException(GetLessOrEqualToInvalidSizeException("length", _stringToValidate.Length, size));
            }
            return this;
        }
        public CheckStringCondition LengthLessOrEqualTo<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Length > size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckStringCondition LengthMoreOrEqualTo(int size)
        {
            if (_stringToValidate.Length < size)
            {
                HandleException(GetMoreOrEqualToInvalidSizeException("length", _stringToValidate.Length, size));
            }
            return this;
        }
        public CheckStringCondition LengthMoreOrEqualTo<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Length < size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidFormatException"></exception>
        public CheckStringCondition Match(string pattern)
        {
            if(!Regex.Match(_stringToValidate, pattern).Success)
            {
                HandleException(new InvalidFormatException(string.Format("The string {0} should match the pattern {1}",_variableProperties.Name,pattern)));
            }
            return this;
        }
        public CheckStringCondition Match<E>(string pattern, object[] exceptionArguments) where E : Exception
        {
            if (!Regex.Match(_stringToValidate, pattern).Success)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckStringCondition LengthEqualTo(int size)
        {
            if (_stringToValidate.Length != size)
            {
                HandleException(GetEqualToInvalidSizeException("length",_stringToValidate.Length,size));
            }
            return this;           
        }
        public CheckStringCondition LengthEqualTo<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Length != size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidFormatException"></exception>
        public CheckStringCondition HasNoSpaces()
        {
            if (_stringToValidate.Contains(" "))
            {
                HandleException(new InvalidFormatException(string.Format("The string {0} shouldn't have empty characters.", _variableProperties.Name)));
            }
            return this;
        }
        public CheckStringCondition HasNoSpaces<E>(object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Contains(" "))
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidFormatException"></exception>
        public CheckStringCondition IsUrl()
        {
            if (!isURL(_stringToValidate))
            {
                HandleException(new InvalidFormatException(string.Format("The string {0} is not a valid URL.",_variableProperties.Name)));
            }
            return this;
        }
        public CheckStringCondition IsUrl<E>(object[] exceptionArguments) where E : Exception
        {
            if (!isURL(_stringToValidate))
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        private static bool isURL(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return false;
            }
            return Regex.IsMatch(url, RegexPattern.URL);
        }

        private static bool isEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }
            return Regex.IsMatch(email, RegexPattern.EMAIL);
        }

        /// <exception cref="InvalidFormatException"></exception>
        public CheckStringCondition IsEmail()
        {
            if (!isEmail(_stringToValidate))
            {
                HandleException(new InvalidFormatException(string.Format("The string {0} is not a valid Email address.", _variableProperties.Name)));
            }
            return this;
        }
        public CheckStringCondition IsEmail<E>(object[] exceptionArguments) where E : Exception
        {
            if (!isEmail(_stringToValidate))
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckStringCondition LengthIsBetween(int minimumSize, int maximumSize)
        {
            if (_stringToValidate.Length < minimumSize || _stringToValidate.Length > maximumSize)
            {
                HandleException(GetBetweenInvalidSizeException("Length",_stringToValidate.Length,minimumSize,maximumSize));
            }
            return this;
        }
        public CheckStringCondition LengthIsBetween<E>(int minimumSize, int maximumSize, object[] exceptionArguments) where E : Exception
        {
            if (_stringToValidate.Length < minimumSize || _stringToValidate.Length > maximumSize)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public CheckStringCondition CanConvertToInt32()
        {
            try
            {
                Convert.ToInt32(_stringToValidate);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            return this;
        }
        public CheckStringCondition CanConvertToInt32<E>(object[] exceptionArguments) where E : Exception
        {
            try
            {
                Convert.ToInt32(_stringToValidate);
            }
            catch (Exception)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="MatchNotFoundException"></exception>
        public CheckStringCondition IsOneOf(string[] options)
        {
            var notFound = true;
            foreach (var option in options)
            {
                if (option == _stringToValidate) notFound = false;
            }
            if (notFound)
            {
                HandleException(new MatchNotFoundException(_variableProperties.Name, _stringToValidate, options));
            }
            return this;
        }

        /// <exception cref="MatchNotFoundException"></exception>
        public CheckStringCondition IsOneOf<E>(string[] options, object[] exceptionArguments) where E : Exception
        {
            var notFound = true;
            foreach (var option in options)
            {
                if (option == _stringToValidate) notFound = false;
            }
            if (notFound)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="FormatException"></exception>
        public CheckStringCondition CanConvertToBool()
        {
            try
            {
                Convert.ToBoolean(_stringToValidate);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            return this;
        }
        public CheckStringCondition CanConvertToBool<E>(object[] exceptionArguments) where E : Exception
        {
            try
            {
                Convert.ToBoolean(_stringToValidate);
            }
            catch (Exception)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }
    }
}