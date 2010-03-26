using System;
using System.Collections.Generic;

namespace DynamicProg.Validation
{
    public class ErrorsCollectionException : Exception
    {
        private readonly IDictionary<string, List<Exception>> _errors = new Dictionary<string, List<Exception>>();

        public IDictionary<string,List<Exception>> ErrorsCollection
        {
            get
            {
                return _errors;
            }
        }

        public void AddError(string key, Exception e)
        {
            if (!_errors.ContainsKey(key))
            {
                _errors.Add(key,new List<Exception>());
            }
            _errors[key].Add(e);
        }
    }
}