using System.Collections.Generic;
using System.ServiceModel;
using ServiceDataContracts;

namespace StoreManager.FoxEBookProductManager.Core
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFoxEbookCrawler" in both code and config file together.
    [ServiceContract]
    public interface IFoxEbookCrawlerService
    {
        [OperationContract]
        IList<FoxeCategory> GetCategoryItems();

        [OperationContract]
        IList<FoxeSort> GetSortMenuItems();

        [OperationContract]
        int GetNetProductsCount();

        [OperationContract]
        int GetNetProductsCountUnderCategory(FoxeCategory foxeCategory);

        [OperationContract]
        IList<FoxeProduct> GetProductsUnderCategory(FoxeCategory foxeCategory);

        [OperationContract]
        IList<FoxeProduct> GetProductsUnderPage(FoxePage foxePage);
    }
}
