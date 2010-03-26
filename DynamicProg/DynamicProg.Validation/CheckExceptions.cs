using System;

namespace DynamicProg.Validation
{
    public class EmptyStringException : Exception
    {
        public EmptyStringException(string objectName) : base(string.Format("The string {0} should not be empty.",objectName))
        {
            
        }
    }
    public class MatchNotFoundException : Exception
    {
        public MatchNotFoundException(string objectName, string stringToValidate, string[] options)
            : base(string.Format("The string {0} for {1} does not match any of the options: {2}", stringToValidate, objectName, options.ToString()))
        {

        }
    }

    public class NotEmptyStringException : Exception
    {
        public NotEmptyStringException(string objectName)
            : base(string.Format("The string {0} should be empty.",objectName))
        {

        }
    }

    public class InvalidFormatException : Exception
    {
        public InvalidFormatException(string message):base(message)
        {
            
        }
    }

    public class NotNullException : Exception
    {
        public NotNullException(string objectName)
            : base(string.Format("{0} should be null", objectName))
        {
        }
    }

    public class NotEqualException : Exception
    {
        public NotEqualException(string toValidate, string toCompareWith)
            : base(string.Format("The value: {0} is different from: {1}", toValidate, toCompareWith))
        {

        }
    }

    public class NullOrEmptyStringException : Exception
    {
        public NullOrEmptyStringException(string objectName):
            base(string.Format("The string {0} should not be empty or null", objectName))
        {
            
        }
    }

    public class NotNullOrEmptyStringException : Exception
    {
        public NotNullOrEmptyStringException(string objectName) :
            base(string.Format("The string {0} should be empty or null", objectName))
        {

        }
    }

    public class InvalidSizeException : Exception
    {
        public InvalidSizeException(string objectName, string dimensionName, decimal actualSize, string comparison, decimal expectedSize):base(string.Format("{0} {1} is {2} but it should be {3} {4}",objectName,dimensionName,actualSize,comparison,expectedSize))
        {}
    }

    public class InvalidValueException : Exception
    {
        public InvalidValueException(string message)
            : base(message)
        {
        }
    }
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException(string collectionName)
            : base(string.Format("The collection {0} is empty.", collectionName))
        {

        }
    }
}