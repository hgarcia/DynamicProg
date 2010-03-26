using System;
using DynamicProg.Validation;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class VariableProperties_Test
    {
        [Test]
        public void Gets_the_name_of_a_variable()
        {
            var variableName = "variableName";
            var variableProperties = new VariableProperties();
            variableProperties.Load(() => variableName);
            Assert.That(variableName, Is.EqualTo(variableProperties.Name));
        }

        [Test]
        public void Gets_the_name_of_a_paramters_passed_to_a_function()
        {
            var parameterName = "parameterName";
            var result = Gets_the_name_passed(() => parameterName);
            Assert.That(parameterName, Is.EqualTo(result));
        }

        private string Gets_the_name_passed<T>(Func<T> expression)
        {
            var variableProperties = new VariableProperties();
            variableProperties.Load(expression);
            return variableProperties.Name;
        }
    }

}