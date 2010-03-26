using System;

namespace DynamicProg.Validation
{
    public abstract class CheckConditionBase : ExceptionThrower
    {
        protected string _name;
        protected VariableProperties _variableProperties;

        protected CheckConditionBase(ErrorsCollectionException errorsCollectionException)
        {
            _errors = errorsCollectionException;
        }
        protected void HandleException(Exception exception)
        {
            _errors.AddError(_variableProperties.Name, exception);
        }

        /// <exception cref="NullReferenceException"></exception>
        protected void IsNotNull(object objectToCheck)
        {
            if (objectToCheck == null)
            {
                HandleException(new NullReferenceException(string.Format("{0} can't be null", _variableProperties.Name)));
            }
        }
        protected void IsNotNull<E>(object objectToCheck, object[] exceptionArguments) where E : Exception
        {
            if (objectToCheck == null)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
        }

        /// <exception cref="NotNullException"></exception>
        protected void IsNull(object objectToCheck)
        {
            if (objectToCheck != null)
            {
                HandleException(new NotNullException(_variableProperties.Name));
            }
        }
        protected virtual void IsNull<E>(object objectToCheck, object[] exceptionArguments) where E : Exception
        {
            if (objectToCheck != null)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
        }

        protected InvalidSizeException GetLessThanInvalidSizeException(string dimensionName, decimal actualSize, decimal expectedSize)
        {
            return new InvalidSizeException(_variableProperties.Name, dimensionName, actualSize, "less than", expectedSize);
        }
        protected InvalidSizeException GetMoreThanInvalidSizeException(string dimensionName, decimal actualSize, decimal expectedSize)
        {
            return new InvalidSizeException(_variableProperties.Name, dimensionName, actualSize, "more than", expectedSize);
        }
        protected InvalidSizeException GetMoreOrEqualToInvalidSizeException(string dimensionName, decimal actualSize, decimal expectedSize)
        {
            return new InvalidSizeException(_variableProperties.Name, dimensionName, actualSize, "more or equal to", expectedSize);
        }
        protected InvalidSizeException GetLessOrEqualToInvalidSizeException(string dimensionName, decimal actualSize, decimal expectedSize)
        {
            return new InvalidSizeException(_variableProperties.Name, dimensionName, actualSize, "less or equal to", expectedSize);
        }      
        protected InvalidSizeException GetEqualToInvalidSizeException(string dimensionName, decimal actualSize, decimal expectedSize)
        {
            return new InvalidSizeException(_variableProperties.Name, dimensionName, actualSize, "equal to", expectedSize);
        }
        protected InvalidSizeException GetBetweenInvalidSizeException(string dimensionName, decimal actualSize, decimal lowerBound, decimal upperBound)
        {
            return new InvalidSizeException(_variableProperties.Name, dimensionName, actualSize, "between " + lowerBound + " and", upperBound);
        }
        
    }
}