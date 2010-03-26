using System;

namespace DynamicProg.Validation
{
    public class CheckDoubleCondition : CheckConditionBase
    {
        private readonly double _doubleToCheck;

        public CheckDoubleCondition(Func<double> doubleToCheck, ErrorsCollectionException errorsCollectionException) : base(errorsCollectionException)
        {
            _doubleToCheck = doubleToCheck();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(doubleToCheck);
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDoubleCondition IsLessOrEqualTo(double compareValue)
        {
            if (_doubleToCheck > compareValue)
            {
                HandleException(GetLessOrEqualToInvalidSizeException("value", (decimal)_doubleToCheck, (decimal)compareValue));
            }
            return this;
        }
        public CheckDoubleCondition IsLessOrEqualTo<E>(double compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_doubleToCheck > compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDoubleCondition IsMoreOrEqualTo(double compareValue)
        {
            if (_doubleToCheck < compareValue)
            {
                HandleException(GetMoreOrEqualToInvalidSizeException("value", (decimal)_doubleToCheck, (decimal)compareValue));
            }
            return this;
        }
        public CheckDoubleCondition IsMoreOrEqualTo<E>(double compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_doubleToCheck < compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDoubleCondition IsMoreThan(double compareValue)
        {
            if (_doubleToCheck <= compareValue)
            {
                HandleException(GetMoreThanInvalidSizeException("value", (decimal)_doubleToCheck, (decimal)compareValue));
            }
            return this;
        }
        public CheckDoubleCondition IsMoreThan<E>(double compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_doubleToCheck <= compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDoubleCondition IsLessThan(double compareValue)
        {
            if (_doubleToCheck >= compareValue)
            {
                HandleException(GetLessThanInvalidSizeException("value", (decimal)_doubleToCheck, (decimal) compareValue));
            }
            return this;
        }
        public CheckDoubleCondition IsLessThan<E>(double compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_doubleToCheck >= compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDoubleCondition IsBetween(double lowerBound, double upperBound)
        {
            if (_doubleToCheck < lowerBound || _doubleToCheck > upperBound)
            {
                HandleException(GetBetweenInvalidSizeException("value", (decimal)_doubleToCheck, (decimal)lowerBound, (decimal)upperBound));
            }
            return this;
        }
        public CheckDoubleCondition IsBetween<E>(double lowerBound, double upperBound, object[] exceptionArguments) where E : Exception
        {
            if (_doubleToCheck < lowerBound || _doubleToCheck > upperBound)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckDoubleCondition IsEqualTo(double compareValue)
        {
            if (_doubleToCheck != compareValue)
            {
                HandleException(GetEqualToInvalidSizeException("value", (decimal)_doubleToCheck, (decimal)compareValue));
            }
            return this;
        }
        public CheckDoubleCondition IsEqualTo<E>(double compareValue, object[] exceptionArguments) where E : Exception
        {
            if (_doubleToCheck != compareValue)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }


    }
}