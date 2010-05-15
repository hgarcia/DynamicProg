using System.Collections.Generic;

namespace Calculator
{
    public class Basic
    {
        private int _result;
        private IList<int> _numbers;

        public Basic()
        {
            _numbers = new List<int>();
        }

        public int GetResult()
        {
            return _result;
        }

        public void Enter(int number)
        {
            _numbers.Add(number);
        }

        public void Add()
        {
            foreach (var number in _numbers)
            {
                _result += number;
            }
            _numbers.Clear();
        }
    }
}