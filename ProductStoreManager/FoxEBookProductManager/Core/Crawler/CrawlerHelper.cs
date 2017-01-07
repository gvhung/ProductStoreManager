using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManager.FoxEBookProductManager.Core.Crawler
{
    public class CrawlerHelper
    {
        public static string GetCategoryItems_PageUrl()
        {
            return CrawlerSettings.CategoryItems_PageUrl;
        }

        public static string GetSortItems_PageUrl()
        {
            return CrawlerSettings.SortItems_PageUrl;
        }

        public static string GetNetProductCount_PageUrl()
        {
            return CrawlerSettings.NetProductCount_PageUrl;
        }

        public static string GetNetProductCountUnderCategory_PageUrl()
        {
            return CrawlerSettings.NetProductCountUnderCategory_PageUrl;
        }

        public static string GetProductsUnderCategory_PageUrl()
        {
            return CrawlerSettings.ProductsUnderCategory_PageUrl;
        }

        public static string GetSchemeDomainUrl()
        {
            return CrawlerSettings.SchemeDomain;
        }
    }
}
