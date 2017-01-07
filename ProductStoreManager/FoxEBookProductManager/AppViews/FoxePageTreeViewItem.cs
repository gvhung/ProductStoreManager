using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using ServiceDataContracts;
using StoreManager.Common;
using StoreManager.FoxEBookProductManager.Core;
using System.Windows.Media.Imaging;
using StoreManager.FoxEBookProductManager.JobManager;

namespace StoreManager.FoxEBookProductManager.AppViews
{
    public class FoxePageTreeViewItem : BaseTreeViewItem
    {
        protected Image PageImage { get; set; }

        public FoxePage FoxePage { get; set; }
        public FoxePageTreeViewItem(FoxePage foxePage)
        {
            this.FoxePage = foxePage;
            this.Tag = foxePage;
            CreatePageTreeViewItem();
        }

        private void CreatePageTreeViewItem()
        {
            CreateHeader();
            CreateContextMenu();
        }

        private void CreateHeader()
        {
            PageImage = new Image();
            PageImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            PageImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            PageImage.Width = 20;
            PageImage.Height = 20;
            PageImage.Margin = new Thickness(2);
            PageImage.Source = new BitmapImage(new Uri(@"Resources/Images/pagesImg.jpg", UriKind.RelativeOrAbsolute));

            StackPanel.Children.Add(PageImage);
            StackPanel.Children.Add(CheckBox);
            MainTitleTxtBlk.Text = "Page #" + FoxePage.PageNo;
            StackPanel.Children.Add(MainTitleTxtBlk);

            StackPanel.Children.Add(ProductsImage);
            ProductsCountTxtBlk.Text = "(" + FoxePage.Products.Count + "/" + FoxePage.DownloadableProductsCount + ")";
            StackPanel.Children.Add(ProductsCountTxtBlk);
            this.Header = StackPanel;
        }

        private void CreateContextMenu()
        {
            base.ContextMenu = new ContextMenu();
            base.ContextMenu.Items.Add(GetDownloadProductsCtxMenuItem(FoxePage));
            base.ContextMenu.Items.Add(GetSendProductsCtxMenuItem(FoxePage));
            base.ContextMenu.Items.Add(GetDisplayProductsCtxMenuItem(FoxePage));
            base.ContextMenu.Items.Add(GetGotoParentCtxMenuItem(FoxePage));
        }

        protected override void downloadProductsCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FoxeTreeViewNotifications.OnDownloadFoxeProductsUnderPage(new FoxeDownloadProductsUnderPageEventArgs()
            {
                FoxePage = this.FoxePage
            });

            JobQueue.Enqueue(new Job(this.FoxePage));
        }

        protected override void sendProductsToStoreCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void displayProductsCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void gotoParentCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FoxeTreeViewNotifications.OnGotoParent(new FoxeGotoParentEventArgs() { TreeViewItem = this });
        }
    }
}
