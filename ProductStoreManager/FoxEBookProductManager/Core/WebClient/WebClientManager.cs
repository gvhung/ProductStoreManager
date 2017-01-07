using System.Threading;
using HtmlAgilityPack;
using StoreManager.FoxEBookProductManager.Core.Parser;

namespace StoreManager.FoxEBookProductManager.Core.WebClient
{
    public class WebClientManager
    {
        public static int webCallsCounter = 0;

        public WebClientManager()
        {

        }

        public HtmlDocument DownloadHtmlDocument(string uri)
        {
            webCallsCounter++;
            CustomHtmlAjilityHtmlWeb website = new CustomHtmlAjilityHtmlWeb();
            HtmlDocument htmlDocument = website.Load(uri);
            Thread.Sleep(500);
            return htmlDocument;
        }
    }
}
