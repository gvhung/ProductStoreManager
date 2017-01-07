using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ServiceDataContracts;
using StoreManager.FoxEBookProductManager.Core.Crawler;
using StoreManager.FoxEBookProductManager.Core.WebClient;

namespace StoreManager.FoxEBookProductManager.Core.Parser
{
    class HtmlAgilityParser : IFoxEbookParser
    {
        public HtmlAgilityParser()
        {

        }

        public IList<FoxeCategory> ParseForCategoryItems(HtmlDocument document)
        {
            IList<FoxeCategory> categories = new List<FoxeCategory>();
            var ulNodes = document.DocumentNode.SelectNodes("//ul[@class='nav navbar-nav']//li[@class='dropdown']//ul[@class='dropdown-menu']");
            var categoryul = ulNodes[0];
            var categoryLis = categoryul.Descendants("li");
            int rank = 1;
            foreach (HtmlNode categoryLi in categoryLis)
            {
                if (categoryLi.HasChildNodes)
                {
                    var uri = categoryLi.FirstChild.Attributes["href"].Value;
                    var name = categoryLi.FirstChild.InnerText;
                    string pattern = @"/category/(?<categoryName>.+)/";
                    Match m = Regex.Match(uri, pattern);
                    if (m.Success)
                    {
                        var seoFriendlyName = m.Groups["categoryName"].Value;
                        categories.Add(new FoxeCategory()
                        {
                            Name = name,
                            Rank = rank++,
                            SEOFriendlyName = seoFriendlyName,
                            Weight = 1,
                            Enabled = true,
                            CreatedOn = DateTime.Now
                        });
                    }
                }
            }
            return categories;
        }

        public IList<FoxeSort> ParseForSortItems(HtmlDocument document)
        {
            IList<FoxeSort> sortMenuItems = new List<FoxeSort>();
            var ulNodes = document.DocumentNode.SelectNodes("//ul[@class='nav navbar-nav']//li[@class='dropdown']//ul[@class='dropdown-menu']");
            var sortUl = ulNodes[1];
            var sortLis = sortUl.Descendants("li");
            int rank = 1;
            foreach (HtmlNode sortLi in sortLis)
            {
                if (sortLi.HasChildNodes)
                {
                    var uri = sortLi.FirstChild.Attributes["href"].Value;
                    var name = sortLi.FirstChild.InnerText;
                    string pattern = @"sort=(?<sortBy>.+)";
                    Match m = Regex.Match(uri, pattern);
                    if (m.Success)
                    {
                        var seoFriendlyName = m.Groups["sortBy"].Value;
                        sortMenuItems.Add(new FoxeSort()
                        {
                            Name = name,
                            Rank = rank++,
                            SEOFriendlyName = seoFriendlyName,
                            Weight = 1,
                            Enabled = true,
                            CreatedOn = DateTime.Now
                        });
                    }
                }
            }
            return sortMenuItems;
        }

        public int ParseForNetProductCountUnderCategory(HtmlDocument document, FoxeCategory category)
        {
            var pager = document.DocumentNode.SelectNodes("//ul[@class='pagination']");
            int firstPageProductCount = ParseForPageProductCountForCategory(document);
            int lastPageProductCount = 0;
            int productCount = 0;
            int pageCount = 0;

            if (pager == null)
            {
                productCount = firstPageProductCount;
                pageCount = 1;
            }
            else
            {
                var pagerLastAnchor = document.DocumentNode.SelectNodes("//ul[@class='pagination']//li//a[@title='Last']");
                string pagerLastHref = "";
                if (pagerLastAnchor != null)
                {
                    pagerLastHref = pagerLastAnchor[0].Attributes["href"].Value;
                }
                else
                {
                    var totalAnchors = document.DocumentNode.SelectNodes("//ul[@class='pagination']//li//a").Count;
                    var pagerNextAnchor = document.DocumentNode.SelectNodes("//ul[@class='pagination']//li//a").Last();
                    pagerLastHref = pagerNextAnchor.Attributes["href"].Value;
                }

                string pattern = @"/page/(?<pageNo>\d+)/";
                Match m = Regex.Match(pagerLastHref, pattern);
                if (m.Success)
                {
                    pageCount = Int32.Parse(m.Groups["pageNo"].Value);
                }

                string lastPageUrl = CrawlerHelper.GetSchemeDomainUrl() + pagerLastHref;
                HtmlDocument lastPageDocument = new WebClientManager().DownloadHtmlDocument(lastPageUrl);
                lastPageProductCount = ParseForPageProductCountForCategory(lastPageDocument);
                productCount = (firstPageProductCount * (pageCount - 1)) + lastPageProductCount;
            }

            category.PageCount = pageCount;
            category.ProductCount = productCount;
            category.FirstPageProductCount = firstPageProductCount;
            category.LastPageProductCount = lastPageProductCount;
            return productCount;
        }

        private int ParseForPageProductCountForCategory(HtmlDocument document)
        {
            var productsCount = document.DocumentNode.SelectNodes("//div[@class='row book-top']").Count;
            return productsCount;
        }

