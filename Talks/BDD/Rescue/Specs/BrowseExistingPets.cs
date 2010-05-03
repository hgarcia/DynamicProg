using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Specs
{
[Binding]
    public class BrowseExistingPets 
    {
        [Given(@"I published some pets")]
        public void GivenIPublishedSomePets()
        {
            //ScenarioContext.Current.Pending();
        }

        [When(@"I click the ""(.*)"" menu item")]
        public void WhenIClickThePublishMenuItem(string menuItem)
        {
            Assert.That(menuItem,Is.EqualTo("Publish"));
            ScenarioContext.Current.Pending();
        }
    
        [Then(@"I should see a list of those pets")]
        public void ThenIShouldSeeAListOfThosePets()
        {
            ScenarioContext.Current.Pending();
        }
    }


}
