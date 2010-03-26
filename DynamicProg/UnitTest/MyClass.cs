namespace UnitTest
{
    public class MyClass
    {
        public string _fieldOne = "value of field 1";

        public string PropertyOne
        {
            get
            {
                return _propertyOne;
            }
            set
            {
                _propertyOne = value;
            }
        }
        public string _fieldTwo = "value of field 2";
        private string _propertyOne = "Value of property one";

        public string PropertyTwo
        {
            get
            {
                return "Value of property two";
            }
        }
    }
}