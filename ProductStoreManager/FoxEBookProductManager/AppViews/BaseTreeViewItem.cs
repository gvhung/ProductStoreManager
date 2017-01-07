using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using StoreManager.FoxEBookProductManager.Core;
using ServiceDataContracts;
using System.Windows;
using StoreManager.Common;
using System.Windows.Media.Imaging;

namespace StoreManager.FoxEBookProductManager.AppViews
{
    public enum DownloadStatus
    {
        NotInitiated = 0,
        Downloading,
        DownloadCompleted,
        DownloadPartial,
        DownloadFailed,
        DownloadQueued
    }

    public class BaseTreeViewItem : TreeViewItem
    {
        protected FoxeTreeViewNotifications FoxeTreeViewNotifications;
        protected StackPanel StackPanel;

        protected CheckBox CheckBox { get; set; }
        protected TextBlock MainTitleTxtBlk { get; set; }

        protected Image ProductsImage { get; set; }
        protected TextBlock ProductsCountTxtBlk { get; set; }

        public DownloadStatus DownloadStatus { get; set; }
        protected Image DownLoadStatusImage { get; set; }

        public bool AreProductsSent { get; set; }
        protected Image ProductsSentImage { get; set; }

        public bool IsChecked
        {
            get { return CheckBox.IsChecked ?? false; }
            set { CheckBox.IsChecked = value; }
        }

        public string MainTitle
        {
            get { return MainTitleTxtBlk.Text; }
            set { MainTitleTxtBlk.Text = value; }
        }

        public BaseTreeViewItem()
        {
            FoxeTreeViewNotifications = Global.FoxeTreeViewNotifications;
            StackPanel = new StackPanel();
            StackPanel.Orientation = Orientation.Horizontal;

            CheckBox = new CheckBox();
            CheckBox.IsChecked = false;
            CheckBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            CheckBox.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            CheckBox.Width = 16;
            CheckBox.Height = 16;
            CheckBox.Margin = new Thickness(2);

            MainTitleTxtBlk = new TextBlock();
            MainTitleTxtBlk.Text = "ENTER TITLE";
            MainTitleTxtBlk.Margin = new Thickness(2);
            MainTitleTxtBlk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            ProductsImage = new Image();
            ProductsImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ProductsImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            ProductsImage.Width = 20;
            ProductsImage.Height = 20;
            ProductsImage.Margin = new Thickness(2);
            ProductsImage.Source = new BitmapImage(new Uri(@"Resources/Images/productsImg.jpg", UriKind.RelativeOrAbsolute));

            ProductsCountTxtBlk = new TextBlock();
            ProductsCountTxtBlk.Text = "(X)";
            ProductsCountTxtBlk.Margin = new Thickness(2);
            ProductsCountTxtBlk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            DownloadStatus = DownloadStatus.NotInitiated;
            DownLoadStatusImage = new Image();
            DownLoadStatusImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            DownLoadStatusImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            DownLoadStatusImage.Width = 20;
            DownLoadStatusImage.Height = 20;
            DownLoadStatusImage.Margin = new Thickness(2);
            DownLoadStatusImage.Source = new BitmapImage(new Uri(@"Resources/Images/downloadNotInit.jpg", UriKind.RelativeOrAbsolute));

            AreProductsSent = false;
            ProductsSentImage = new Image();
            ProductsSentImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ProductsSentImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            ProductsSentImage.Width = 20;
            ProductsSentImage.Height = 20;
            ProductsSentImage.Margin = new Thickness(2);
            ProductsSentImage.Source = new BitmapImage(new Uri(@"Resources/Images/productNotSent.jpg", UriKind.RelativeOrAbsolute));
        }

        protected MenuItem GetDownloadProductsCtxMenuItem(Object Tag)
        {
            MenuItem downloadProductsCtxMenuItem = new MenuItem();
            downloadProductsCtxMenuItem.Tag = Tag;
            downloadProductsCtxMenuItem.Header = "Download Products";
            downloadProductsCtxMenuItem.Click += new RoutedEventHandler(downloadProductsCtxMenuItem_Click);
            return downloadProductsCtxMenuItem;
        }

        protected MenuItem GetSendProductsCtxMenuItem(Object Tag)
        {
            MenuItem sendProductsToStoreCtxMenuItem = new MenuItem();
            sendProductsToStoreCtxMenuItem.Tag = Tag;
            sendProductsToStoreCtxMenuItem.Header = "Send Products";
            sendProductsToStoreCtxMenuItem.Click += new RoutedEventHandler(sendProductsToStoreCtxMenuItem_Click);
            return sendProductsToStoreCtxMenuItem;
        }

        protected MenuItem GetSendCategoriesCtxMenuItem(Object Tag)
        {
            MenuItem sendCategoriesStoreCtxMenuItem = new MenuItem();
            sendCategoriesStoreCtxMenuItem.Tag = Tag;
            sendCategoriesStoreCtxMenuItem.Header = "Send Categories";

            MenuItem dropCreateDatabaseCtxMenuItem = new MenuItem();
            dropCreateDatabaseCtxMenuItem.Tag = Tag;
            dropCreateDatabaseCtxMenuItem.Click += new RoutedEventHandler(dropCreateDatabaseCtxMenuItem_Click);
            dropCreateDatabaseCtxMenuItem.Header = "By DropCreate";

            MenuItem createIfNotExistCtxMenuItem = new MenuItem();
            createIfNotExistCtxMenuItem.Tag = Tag;
            createIfNotExistCtxMenuItem.Click += new RoutedEventHandler(createIfNotExistCtxMenuItem_Click);
            createIfNotExistCtxMenuItem.Header = "Create If NotExist";

            sendCategoriesStoreCtxMenuItem.Items.Add(dropCreateDatabaseCtxMenuItem);
            sendCategoriesStoreCtxMenuItem.Items.Add(createIfNotExistCtxMenuItem);

            return sendCategoriesStoreCtxMenuItem;
        }

        protected MenuItem GetDisplayProductsCtxMenuItem(Object Tag)
        {
            MenuItem displayProductsCtxMenuItem = new MenuItem();
            displayProductsCtxMenuItem.Tag = Tag;
            displayProductsCtxMenuItem.Header = "Display Products";
            displayProductsCtxMenuItem.Click += new RoutedEventHandler(displayProductsCtxMenuItem_Click);
            return displayProductsCtxMenuItem;
        }


        protected MenuItem GetDisplayProductCtxMenuItem(Object Tag)
        {
            MenuItem displayProductCtxMenuItem = new MenuItem();
            displayProductCtxMenuItem.Tag = Tag;
            displayProductCtxMenuItem.Header = "Display Product";
            displayProductCtxMenuItem.Click += new RoutedEventHandler(displayProductCtxMenuItem_Click);
            return displayProductCtxMenuItem;
        }


        protected MenuItem GetGotoParentCtxMenuItem(Object Tag)
        {
            MenuItem gotoParentCtxMenuItem = new MenuItem();
            gotoParentCtxMenuItem.Tag = Tag;
            gotoParentCtxMenuItem.Header = "Goto Parent";
            gotoParentCtxMenuItem.Click += new RoutedEventHandler(gotoParentCtxMenuItem_Click);
            return gotoParentCtxMenuItem;
        }

        protected virtual void gotoParentCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        protected virtual void downloadProductsCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        protected virtual void displayProductCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }


        protected virtual void sendProductsToStoreCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        protected virtual void displayProductsCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        protected virtual void createIfNotExistCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void dropCreateDatabaseCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
