using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace DynamicProg.Validation
{
    public class Validator : ExceptionThrower
    {
        public Validator()
        {
            _errors = new ErrorsCollectionException();
        }

        public CheckXmlNodeCondition CheckThat(Func<XmlNode> nodeToCheck)
        {
            var condition = new CheckXmlNodeCondition(nodeToCheck, _errors);
            return condition;
        }

        public CheckNameValueCollectionCondition CheckThat(Func<NameValueCollection> collectionToCheck)
        {
            var condition = new CheckNameValueCollectionCondition(collectionToCheck, _errors);
            return condition;
        }
        public CheckStringCondition CheckThat(Func<string> stringToCheck)
        {
            var condition = new CheckStringCondition(stringToCheck, _errors);
            return condition;
        }
        public CheckDateTimeCondition CheckThat(Func<DateTime> dateTimeToCheck)
        {
            var condition = new CheckDateTimeCondition(dateTimeToCheck, _errors);
            return condition;
        }
        public CheckIntegerCondition CheckThat(Func<int> integerToCheck)
        {
            var condition = new CheckIntegerCondition(integerToCheck, _errors);
            return condition;
        }
        
        public CheckDoubleCondition CheckThat(Func<double> doubleToCheck)
        {
            var condition = new CheckDoubleCondition(doubleToCheck,_errors);
            return condition;
        }
        
        public CheckDecimalCondition CheckThat(Func<decimal> decimalToCheck)
        {
            var condition = new CheckDecimalCondition(decimalToCheck, _errors);
            return condition;
        }
        
        public CheckEnumerableOfTCondition<T> CheckThat<T>(Func<IEnumerable<T>> enumerableToCheck)
        {
            var condition = new CheckEnumerableOfTCondition<T>(enumerableToCheck, _errors);
            return condition;
        }

        public CheckObjectCondition CheckThat(Func<object> objectToCheck)
        {
            var condition = new CheckObjectCondition(objectToCheck, _errors);
            return condition;
        }
    }
}