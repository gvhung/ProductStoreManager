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
    public class FoxeCategoryTreeViewItem : BaseTreeViewItem
    {
        private Image FoxeWebImage { get; set; }
        private Image CategoryImage { get; set; }
        private TextBlock CategoriesCountTxtBlk { get; set; }
        private Image PagesImage { get; set; }
        private TextBlock PagesCountTxtBlk { get; set; }

        public FoxeCategory FoxeCategory { get; set; }
        public FoxeCategoryTreeViewItem(FoxeCategory foxeCategory)
        {
            this.FoxeCategory = foxeCategory;
            this.Tag = foxeCategory;
            CreateCategoryTreeViewItem();
        }

        private void CreateCategoryTreeViewItem()
        {
            CreateHeader();
        }

        private void CreateHeader()
        {
            if (FoxeCategory.IsRoot())
            {
                CreateRootCategoryHeaderInfra();
            }

            if (FoxeCategory.IsLeaf())
            {
                CreateLeafCategoryHeaderInfra();
            }
        }

        private void CreateRootCategoryHeaderInfra()
        {
            FoxeWebImage = new Image();
            FoxeWebImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            FoxeWebImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            FoxeWebImage.Width = 20;
            FoxeWebImage.Height = 20;
            FoxeWebImage.Margin = new Thickness(2);
            FoxeWebImage.Source = new BitmapImage(new Uri(@"Resources/Images/foxeweb.jpg", UriKind.RelativeOrAbsolute));

            CategoryImage = new Image();
            CategoryImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            CategoryImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            CategoryImage.Width = 20;
            CategoryImage.Height = 20;
            CategoryImage.Margin = new Thickness(2);
            CategoryImage.Source = new BitmapImage(new Uri(@"Resources/Images/categoryImg.jpg", UriKind.RelativeOrAbsolute));

            CategoriesCountTxtBlk = new TextBlock();
            CategoriesCountTxtBlk.Text = "(" + FoxeCategory.ChildCategories.Count() + ")";
            CategoriesCountTxtBlk.Margin = new Thickness(2);
            CategoriesCountTxtBlk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            PagesImage = new Image();
            PagesImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            PagesImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            PagesImage.Width = 20;
            PagesImage.Height = 20;
            PagesImage.Margin = new Thickness(2);
            PagesImage.Source = new BitmapImage(new Uri(@"Resources/Images/pagesImg.jpg", UriKind.RelativeOrAbsolute));

            PagesCountTxtBlk = new TextBlock();
            PagesCountTxtBlk.Text = "(" + FoxeCategory.PageCount + ")";
            PagesCountTxtBlk.Margin = new Thickness(2);
            PagesCountTxtBlk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            // Controls Added To Stack Panel
            StackPanel.Children.Add(FoxeWebImage);
            StackPanel.Children.Add(CheckBox);
            MainTitleTxtBlk.Text = FoxeCategory.Name;
            StackPanel.Children.Add(MainTitleTxtBlk);

            StackPanel.Children.Add(CategoryImage);
            CategoriesCountTxtBlk.Text = "(" + FoxeCategory.ChildCategories.Count() + ")";
            StackPanel.Children.Add(CategoriesCountTxtBlk);

            StackPanel.Children.Add(PagesImage);
            StackPanel.Children.Add(PagesCountTxtBlk);

            StackPanel.Children.Add(ProductsImage);
            ProductsCountTxtBlk.Text = "(" + FoxeCategory.ProductCount + ")";
            StackPanel.Children.Add(ProductsCountTxtBlk);
            this.Header = StackPanel;

            base.ContextMenu = new ContextMenu();
            base.ContextMenu.Items.Add(GetDownloadProductsCtxMenuItem(FoxeCategory));
            base.ContextMenu.Items.Add(GetSendCategoriesCtxMenuItem(FoxeCategory));
            base.ContextMenu.Items.Add(GetSendProductsCtxMenuItem(FoxeCategory));
            base.ContextMenu.Items.Add(GetDisplayProductsCtxMenuItem(FoxeCategory));
        }

        private void CreateLeafCategoryHeaderInfra()
        {
            CategoryImage = new Image();
            CategoryImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            CategoryImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            CategoryImage.Width = 20;
            CategoryImage.Height = 20;
            CategoryImage.Margin = new Thickness(2);
            CategoryImage.Source = new BitmapImage(new Uri(@"Resources/Images/categoryImg.jpg", UriKind.RelativeOrAbsolute));

            CategoriesCountTxtBlk = new TextBlock();
            CategoriesCountTxtBlk.Text = "(" + FoxeCategory.ChildCategories.Count() + ")";
            CategoriesCountTxtBlk.Margin = new Thickness(2);
            CategoriesCountTxtBlk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            PagesImage = new Image();
            PagesImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            PagesImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            PagesImage.Width = 20;
            PagesImage.Height = 20;
            PagesImage.Margin = new Thickness(2);
            PagesImage.Source = new BitmapImage(new Uri(@"Resources/Images/pagesImg.jpg", UriKind.RelativeOrAbsolute));

            PagesCountTxtBlk = new TextBlock();
            PagesCountTxtBlk.Text = "(" + FoxeCategory.PageCount + ")";
            PagesCountTxtBlk.Margin = new Thickness(2);
            PagesCountTxtBlk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            StackPanel.Children.Add(CategoryImage);
            StackPanel.Children.Add(CheckBox);
            MainTitleTxtBlk.Text = FoxeCategory.Name;
            StackPanel.Children.Add(MainTitleTxtBlk);

            StackPanel.Children.Add(PagesImage);
            StackPanel.Children.Add(PagesCountTxtBlk);

            StackPanel.Children.Add(ProductsImage);
            ProductsCountTxtBlk.Text = "(" + FoxeCategory.ProductCount + ")";
            StackPanel.Children.Add(ProductsCountTxtBlk);
            this.Header = StackPanel;

            base.ContextMenu = new ContextMenu();
            base.ContextMenu.Items.Add(GetDownloadProductsCtxMenuItem(FoxeCategory));
            base.ContextMenu.Items.Add(GetSendProductsCtxMenuItem(FoxeCategory));
            base.ContextMenu.Items.Add(GetDisplayProductsCtxMenuItem(FoxeCategory));
        }

        protected override void downloadProductsCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FoxeTreeViewNotifications.OnDownloadFoxeProductsUnderCategory(new FoxeDownloadProductsUnderCategoryEventArgs()
            {
                FoxeCategory = this.FoxeCategory
            });

            foreach (FoxePage foxePage in this.FoxeCategory.Pages)
            {
                JobQueue.Enqueue(new Job(foxePage));
            }
        }

        protected override void sendProductsToStoreCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void createIfNotExistCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        protected override void dropCreateDatabaseCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        protected override void displayProductsCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

