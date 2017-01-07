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

namespace StoreManager.FoxEBookProductManager.AppViews
{
    public class FoxeProductTreeViewItem : BaseTreeViewItem
    {
        public FoxeProduct FoxeProduct;
        public FoxeProductTreeViewItem(FoxeProduct foxeProduct)
        {
            this.FoxeProduct = foxeProduct;
            this.Tag = foxeProduct;
            CreatePageTreeViewItem();
        }
        private void CreatePageTreeViewItem()
        {
            CreateHeader();
            CreateContextMenu();
        }

        private void CreateHeader()
        {
            StackPanel.Children.Add(ProductsImage);
            MainTitleTxtBlk.Text = FoxeProduct.Title;
            StackPanel.Children.Add(MainTitleTxtBlk);
            this.Header = StackPanel;
        }

        private void CreateContextMenu()
        {
            base.ContextMenu = new ContextMenu();
            base.ContextMenu.Items.Add(GetDisplayProductCtxMenuItem(FoxeProduct));
        }

        protected override void displayProductCtxMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
