using StorEvil.Utility;
using Calculator;

namespace StorEvilSpecs
{
    [StorEvil.Context]
    public class CalculatorContext
    {
        Basic _calculator =  new Basic();

        public void Given_I_enter_the_number(int arg0)
        {
            _calculator.Enter(arg0);
        }
        public void And_enter_the_number(int arg0)
        {
            _calculator.Enter(arg0);
        }
        public void When_Add_is_been_call()
        {
            _calculator.Add();
        }

        public void Then_the_result_should_be(int arg0)
        {
            var result = _calculator.GetResult();
            result.ShouldEqual(arg0);
        }
    }
}
