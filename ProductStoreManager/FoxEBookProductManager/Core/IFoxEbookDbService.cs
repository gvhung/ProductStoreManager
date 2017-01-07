using System.Collections.Generic;
using System.ServiceModel;
using ServiceDataContracts;

namespace StoreManager.FoxEBookProductManager.Core
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFoxEbookCrawler" in both code and config file together.
    [ServiceContract]
    public interface IFoxEbookDbService
    {
        [OperationContract]
        void AddCategoryMenuItems(IList<FoxeCategory> categoryItems);

        [OperationContract]
        void AddCategoryMenuItem(FoxeCategory categoryItem);

        [OperationContract]
        void AddSortMenuItems(IList<FoxeSort> sortItems);

        [OperationContract]
        IList<FoxeCategory> GetCategoryMenuItems();

        [OperationContract]
        IList<FoxeSort> GetSortMenuItems();

        [OperationContract]
        void AddProducts(IList<FoxeProduct> products);
    }
}
