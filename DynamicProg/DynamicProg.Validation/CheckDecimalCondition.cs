using System;

namespace DynamicProg.Validation
{
    public class CheckDecimalCondition : CheckConditionBase
    {
        private readonly decimal _decimalToCheck;

        public CheckDecimalCondition(Func<decimal> decimalToCheck, ErrorsCollectionException errorsCollectionException) : base(errorsCollectionException)
        {
            _decimalToCheck = decimalToCheck();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(decimalToCheck);
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDecimalCondition IsLessOrEqualTo(decimal compareValue)
        {
            if (_decimalToCheck > compareValue)
            {
                HandleException(GetLessOrEqualToInvalidSizeException("value", _decimalToCheck, compareValue));
            }
            return this;
        }
        public CheckDecimalCondition IsLessOrEqualTo<E>(decimal compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_decimalToCheck > compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDecimalCondition IsMoreOrEqualTo(decimal compareValue)
        {
            if (_decimalToCheck < compareValue)
            {
                HandleException(GetMoreOrEqualToInvalidSizeException("value", _decimalToCheck, compareValue));
            }
            return this;
        }
        public CheckDecimalCondition IsMoreOrEqualTo<E>(decimal compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_decimalToCheck < compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDecimalCondition IsMoreThan(decimal compareValue)
        {
            if (_decimalToCheck <= compareValue)
            {
                HandleException(GetMoreThanInvalidSizeException("value", _decimalToCheck, compareValue));
            }
            return this;
        }
        public CheckDecimalCondition IsMoreThan<E>(decimal compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_decimalToCheck <= compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDecimalCondition IsLessThan(decimal compareValue)
        {
            if (_decimalToCheck >= compareValue)
            {
                HandleException(GetLessThanInvalidSizeException("value", _decimalToCheck, compareValue));
            }
            return this;
        }
        public CheckDecimalCondition IsLessThan<E>(decimal compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_decimalToCheck >= compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDecimalCondition IsBetween(decimal lowerBound, decimal upperBound)
        {
            if (_decimalToCheck < lowerBound || _decimalToCheck > upperBound)
            {
                HandleException(GetBetweenInvalidSizeException("value", _decimalToCheck, lowerBound, upperBound));
            }
            return this;
        }
        public CheckDecimalCondition IsBetween<E>(decimal lowerBound, decimal upperBound, object[] exceptionArguments) where E : Exception
        {
            if (_decimalToCheck < lowerBound || _decimalToCheck > upperBound)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDecimalCondition IsEqualTo(decimal compareValue)
        {
            if (_decimalToCheck != compareValue)
            {
                HandleException(GetEqualToInvalidSizeException("value", _decimalToCheck, compareValue));
            }
            return this;
        }
        public CheckDecimalCondition IsEqualTo<E>(decimal compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_decimalToCheck != compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }
    }
}