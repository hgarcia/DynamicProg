using System.Reflection;
using Calculator;
using NUnit.Framework;
using StoryQ;

namespace Stories
{
    [TestFixture]
    public class AddNumbers
    {
        private Basic _calculator;

        [Test]
        public void AddingNumbers()
        {
            new Story("Adding two numbers")
                .InOrderTo("don't make mistakes")
                .AsA("accountant")
                .IWant("to use a calculator")
                .WithScenario("Add two positive numbers")
                .Given(IStartTheCalculator)
                .And(EnterNumber,25)
                .And(EnterNumber,25)
                .When(CallAdd)
                .Then(ResultShouldBe,50)
                .ExecuteWithReport(MethodBase.GetCurrentMethod());
        }

        private void ResultShouldBe(int result)
        {
            Assert.That(result,Is.EqualTo(_calculator.GetResult()));
        }

        private void EnterNumber(int number)
        {
            _calculator.Enter(number);
        }

        private void IStartTheCalculator()
        {
            _calculator = new Basic();
        }

        private void CallAdd()
        {
            _calculator.Add();
        }

    }
}