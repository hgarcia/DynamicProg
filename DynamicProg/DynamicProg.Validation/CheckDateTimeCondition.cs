using System;

namespace DynamicProg.Validation
{
    public class CheckDateTimeCondition : CheckConditionBase
    {
        private readonly DateTime _dateTimeToCheck;

        public CheckDateTimeCondition(Func<DateTime> dateTimeToCheck, ErrorsCollectionException errors) : base(errors)
        {
            _dateTimeToCheck = dateTimeToCheck();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(dateTimeToCheck);
        }

        /// <exception cref="InvalidValueException"></exception>
        public CheckDateTimeCondition IsEqualTo(DateTime dateTime)
        {
            if (_dateTimeToCheck.CompareTo(dateTime) != 0)
            {
                HandleException(new InvalidValueException(string.Format("The DateTime {0} value is {1} and should be {2}", _variableProperties.Name, _dateTimeToCheck, dateTime)));
            }
            return this;
        }
        public CheckDateTimeCondition IsEqualTo<E>(DateTime dateTime, object[] exceptionArguments) where E : Exception
        {
            if (_dateTimeToCheck.CompareTo(dateTime) != 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidValueException"></exception>
        public CheckDateTimeCondition IsBefore(DateTime dateTime)
        {
            if(_dateTimeToCheck.CompareTo(dateTime) >= 0)
            {
                HandleException(new InvalidValueException(string.Format("The DateTime {0} value is {1} and should be before {2}",_variableProperties.Name,_dateTimeToCheck,dateTime)));
            }
            return this;
        }
        public CheckDateTimeCondition IsBefore<E>(DateTime dateTime, object[] exceptionArguments) where E : Exception
        {
            if (_dateTimeToCheck.CompareTo(dateTime) >= 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidValueException"></exception>
        public CheckDateTimeCondition IsBeforeOrEqualTo(DateTime dateTime)
        {
            if (_dateTimeToCheck.CompareTo(dateTime) > 0)
            {
                HandleException(new InvalidValueException(string.Format("The DateTime {0} value is {1} and should be before or equal to {2}", _variableProperties.Name, _dateTimeToCheck, dateTime)));
            }
            return this;
        }
        public CheckDateTimeCondition IsBeforeOrEqualTo<E>(DateTime dateTime, object[] exceptionArguments) where E : Exception
        {
            if (_dateTimeToCheck.CompareTo(dateTime) > 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidValueException"></exception>
        public CheckDateTimeCondition IsAfter(DateTime dateTime)
        {
            if (_dateTimeToCheck.CompareTo(dateTime) <= 0)
            {
                HandleException(new InvalidValueException(string.Format("The DateTime {0} value is {1} and should be after {2}", _variableProperties.Name, _dateTimeToCheck, dateTime)));
            }
            return this;
        }
        public CheckDateTimeCondition IsAfter<E>(DateTime dateTime, object[] exceptionArguments) where E : Exception
        {
            if (_dateTimeToCheck.CompareTo(dateTime) <= 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidValueException"></exception>
        public CheckDateTimeCondition IsAfterOrEqualTo(DateTime dateTime)
        {
            if (_dateTimeToCheck.CompareTo(dateTime) < 0)
            {
                HandleException(new InvalidValueException(string.Format("The DateTime {0} value is {1} and should be after or equal to {2}", _variableProperties.Name, _dateTimeToCheck, dateTime)));
            }
            return this;
        }
        public CheckDateTimeCondition IsAfterOrEqualTo<E>(DateTime dateTime, object[] exceptionArguments) where E : Exception
        {
            if (_dateTimeToCheck.CompareTo(dateTime) < 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="InvalidValueException"></exception>
        public CheckDateTimeCondition IsBetween(DateTime beforeDate, DateTime afterDate)
        {
            if(_dateTimeToCheck.CompareTo(beforeDate)<0 || _dateTimeToCheck.CompareTo(afterDate)>0)
            {
                HandleException(new InvalidValueException(string.Format("The DateTime {0} value is {1} and should be between {2} and {3}", _variableProperties.Name, _dateTimeToCheck, afterDate,beforeDate)));            
            }
            return this;
        }
        public CheckDateTimeCondition IsBetween<E>(DateTime beforeDate, DateTime afterDate, object[] exceptionArguments) where E : Exception
        {
            if (_dateTimeToCheck.CompareTo(beforeDate) < 0 || _dateTimeToCheck.CompareTo(afterDate) > 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }
    }
}