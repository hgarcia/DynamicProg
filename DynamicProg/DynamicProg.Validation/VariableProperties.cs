using System;

namespace DynamicProg.Validation
{
    public struct VariableProperties
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private object _value;
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void Load<Tvariable>(Func<Tvariable> expression)
        {
            try
            {
                // get IL code behind the delegate
                var il = expression.Method.GetMethodBody().GetILAsByteArray();
                // bytes 2-6 represent the field handle
                var fieldHandle = BitConverter.ToInt32(il, 2);


                var field = expression.Target.GetType().Module.ResolveField(fieldHandle);
                _name = field.Name;
                _value = expression;
            }
            catch (Exception)
            {
                _name = string.Empty;
                _value = string.Empty;
            }
        }
    }
}