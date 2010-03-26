using System.Xml;
using DynamicProg.Validation;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class CheckThat_XmlNode_Test
    {
        private XmlDocument _doc;
        [SetUp]
        public void SetUp()
        {

            _doc = new XmlDocument();
            _doc.LoadXml(@"<root>
							<element>value
								<childNode>child node value</childNode>
								<childNode>child node value</childNode>
								<childNode>child node value</childNode>
							</element>
							<elementWithParam name=""myName""></elementWithParam>
						</root>");
        }

        [Test]
        public void Check_that_is_not_null_and_has_attributes_error()
        {
            XmlNode node = _doc.SelectSingleNode("//element");
            var list = new Validator().CheckThat(() => node).HasAttributes().List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(1));
        }
        [Test]
        public void Check_that_is_not_null_and_has_attributes()
        {
            XmlNode node = _doc.SelectSingleNode("//elementWithParam");
            var list = new Validator().CheckThat(() => node).HasAttributes().List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(0));
        }
        [Test]
        public void Check_that_has_childNodes_error()
        {
            XmlNode node = _doc.SelectSingleNode("//elementWithParam");
            var list = new Validator().CheckThat(() => node).HasChildNodes().List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(1));
        }

        [Test]
        public void Check_that_has_childNodes()
        {
            XmlNode node = _doc.SelectSingleNode("//element");
            var list = new Validator().CheckThat(() => node).HasChildNodes().List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(0));
        }

        [Test]
        public void Check_that_has_childNode_xpath_on_empty_node_error()
        {
            XmlNode node = _doc.SelectSingleNode("//elementWithParam");
            var list = new Validator().CheckThat(() => node).HasChildNode("//child").List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(1));
        }
        [Test]
        public void Check_that_has_childNode_xpath_error()
        {
            XmlNode node = _doc.SelectSingleNode("//element");
            var list = new Validator().CheckThat(() => node).HasChildNode("//child").List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(1));
        }

        [Test]
        public void Check_that_has_childNode_xpath()
        {
            XmlNode node = _doc.SelectSingleNode("//element");
            var list = new Validator().CheckThat(() => node).HasChildNode("//childNode").List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(0));
        }

        [Test]
        public void Check_that_has_attribute_by_name()
        {
            XmlNode node = _doc.SelectSingleNode("//elementWithParam");
            var list = new Validator().CheckThat(() => node).HasAttributeWithName("name").List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(0));
        }

        [Test]
        public void Check_that_has_attribute_by_name_error()
        {
            XmlNode node = _doc.SelectSingleNode("//element");
            var list = new Validator().CheckThat(() => node).HasAttributeWithName("name").List();

            Assert.That(list.ErrorsCollection.Count, Is.EqualTo(1));
        }
    }
}