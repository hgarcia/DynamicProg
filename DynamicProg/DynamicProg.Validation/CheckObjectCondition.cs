using System;

namespace DynamicProg.Validation
{
    public class CheckObjectCondition : CheckConditionBase
    {
        private readonly object _objectToCheck;

        public CheckObjectCondition(Func<object> objectToCheck, ErrorsCollectionException errors) : base(errors)
        {
            _objectToCheck = objectToCheck();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(objectToCheck);
        }

        /// <exception cref="NullReferenceException"></exception>
        public CheckObjectCondition IsNotNull()
        {
            if(_objectToCheck == null)
            {
                HandleException(new NullReferenceException(string.Format("The object {0} is null.",_variableProperties.Name)));
            }
            return this;
        }
        public CheckObjectCondition IsNotNull<E>(object[] exceptionArguments) where E : Exception
        {
            if(_objectToCheck == null)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="NotNullException"></exception>
        public CheckObjectCondition IsNull()
        {
            if (_objectToCheck != null)
            {
                HandleException(new NotNullException(_variableProperties.Name));
            }
            return this;
        }
        public CheckObjectCondition IsNull<E>(object[] exceptionArguments) where E : Exception
        {
            if(_objectToCheck != null)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidCastException"></exception>
        public CheckObjectCondition IsInstanceOf(Type type)
        {
            if(_objectToCheck.GetType() != type)
            {
                HandleException(new InvalidCastException(string.Format("The object {0} should be an instance of {1} but is an instance of {2}.",_variableProperties.Name,_objectToCheck.GetType(),type)));
            }
            return this;
        }
        public CheckObjectCondition IsInstanceOf<E>(Type type,object[] exceptionArguments) where E : Exception
        {
            if (_objectToCheck.GetType() != type)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }
    }
}