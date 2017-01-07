using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManager.FoxEBookProductManager.Core.Crawler
{
    public static class CrawlerSettings
    {
        public static string SchemeDomain { get; set; }
        public static string CategoryItems_PageUrl { get; set; }
        public static string SortItems_PageUrl { get; set; }
        public static string NetProductCount_PageUrl { get; set; }
        public static string NetProductCountUnderCategory_PageUrl { get; set; }
        public static string ProductsUnderCategory_PageUrl { get; set; }

        static CrawlerSettings()
        {
            SchemeDomain = "http://www.foxebook.net";
            CategoryItems_PageUrl = "http://www.foxebook.net/";
            SortItems_PageUrl = "http://www.foxebook.net/";
            NetProductCount_PageUrl = "http://www.foxebook.net/page/{{PAGE_NO}}";
            NetProductCountUnderCategory_PageUrl = "http://www.foxebook.net/category/{{CATEGORY_NAME}}/page/{{PAGE_NO}}/";
            ProductsUnderCategory_PageUrl = "http://www.foxebook.net/category/{{CATEGORY_NAME}}/page/{{PAGE_NO}}/";
        }
    }
}
