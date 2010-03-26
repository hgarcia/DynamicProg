using System.Collections.Specialized;
using DynamicProg.Extensions;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class ObjectExtensionsTest
    {
        private const string VALUE_SET_FOR_PROPERTY_ONE = "Value set for property one";

        [Test]
        public void Get_the_properties_from_an_object()
        {
            var result = new MyClass().GetPropertyList();

            Assert.That(result.Count == 2);
            Assert.That(result.ContainsKey("PropertyOne"));
        }

        [Test]
        public void Get_the_fields_from_an_object()
        {
            var result = new MyClass().GetFieldList();

            Assert.That(result.Count == 2);
            Assert.That(result.ContainsKey("_fieldOne"));
        }

        [Test]
        public void Get_the_fields_and_properties_from_an_object()
        {
            var result = new MyClass().GetFieldAndPropertyList();

            Assert.That(result.Count == 4);
            Assert.That(result.ContainsKey("_fieldOne"));
            Assert.That(result.ContainsKey("PropertyOne"));
        }

        [Test]
        public void Sets_the_value_for_a_property()
        {
            var m = new MyClass();
            m.SetProperty("PropertyOne", "Value one");

            Assert.AreEqual(m.PropertyOne , "Value one");
        }

        [Test]
        public void Sets_the_value_for_a_field()
        {
            var m = new MyClass();
            m.SetField("_fieldOne", "Value one");

            Assert.AreEqual(m._fieldOne, "Value one");
        }

        [Test]
        public void Gets_the_value_for_a_property()
        {
            var result = new MyClass().GetPropertyValue<string>("PropertyOne");

            Assert.AreEqual("Value of property one", result);
        }

        [Test]
        public void Gets_the_value_for_a_field()
        {
            var result = new MyClass().GetFieldValue<string>("_fieldOne");

            Assert.AreEqual("value of field 1", result);
        }

        [Test]
        public void Sets_Properties_from_a_NameValueCollection()
        {
            var collection = new NameValueCollection
                                 {
                                     {"propertyone", VALUE_SET_FOR_PROPERTY_ONE},
                                     {"propertythree", "this does not exist"},
                                     {"PropertyTwo", "Trying to change a read only property"}
                                 };

           var mc = new MyClass().Hydrate(collection);
                                               
           Assert.That(mc.PropertyOne, Is.EqualTo(VALUE_SET_FOR_PROPERTY_ONE));
           Assert.That(mc.PropertyTwo, Is.EqualTo("Value of property two"));

        }
    }

}