using System;
using StoreManager.FoxEBookProductManager.Core;
using System.Linq;
using ServiceDataContracts;
using System.Collections.Generic;
using StoreManager.LogHandler;

namespace StoreManager
{
    class Program
    {
        public static void DatabaseSetup()
        {
            SeedCategoryMenuItems();
            SeedSortmenuItems();
        }

        private static void SeedSortmenuItems()
        {
            FoxEbookDbService foxEbookDbService = new FoxEbookDbService();
            FoxEbookCrawlerService foxEbookCrawlerService = new FoxEbookCrawlerService();
            var sortItems = foxEbookCrawlerService.GetSortMenuItems();
            foxEbookDbService.AddSortMenuItems(sortItems);
        }

        private static void SeedCategoryMenuItems()
        {
            FoxEbookDbService foxEbookDbService = new FoxEbookDbService();
            FoxEbookCrawlerService foxEbookCrawlerService = new FoxEbookCrawlerService();

            var categories = foxEbookCrawlerService.GetCategoryItems();
            foreach (var category in categories)
            {
                var productsCount = foxEbookCrawlerService.GetNetProductsCountUnderCategory(category);
                int i;
                if (category.PageCount > 1)
                {
                    for (i = 1; i < category.PageCount; i++)
                    {
                        category.Pages.Add(new FoxePage()
                        {
                            PageNo = i,
                            Category = category,
                            CreatedOn = DateTime.Now,
                            DownloadableProductsCount = category.FirstPageProductCount
                        });
                    }
                    category.Pages.Add(new FoxePage()
                    {
                        PageNo = i,
                        Category = category,
                        CreatedOn = DateTime.Now,
                        DownloadableProductsCount = category.LastPageProductCount
                    });
                }
                else
                {
                    category.Pages.Add(new FoxePage()
                    {
                        PageNo = 1,
                        Category = category,
                        CreatedOn = DateTime.Now,
                        DownloadableProductsCount = category.FirstPageProductCount
                    });
                }
                LogManager.WriteToLog(new LogMessage()
                {
                    Message = string.Format("{0} has  #{1} Products under #{2} Pages", category.Name, category.ProductCount, category.PageCount)
                });
            }

            var rootCategory = new FoxeCategory()
            {
                Name = "FoxeWeb",
                SEOFriendlyName = "foxeweb",
                ParentCategoryId = null,
                CreatedOn = DateTime.Now,
                Rank = 0,
                Weight = 0
            };
            rootCategory.ChildCategories = categories;
            foxEbookDbService.AddCategoryMenuItem(rootCategory);
        }
    }
}

