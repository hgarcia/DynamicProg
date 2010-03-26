using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicProg.Validation
{
    public class CheckEnumerableOfTCondition<T> : CheckConditionBase
    {
        private readonly int _enumerableCount;
        private IEnumerable<T> _enumerableToCheck;

        public CheckEnumerableOfTCondition(Func<IEnumerable<T>> enumerableToCheck,
                                           ErrorsCollectionException errorsCollectionException)
            : base(errorsCollectionException)
        {
            _enumerableToCheck = enumerableToCheck();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(enumerableToCheck);
            if (_enumerableToCheck != null) _enumerableCount = _enumerableToCheck.Count();
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckEnumerableOfTCondition<T> CountIsLessThan(int size)
        {
            if (_enumerableCount >= size)
            {
                HandleException(GetLessThanInvalidSizeException("count", _enumerableCount, size));
            }
            return this;
        }

        public CheckEnumerableOfTCondition<T> CountIsLessThan<E>(int size, object[] exceptionArguments)
            where E : Exception
        {
            if (_enumerableCount >= size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckEnumerableOfTCondition<T> CountIsMoreThan(int size)
        {
            if (_enumerableCount <= size)
            {
                HandleException(GetMoreThanInvalidSizeException("count", _enumerableCount, size));
            }
            return this;
        }

        public CheckEnumerableOfTCondition<T> CountIsMoreThan<E>(int size, object[] exceptionArguments)
            where E : Exception
        {
            if (_enumerableCount <= size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckEnumerableOfTCondition<T> CountIsEqualTo(int size)
        {
            if (_enumerableCount != size)
            {
                HandleException(GetEqualToInvalidSizeException("count", _enumerableCount, size));
            }
            return this;
        }

        public CheckEnumerableOfTCondition<T> CountIsEqualTo<E>(int size, object[] exceptionArguments)
            where E : Exception
        {
            if (_enumerableCount != size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckEnumerableOfTCondition<T> CountIsBetween(int lowerBound, int upperBound)
        {
            if (_enumerableCount < lowerBound || _enumerableCount > upperBound)
            {
                HandleException(GetBetweenInvalidSizeException("count", _enumerableCount, lowerBound, upperBound));
            }
            return this;
        }

        public CheckEnumerableOfTCondition<T> CountIsBetween<E>(int lowerBound, int upperBound,
                                                                object[] exceptionArguments) where E : Exception
        {
            if (_enumerableCount < lowerBound || _enumerableCount > upperBound)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckEnumerableOfTCondition<T> CountIsMoreOrEqualTo(int size)
        {
            if (_enumerableCount < size)
            {
                HandleException(GetMoreOrEqualToInvalidSizeException("count", _enumerableCount, size));
            }
            return this;
        }

        public CheckEnumerableOfTCondition<T> CountIsMoreOrEqualTo<E>(int size, object[] exceptionArguments)
            where E : Exception
        {
            if (_enumerableCount < size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckEnumerableOfTCondition<T> CountIsLessOrEqualTo(int size)
        {
            if (_enumerableCount > size)
            {
                HandleException(GetLessOrEqualToInvalidSizeException("count", _enumerableCount, size));
            }
            return this;
        }

        public CheckEnumerableOfTCondition<T> CountIsLessOrEqualTo<E>(int size, object[] exceptionArguments)
            where E : Exception
        {
            if (_enumerableCount > size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="NullReferenceException"></exception>
        public CheckEnumerableOfTCondition<T> IsNotNull()
        {
            IsNotNull(_enumerableToCheck);
            madeNotNull();
            return this;
        }

        public CheckEnumerableOfTCondition<T> IsNotNull<E>(object[] exceptionArguments) where E : Exception
        {
            IsNotNull<E>(_enumerableToCheck, exceptionArguments);
            madeNotNull();
            return this;
        }

        /// <exception cref="NotNullException"></exception>
        public CheckEnumerableOfTCondition<T> IsNull()
        {
            IsNull(_enumerableToCheck);
            madeNotNull();
            return this;
        }

        public CheckEnumerableOfTCondition<T> IsNull<E>(object[] exceptionArguments) where E : Exception
        {
            IsNull<E>(_enumerableToCheck, exceptionArguments);
            madeNotNull();
            return this;
        }

        private void madeNotNull()
        {
            if (_enumerableToCheck == null) _enumerableToCheck = new List<T>();
        }

        public CheckEnumerableOfTCondition<T> Contains(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                checkEnumerableContainsElement(value);
            }
            return this;
        }

        public CheckEnumerableOfTCondition<T> Contains<E>(IEnumerable<T> values, object[] exceptionArguments)
            where E : Exception
        {
            foreach (T value in values)
            {
                checkEnumerableContainsElement<E>(value, exceptionArguments);
            }
            return this;
        }

        private void checkEnumerableContainsElement<E>(T value, object[] exceptionArguments) where E : Exception
        {
            if (!_enumerableToCheck.Contains(value))
                HandleException(GetException<E>(exceptionArguments));
        }

        public CheckEnumerableOfTCondition<T> Contains(T value)
        {
            checkEnumerableContainsElement(value);
            return this;
        }

        private void checkEnumerableContainsElement(T value)
        {
            if (!_enumerableToCheck.Contains(value))
                HandleException(
                    new IndexOutOfRangeException(string.Format("The enumerable does not contains {0}", value)));
        }

        public CheckEnumerableOfTCondition<T> Contains<E>(T value, object[] exceptionArguments) where E : Exception
        {
            checkEnumerableContainsElement<E>(value, exceptionArguments);
            return this;
        }
    }
}