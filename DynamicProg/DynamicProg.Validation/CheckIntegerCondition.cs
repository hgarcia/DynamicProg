using System;

namespace DynamicProg.Validation
{
    public class CheckIntegerCondition : CheckConditionBase
    {
        private readonly int _integerToCheck;

        public CheckIntegerCondition(Func<int> integerToCheck, ErrorsCollectionException errors)
            : base(errors)
        {
            _integerToCheck = integerToCheck();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(integerToCheck);
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckIntegerCondition IsLessOrEqualTo(int compareValue)
        {
            if (_integerToCheck > compareValue)
            {
                HandleException(GetLessOrEqualToInvalidSizeException("value", _integerToCheck, compareValue));
            }
            return this;
        }
        public CheckIntegerCondition IsLessOrEqualTo<E>(int compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_integerToCheck > compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckIntegerCondition IsMoreOrEqualTo(int compareValue)
        {
            if (_integerToCheck < compareValue)
            {
                HandleException(GetMoreOrEqualToInvalidSizeException("value", _integerToCheck, compareValue));
            }
            return this;
        }
        public CheckIntegerCondition IsMoreOrEqualTo<E>(int compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_integerToCheck < compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckIntegerCondition IsMoreThan(int compareValue)
        {
            if (_integerToCheck <= compareValue)
            {
                HandleException(GetMoreThanInvalidSizeException("value", _integerToCheck, compareValue));
            }
            return this;
        }
        public CheckIntegerCondition IsMoreThan<E>(int compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_integerToCheck <= compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckIntegerCondition IsLessThan(int compareValue)
        {
            if (_integerToCheck >= compareValue)
            {
                HandleException(GetLessThanInvalidSizeException("value", _integerToCheck, compareValue));
            }
            return this;
        }
        public CheckIntegerCondition IsLessThan<E>(int compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_integerToCheck >= compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckIntegerCondition IsBetween(int lowerBound, int upperBound)
        {
            if (_integerToCheck < lowerBound || _integerToCheck > upperBound)
            {
                HandleException(GetBetweenInvalidSizeException("value", _integerToCheck, lowerBound, upperBound));
            }
            return this;
        }
        public CheckIntegerCondition IsBetween<E>(int lowerBound, int upperBound, object[] exceptionArguments) where E : Exception
        {
            if (_integerToCheck < lowerBound || _integerToCheck > upperBound)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckIntegerCondition IsEqualTo(int compareValue)
        {
            if(_integerToCheck!=compareValue)
            {
                HandleException(GetEqualToInvalidSizeException("value",_integerToCheck,compareValue));
            }
            return this;
        }
        public CheckIntegerCondition IsEqualTo<E>(int compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_integerToCheck != compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }
    }
}