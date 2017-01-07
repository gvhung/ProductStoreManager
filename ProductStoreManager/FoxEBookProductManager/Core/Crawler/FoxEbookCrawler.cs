using System.Collections.Generic;
using ServiceDataContracts;
using HtmlAgilityPack;
using StoreManager.FoxEBookProductManager.Core.Parser;
using StoreManager.FoxEBookProductManager.Core.WebClient;
using StoreManager.Common;

namespace StoreManager.FoxEBookProductManager.Core.Crawler
{
    public class FoxEbookCrawler : IFoxEbookCrawler
    {
        IFoxEbookParser foxEbookParser;
        WebClientManager webClientManager;
        public FoxEbookCrawler()
        {
            foxEbookParser = new HtmlAgilityParser();
            webClientManager = new WebClientManager();
        }

        public IList<FoxeCategory> GetCategoryItems()
        {
            string categoryItems_PageUrl = CrawlerHelper.GetCategoryItems_PageUrl();
            HtmlDocument categoryItemsPageDocument = webClientManager.DownloadHtmlDocument(categoryItems_PageUrl);
            IList<FoxeCategory> categoryItems = foxEbookParser.ParseForCategoryItems(categoryItemsPageDocument);
            return categoryItems;
        }

        public IList<FoxeSort> GetSortItems()
        {
            string sortItems_PageUrl = CrawlerHelper.GetSortItems_PageUrl();
            HtmlDocument sortItemsPageDocument = webClientManager.DownloadHtmlDocument(sortItems_PageUrl);
            IList<FoxeSort> sortItems = foxEbookParser.ParseForSortItems(sortItemsPageDocument);
            return sortItems;
        }

        public int GetNetProductsCount()
        {
            string productsCount_PageUrl = CrawlerHelper.GetNetProductCount_PageUrl().Replace("{{PAGE_NO}}", "1");
            HtmlDocument productsCountPageDocument = webClientManager.DownloadHtmlDocument(productsCount_PageUrl);
            return foxEbookParser.ParseForNetProductCount(productsCountPageDocument);
        }

        public int GetNetProductsCountUnderCategory(FoxeCategory category)
        {
            string productsCountUnderCategory_PageUrl = CrawlerHelper.GetNetProductCountUnderCategory_PageUrl()
                .Replace("{{CATEGORY_NAME}}", category.SEOFriendlyName)
                .Replace("{{PAGE_NO}}", "1");
            HtmlDocument productsCountUnderCategoryPageDocument = webClientManager.DownloadHtmlDocument(productsCountUnderCategory_PageUrl);
            return foxEbookParser.ParseForNetProductCountUnderCategory(productsCountUnderCategoryPageDocument, category);
        }


        public IList<FoxeProduct> GetProductsUnderCategory(FoxeCategory category)
        {
            IList<FoxeProduct> products = new List<FoxeProduct>();
            for (int currentPageNo = 1; currentPageNo <= category.PageCount; currentPageNo++)
            {
                string productsUnderCategory_PageUrl = CrawlerHelper.GetProductsUnderCategory_PageUrl()
                .Replace("{{CATEGORY_NAME}}", category.SEOFriendlyName)
                .Replace("{{PAGE_NO}}", currentPageNo.ToString());

                HtmlDocument productsCategoryPageDocument = webClientManager.DownloadHtmlDocument(productsUnderCategory_PageUrl);
                var pageProducts = foxEbookParser.ParseForProductsUnderCategory(productsCategoryPageDocument, category);
                products.AddRange(pageProducts);
            }
            return products;
        }


        public IList<FoxeProduct> GetProductsUnderPage(FoxePage foxePage)
        {
            IList<FoxeProduct> products;
            string productsUnderCategory_PageUrl = CrawlerHelper.GetProductsUnderCategory_PageUrl()
                .Replace("{{CATEGORY_NAME}}", foxePage.Category.SEOFriendlyName)
                .Replace("{{PAGE_NO}}", foxePage.PageNo.ToString());

            HtmlDocument productsCategoryPageDocument = webClientManager.DownloadHtmlDocument(productsUnderCategory_PageUrl);
            products = foxEbookParser.ParseForProductsUnderPage(productsCategoryPageDocument, foxePage);
            return products;
        }
    }
}