        public int ParseForNetProductCount(HtmlDocument document)
        {
            int lastPageNo = 0;
            var pagerLastAnchor = document.DocumentNode.SelectNodes("//ul[@class='pagination']//li//a[@title='Last']");
            string pagerLastHref = pagerLastAnchor[0].Attributes["href"].Value;
            string pattern = @"/page/(?<pageNo>\d+)/";
            Match m = Regex.Match(pagerLastHref, pattern);
            if (m.Success)
            {
                lastPageNo = Int32.Parse(m.Groups["pageNo"].Value);
            }

            var firstPageProductCount = ParseForPageProductCount(document);
            string lastPageUrl = CrawlerHelper.GetSchemeDomainUrl() + pagerLastHref;
            HtmlDocument lastPageDocument = new WebClientManager().DownloadHtmlDocument(lastPageUrl);
            var lastPageProductCount = ParseForPageProductCount(lastPageDocument);
            var netProducts = (firstPageProductCount * (lastPageNo - 1)) + lastPageProductCount;
            return netProducts;
        }

        private int ParseForPageProductCount(HtmlDocument document)
        {
            var productsCount = document.DocumentNode.SelectNodes("//div[@class='col-sm-6 col-md-3 col-lg-2']").Count;
            return productsCount;
        }

        public IList<FoxeProduct> ParseForProductsUnderCategory(HtmlDocument document, FoxeCategory category)
        {
            IList<FoxeProduct> products = new List<FoxeProduct>();

            HtmlNodeCollection productNodes = document.DocumentNode.SelectNodes("//div[@class='row book-top']");
            foreach (var productnode in productNodes)
            {
                FoxeProduct product = new FoxeProduct();
                product.CoverPageUrl = productnode.ChildNodes["div"].ChildNodes["img"].Attributes["src"].Value;
                var productDetailsPageUrl = CrawlerHelper.GetSchemeDomainUrl() + productnode.Descendants("a").Last().Attributes["href"].Value;
                HtmlDocument productDetailPageDocument = new WebClientManager().DownloadHtmlDocument(productDetailsPageUrl);
                ParseProductDetailPageUpdateProduct(productDetailPageDocument, product);
                product.Category = category;
                products.Add(product);
            }
            return products;
        }

        public IList<FoxeProduct> ParseForProductsUnderPage(HtmlDocument document, FoxePage foxePage)
        {
            IList<FoxeProduct> products = new List<FoxeProduct>();

            HtmlNodeCollection productNodes = document.DocumentNode.SelectNodes("//div[@class='row book-top']");
            foreach (var productnode in productNodes)
            {
                FoxeProduct product = new FoxeProduct();
                product.CoverPageUrl = productnode.ChildNodes["div"].ChildNodes["img"].Attributes["src"].Value;
                var productDetailsPageUrl = CrawlerHelper.GetSchemeDomainUrl() + productnode.Descendants("a").Last().Attributes["href"].Value;
                HtmlDocument productDetailPageDocument = new WebClientManager().DownloadHtmlDocument(productDetailsPageUrl);
                ParseProductDetailPageUpdateProduct(productDetailPageDocument, product);
                product.Category = foxePage.Category;
                product.Page = foxePage;
                products.Add(product);
            }
            return products;
        }

        private void ParseProductDetailPageUpdateProduct(HtmlDocument document, FoxeProduct product)
        {
            try
            {
                var infoNodes = document.DocumentNode.SelectNodes("//ul[@class='list-unstyled']//li");
                product.Description = document.DocumentNode.SelectNodes("//div[@class='at-above-post']")[0].NextSibling.InnerHtml;
                foreach (var infoNode in infoNodes)
                {
                    var keyValPairs = infoNode.InnerText.Split(':');
                    AddToBookPropertyValue(keyValPairs, product);
                }
            }
            catch (ArgumentNullException)
            {
                //
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void AddToBookPropertyValue(string[] keyValPairs, FoxeProduct product)
        {
            switch (keyValPairs[0])
            {
                case "Title":
                    product.Title = keyValPairs[1].Trim();
                    break;
                case "Author":
                    product.Author = keyValPairs[1].Trim();
                    break;
                case "Length":
                    string pages = Regex.Match(keyValPairs[1].ToString().Trim(), @"\d+").Value;
                    int length;
                    Int32.TryParse(pages, out length);
                    product.Pages = length;
                    break;
                case "Edition":
                    string edition = Regex.Match(keyValPairs[1].ToString().Trim(), @"\d+").Value;
                    int ed;
                    Int32.TryParse(edition, out ed);
                    product.Edition = ed;
                    break;
                case "Language":
                    product.Language = keyValPairs[1].Trim();
                    break;
                case "Publisher":
                    product.Publisher = keyValPairs[1].Trim();
                    break;
                case "Publication Date":
                    DateTime publishedDate = new DateTime(1755, 1, 1);
                    DateTime.TryParse(keyValPairs[1].ToString().Trim(), out publishedDate);
                    product.PublishedDate = publishedDate;
                    break;
                case "ISBN-10":
                    product.ISBN_10 = keyValPairs[1].Trim();
                    break;
                case "ISBN-13":
                    product.ISBN_13 = keyValPairs[1].Trim();
                    break;
            }
        }

    }
}
