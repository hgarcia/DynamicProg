using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;
using UiDriver;
using WatiN.Core;

namespace Specs
{
    [Binding]
    public class AddANewPet
    {
        private static Publish publish;
        //private static Browser browser;

        [BeforeFeature]
        public static void Setup()
        {
            publish = new Rescue().GoTo<Publish>(); 
            //browser = new IE("http://rescue.com:1010/");
        }
        [Given(@"I have entered all the information for a pet")]
        public void GivenIHaveEnteredAllTheInformationForAPet()
        {
            publish.CreateNew();
            publish.Name().Value = "Fido";
            publish.Status().Select("Found");
            publish.Breed().Select("Labrador");
            publish.Age().Value = "3";

            //browser.Link(Find.ByText("Publish")).Click();
            //browser.Link(Find.ByText("Create New")).ClickNoWait();
            //browser.TextField(Find.ByName("Name")).Value = "Eddie";
            //browser.SelectList(Find.ByName("Status")).Select("Found");
            //browser.SelectList(Find.ByName("Breed")).Select("Golden");
            //browser.TextField(Find.ByName("Age")).Value = "4";
        }

        [When(@"I save the pet")]
        public void WhenISaveThePer()
        {
            publish.Save();

            //browser.Button(Find.ByValue("Create")).Click();
        }

        [Then(@"I should see the pet in the list")]
        public void ThenIShouldSeeThePetInTheList()
        {
            var tableContent = publish.List().InnerHtml;
            Assert.That(tableContent.ToLower(), Is.StringContaining("fido"));

            //var tableContent = browser.Table(Find.ById("list")).InnerHtml;
            //Assert.That(tableContent.ToLower(), Is.StringContaining("eddie"));
        }
        [AfterFeature]
        public static void TearDown()
        {
            publish.Close();

            //browser.Close();
        }
    }
}

