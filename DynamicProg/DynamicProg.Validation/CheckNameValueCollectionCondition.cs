using System;
using System.Collections.Specialized;

namespace DynamicProg.Validation
{
    public class CheckNameValueCollectionCondition : CheckConditionBase
    {
        private NameValueCollection _collectionToCheck;

        public CheckNameValueCollectionCondition(Func<NameValueCollection> collectionToCheck, ErrorsCollectionException errors) : base(errors)
        {
            _collectionToCheck = collectionToCheck();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(collectionToCheck);
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckNameValueCollectionCondition CountIsLessThan(int size)
        {
            if (_collectionToCheck.Count >= size)
            {
                HandleException(GetLessThanInvalidSizeException("count", _collectionToCheck.Count,size));
            }
            return this;
        }
        public CheckNameValueCollectionCondition CountIsLessThan<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_collectionToCheck.Count >= size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckNameValueCollectionCondition CountIsMoreThan(int size)
        {
            if (_collectionToCheck.Count <= size)
            {
                HandleException(GetMoreThanInvalidSizeException("count", _collectionToCheck.Count, size));
            }
            return this;
        }
        public CheckNameValueCollectionCondition CountIsMoreThan<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_collectionToCheck.Count <= size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckNameValueCollectionCondition CountIsEqualTo(int size)
        {
            if (_collectionToCheck.Count != size)
            {
                HandleException(GetEqualToInvalidSizeException("count", _collectionToCheck.Count, size));
            }
            return this;
        }
        public CheckNameValueCollectionCondition CountIsEqualTo<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_collectionToCheck.Count != size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckNameValueCollectionCondition CountIsBetween(int lowerBound, int upperBound)
        {
            if (_collectionToCheck.Count < lowerBound || _collectionToCheck.Count > upperBound)
            {
                HandleException(GetBetweenInvalidSizeException("count", _collectionToCheck.Count, lowerBound, upperBound));
            }
            return this;
        }
        public CheckNameValueCollectionCondition CountIsBetween<E>(int lowerBound, int upperBound, object[] exceptionArguments) where E : Exception
        {
            if (_collectionToCheck.Count < lowerBound || _collectionToCheck.Count > upperBound)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckNameValueCollectionCondition CountIsMoreOrEqualTo(int size)
        {
            if (_collectionToCheck.Count < size)
            {
                HandleException(GetMoreOrEqualToInvalidSizeException("count", _collectionToCheck.Count, size));
            }
            return this;
        }
        public CheckNameValueCollectionCondition CountIsMoreOrEqualTo<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_collectionToCheck.Count < size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidSizeException"></exception>
        public CheckNameValueCollectionCondition CountIsLessOrEqualTo(int size)
        {
            if (_collectionToCheck.Count > size)
            {
                HandleException(GetLessOrEqualToInvalidSizeException("count", _collectionToCheck.Count, size));
            }
            return this;
        }
        public CheckNameValueCollectionCondition CountIsLessOrEqualTo<E>(int size, object[] exceptionArguments) where E : Exception
        {
            if (_collectionToCheck.Count > size)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="NullReferenceException"></exception>
        public CheckNameValueCollectionCondition IsNotNull()
        {
            IsNotNull(_collectionToCheck);
            madeNotNull();
            return this;
        }
        public CheckNameValueCollectionCondition IsNotNull<E>(object[] exceptionArguments) where E : Exception
        {
            IsNotNull<E>(_collectionToCheck, exceptionArguments);
            madeNotNull();
            return this;
        }

        /// <exception cref="NotNullException"></exception>
        public CheckNameValueCollectionCondition IsNull()
        {
            IsNull(_collectionToCheck);
            madeNotNull();
            return this;
        }
        public CheckNameValueCollectionCondition IsNull<E>(object[] exceptionArguments) where E : Exception
        {
            IsNull<E>(_collectionToCheck, exceptionArguments);
            madeNotNull();
            return this;
        }

        /// <exception cref="IndexOutOfRangeException"></exception>
        public CheckNameValueCollectionCondition ContainsKeys(string[] keys)
        {
            foreach (var key in keys)
            {
                if (_collectionToCheck.Get(key) == null)
                {
                    HandleException(new IndexOutOfRangeException(string.Format("The collection {0} does not contains the key: {1}",_variableProperties.Name, key)));
                    continue;
                }
            }

            return this;
        }
        public CheckNameValueCollectionCondition ContainsKeys<E>(string[] keys, object[] exceptionArguments) where E : Exception
        {
            foreach (var key in keys)
            {
                if (_collectionToCheck.Get(key) == null)
                {
                    HandleException(GetException<E>(exceptionArguments));
                    continue;
                }
            }
            return this;
        }

        private void madeNotNull()
        {
            if (_collectionToCheck == null) _collectionToCheck = new NameValueCollection();
        }
    }
}