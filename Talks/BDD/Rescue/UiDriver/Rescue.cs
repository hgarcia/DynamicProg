using System;
using WatiN.Core;

namespace UiDriver
{
    public class Rescue : IDisposable
    {
        internal Browser Browser;
        public Rescue()
        {
            Browser = new IE("http://rescue.com:1010/");
        }

        public TPage GoTo<TPage>() where TPage : WebPage
        {
            return (TPage)Activator.CreateInstance(typeof (TPage), new[] {Browser});
        }

        public void Dispose()
        {
            Browser.Dispose();
        }
    }
}