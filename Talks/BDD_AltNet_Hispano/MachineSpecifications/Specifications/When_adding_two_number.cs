using Calculator;
using Machine.Specifications;

namespace Specifications
{
    [Subject("Adding two positive numbers")]
    public class When_adding_two_number
    {
        private Establish context = () =>
                                        {
                                            _calculator = new Basic();
                                            _calculator.Enter(25);
                                            _calculator.Enter(25);
                                        };

        private Because of = () => _calculator.Add();
                                 

        private It should_sum_both_numbers = () =>
                                  {
                                     _calculator.GetResult().ShouldEqual(50);
                                   };

        private static Basic _calculator;
        //private static int _result;
    }
}
