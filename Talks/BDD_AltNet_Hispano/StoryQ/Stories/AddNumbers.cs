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
                .And(EnterTheNumber,25)
                .And(EnterTheNumber,30)
                .When(IAddThem)
                .Then(TheResultShouldBe,55)
                //.Execute();
                .ExecuteWithReport(MethodBase.GetCurrentMethod());
        }

        private void TheResultShouldBe(int result)
        {
            Assert.That(result,Is.EqualTo(_calculator.GetResult()));
        }

        private void EnterTheNumber(int number)
        {
            _calculator.Enter(number);
        }

        private void IStartTheCalculator()
        {
            _calculator = new Basic();
        }

        private void IAddThem()
        {
            _calculator.Add();
        }
    }
}