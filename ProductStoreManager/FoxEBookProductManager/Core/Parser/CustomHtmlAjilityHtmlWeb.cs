using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Net;

namespace StoreManager.FoxEBookProductManager.Core.Parser
{
    public class CustomHtmlAjilityHtmlWeb : HtmlWeb
    {
        public CustomHtmlAjilityHtmlWeb()
            : base()
        {
            Init();
        }

        public void Init()
        {
            this.PreRequest += request =>
            {
                //request.Timeout = 10000;
                RequestInfrastructure.SetHeaders(request.Headers);
                return true;
            };
            var usrAgent = RequestInfrastructure.GetRandomUserAgent();
            if (!String.IsNullOrEmpty(usrAgent))
                this.UserAgent = usrAgent;
        }
    }

    public static class RequestInfrastructure
    {
        public enum USER_AGENT
        {
            //UC_BROWSER_YUREKHA = 0,
            CHROME_BROWSER_OLD,
            FIREFOX,
            MOZILLA
        }

        private static Dictionary<USER_AGENT, string> userAgents;
        static RequestInfrastructure()
        {
            userAgents = new Dictionary<USER_AGENT, string>();
            userAgents[USER_AGENT.CHROME_BROWSER_OLD] = "Mozilla/5.0+(Windows+NT+6.0)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome/49.0.2623.112+Safari/537.36";
            //userAgents[USER_AGENT.UC_BROWSER_YUREKHA] = "Mozilla/5.0+(Linux;+U;+Android+5.1.1;+en-US;+YU5510+Build/LMY49J)+AppleWebKit/534.30+(KHTML,+like+Gecko)+Version/4.0+UCBrowser/11.0.5.850+U3/0.8.0+Mobile+Safari/534.30";
            userAgents[USER_AGENT.FIREFOX] = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1";
            userAgents[USER_AGENT.MOZILLA] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; rv:2.2) Gecko/20110201";
        }

        public static void SetHeaders(WebHeaderCollection headers)
        {
            headers["Cache-Control"] = "max-age=0";
            headers["Accept-Language"] = "en-US,en;q=0.8,te;q=0.6";
            //Unhandled Exception: System.ArgumentException: This header must be modified using the appropriate property or method.
            //headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //headers["Accept-Encoding"] = "deflate";
            //headers["Connection"] = "keep-alive";
            //headers["Host"] = new Uri(AppSettings.FoxebooknetBaseUrl).Host;
            //headers["Upgrade-Insecure-Requests"] = "1";
        }

        public static WebHeaderCollection GetHeaders()
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers["Cache-Control"] = "max-age=0";
            headers["Accept-Language"] = "en-US,en;q=0.8,te;q=0.6";
            //Unhandled Exception: System.ArgumentException: This header must be modified using the appropriate property or method.
            //headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //headers["Accept-Encoding"] = "deflate";
            //headers["Connection"] = "keep-alive";
            //headers["Host"] = new Uri(AppSettings.FoxebooknetBaseUrl).Host;
            //headers["Upgrade-Insecure-Requests"] = "1";
            return headers;
        }

        public static string GetRandomUserAgent()
        {
            int randVal = new Random().Next(0, Enum.GetNames(typeof(USER_AGENT)).Length - 1);
            return userAgents[(USER_AGENT)randVal];
        }
    }

}
