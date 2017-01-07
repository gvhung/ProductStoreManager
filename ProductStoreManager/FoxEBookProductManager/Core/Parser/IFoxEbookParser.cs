using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceDataContracts;
using HtmlAgilityPack;

namespace StoreManager.FoxEBookProductManager.Core.Parser
{
    public interface IFoxEbookParser
    {
        IList<FoxeCategory> ParseForCategoryItems(HtmlDocument document);
        IList<FoxeSort> ParseForSortItems(HtmlDocument document);
        int ParseForNetProductCount(HtmlDocument document);
        int ParseForNetProductCountUnderCategory(HtmlDocument document, FoxeCategory category);
        IList<FoxeProduct> ParseForProductsUnderCategory(HtmlDocument document, FoxeCategory category);
        IList<FoxeProduct> ParseForProductsUnderPage(HtmlDocument document, FoxePage foxePage);
    }
}
