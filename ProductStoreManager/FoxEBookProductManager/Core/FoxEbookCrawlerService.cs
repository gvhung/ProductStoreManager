using System.Collections.Generic;
using ServiceDataContracts;
using StoreManager.FoxEBookProductManager.Core.Crawler;

namespace StoreManager.FoxEBookProductManager.Core
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class FoxEbookCrawlerService : IFoxEbookCrawlerService
    {
        IFoxEbookCrawler crawler;
        public FoxEbookCrawlerService()
        {
            crawler = new FoxEbookCrawler();
        }

        public IList<FoxeCategory> GetCategoryItems()
        {
            return crawler.GetCategoryItems();
        }

        public IList<FoxeSort> GetSortMenuItems()
        {
            return crawler.GetSortItems();
        }

        public int GetNetProductsCount()
        {
            return crawler.GetNetProductsCount();
        }

        public int GetNetProductsCountUnderCategory(FoxeCategory category)
        {
            return crawler.GetNetProductsCountUnderCategory(category);
        }

        public IList<FoxeProduct> GetProductsUnderCategory(FoxeCategory category)
        {
            return crawler.GetProductsUnderCategory(category);
        }


        public IList<FoxeProduct> GetProductsUnderPage(FoxePage foxePage)
        {
            return crawler.GetProductsUnderPage(foxePage);
        }
    }
}
