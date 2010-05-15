using WatiN.Core;

namespace UiDriver
{
    public class WebPage
    {
        protected readonly Browser _browser;

        public WebPage(Browser browser)
        {
            _browser = browser;
        }

        public void Close()
        {
            _browser.Close();
           //_browser.Dispose();
        }
    }
}