using WatiN.Core;

namespace UiDriver
{
    public class Publish : WebPage
    {
        public Publish(Browser browser) : base(browser)
        {
            _browser.Link(Find.ByText("Publish")).Click();
        }

        public TextField Age()
        {
            return _browser.TextField(Find.ByName("Age"));
        }

        public TextField Name()
        {
            return _browser.TextField(Find.ByName("Name"));
        }

        public SelectList Breed()
        {
            return _browser.SelectList(Find.ByName("Breed"));
        }

        public SelectList Status()
        {
            return _browser.SelectList(Find.ByName("Status"));
        }

        public void CreateNew()
        {
            _browser.Link(Find.ByText("Create New")).ClickNoWait();
        }

        public void Save()
        {
            _browser.Button(Find.ByValue("Create")).Click();
        }

        public Table List()
        {
            return _browser.Table(Find.ById("list"));
        }
    }
}
