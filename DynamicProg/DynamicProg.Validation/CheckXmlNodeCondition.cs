using System;
using System.Xml;

namespace DynamicProg.Validation
{
    public class CheckXmlNodeCondition : CheckConditionBase
    {
        private XmlNode _nodeToValidate;

        public CheckXmlNodeCondition(Func<XmlNode> nodeToValidate, ErrorsCollectionException errorsCollectionException)
            : base(errorsCollectionException)
        {
            _nodeToValidate = nodeToValidate();
            _variableProperties = new VariableProperties();
            _variableProperties.Load(nodeToValidate);
        }

        private void madeNotNull()
        {
            if (_nodeToValidate == null) _nodeToValidate = new XmlDocument();
        }

        /// <exception cref="NullReferenceException"></exception>
        public CheckXmlNodeCondition IsNotNull()
        {
            IsNotNull(_nodeToValidate);
            madeNotNull();
            return this;
        }
        public CheckXmlNodeCondition IsNotNull<E>(object[] exceptionArguments) where E : Exception
        {
            IsNotNull<E>(_nodeToValidate, exceptionArguments);
            madeNotNull();
            return this;
        }

        /// <exception cref="NotNullException"></exception>
        public CheckXmlNodeCondition IsNull()
        {
            IsNull(_nodeToValidate);
            madeNotNull();
            return this;
        }
        public CheckXmlNodeCondition IsNull<E>(object[] exceptionArguments) where E : Exception
        {
            IsNull<E>(_nodeToValidate, exceptionArguments);
            madeNotNull();
            return this;
        }

        /// <exception cref="NullReferenceException"></exception>
        public CheckXmlNodeCondition HasAttributeWithName(string attributeName)
        {
            if (_nodeToValidate.Attributes.Count == 0 ||
                _nodeToValidate.Attributes[attributeName] == null)
            {
                HandleException(new NullReferenceException(string.Format("The attribute {0} is not in node {1}", attributeName, _variableProperties.Name)));
            }
            return this;
        }
        public CheckXmlNodeCondition HasAttributeWithName<E>(string attributeName, object[] exceptionArguments) where E : Exception
        {
            if (_nodeToValidate.Attributes.Count == 0 ||
                _nodeToValidate.Attributes[attributeName] == null)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="EmptyCollectionException"></exception>
        public CheckXmlNodeCondition HasAttributes()
        {
            if (_nodeToValidate.Attributes.Count == 0)
            {
                HandleException(new EmptyCollectionException(_variableProperties.Name + ".Attributes"));
            }
            return this;
        }
        public CheckXmlNodeCondition HasAttributes<E>(object[] exceptionArguments) where E : Exception
        {
            if (_nodeToValidate.Attributes.Count == 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="EmptyCollectionException"></exception>
        public CheckXmlNodeCondition HasChildNode(string xpath)
        {
            if (_nodeToValidate.ChildNodes.Count == 0 ||
                _nodeToValidate.SelectSingleNode(xpath) == null)
            {
                HandleException(new NullReferenceException(string.Format("Can't find a node using the xpath query {0} in node {1}", xpath, _variableProperties.Name)));
            }
            return this;
        }
        public CheckXmlNodeCondition HasChildNode<E>(string xpath, object[] exceptionArguments) where E : Exception
        {
            if (_nodeToValidate.ChildNodes.Count == 0 ||
                _nodeToValidate.SelectSingleNode(xpath) == null)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }

        /// <exception cref="EmptyCollectionException"></exception>
        public CheckXmlNodeCondition HasChildNodes()
        {
            if (_nodeToValidate.ChildNodes.Count == 0)
            {
                HandleException(new EmptyCollectionException(_variableProperties.Name + ".ChildNodes"));
            }
            return this;
        }
        public CheckXmlNodeCondition HasChildNodes<E>(object[] exceptionArguments) where E : Exception
        {
            if (_nodeToValidate.ChildNodes.Count == 0)
            {
                HandleException(GetException<E>(exceptionArguments));
            }
            return this;
        }
    }
}