using System;
using System.Linq;

namespace DynamicProg.Validation
{
    public abstract class ExceptionThrower
    {
        protected ErrorsCollectionException _errors;

        public void ThrowFirst()
        {
            if (_errors.ErrorsCollection.Count > 0)
            {
                throw _errors.ErrorsCollection.First().Value.First();
            }
        }

        /// <exception cref="ErrorsCollectionException"></exception>
        public void Throw()
        {
            if (_errors.ErrorsCollection.Count > 0)
            {
                throw _errors;
            }
        }

        public void Throw<E>(object[] exceptionArguments) where E :  Exception
        {
            if (_errors.ErrorsCollection.Count > 0)
            {
                throw GetException<E>(exceptionArguments);
            }
        }

        public ErrorsCollectionException List()
        {
            return _errors;
        }

        protected static E GetException<E>(object[] exceptionArguments)
        {
            return (E)Activator.CreateInstance(typeof(E), exceptionArguments);
        }
    }
}