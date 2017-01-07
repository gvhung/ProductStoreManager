using System;
using StoreManager.FoxEBookProductManager.AppViews;
using System.Windows.Controls;
using ServiceDataContracts;

namespace StoreManager.FoxEBookProductManager.Core
{
    public class FoxeDownloadProductsUnderCategoryEventArgs : EventArgs
    {
        public FoxeCategory FoxeCategory { get; set; }
    }

    public class FoxeDownloadProductsUnderPageEventArgs : EventArgs
    {
        public FoxePage FoxePage { get; set; }
    }


    public class FoxeSendproductsEventArgs : EventArgs
    {
    }

    public class FoxeDisplayproductsEventArgs : EventArgs
    {
    }

    public class FoxeGotoParentEventArgs : EventArgs
    {
        public TreeViewItem TreeViewItem;
    }


    public class FoxeTreeViewNotifications
    {
        public event EventHandler<FoxeDownloadProductsUnderCategoryEventArgs> DownloadFoxeProductsUnderCategory;
        public event EventHandler<FoxeDownloadProductsUnderPageEventArgs> DownloadFoxeProductsUnderPage;

        public event EventHandler<FoxeSendproductsEventArgs> SendFoxeProducts;
        public event EventHandler<FoxeDisplayproductsEventArgs> DisplayFoxeProducts;
        public event EventHandler<FoxeGotoParentEventArgs> GotoParent;

        public FoxeTreeViewNotifications()
        {

        }

        public void OnDownloadFoxeProductsUnderCategory(FoxeDownloadProductsUnderCategoryEventArgs e)
        {
            if (DownloadFoxeProductsUnderCategory != null)
            {
                DownloadFoxeProductsUnderCategory(this, e);
            }
        }

        public void OnDownloadFoxeProductsUnderPage(FoxeDownloadProductsUnderPageEventArgs e)
        {
            if (DownloadFoxeProductsUnderPage != null)
            {
                DownloadFoxeProductsUnderPage(this, e);
            }
        }

        public void OnSendFoxeProducts(FoxeSendproductsEventArgs e)
        {
            if (SendFoxeProducts != null)
            {
                SendFoxeProducts(this, e);
            }
        }

        public void OnDisplayFoxeProducts(FoxeDisplayproductsEventArgs e)
        {
            if (DisplayFoxeProducts != null)
            {
                DisplayFoxeProducts(this, e);
            }
        }

        public void OnGotoParent(FoxeGotoParentEventArgs e)
        {
            if (GotoParent != null)
            {
                GotoParent(this, e);
            }
        }
    }
}
