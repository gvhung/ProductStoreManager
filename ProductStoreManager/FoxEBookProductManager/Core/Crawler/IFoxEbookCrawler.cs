using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceDataContracts;

namespace StoreManager.FoxEBookProductManager.Core.Crawler
{
    public interface IFoxEbookCrawler
    {
        IList<FoxeCategory> GetCategoryItems();
        IList<FoxeSort> GetSortItems();
        int GetNetProductsCount();
        int GetNetProductsCountUnderCategory(FoxeCategory category);
        IList<FoxeProduct> GetProductsUnderCategory(FoxeCategory category);
        IList<FoxeProduct> GetProductsUnderPage(FoxePage foxePage);
    }
}
