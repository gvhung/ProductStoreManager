using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using ServiceDataContracts;
using StoreManager.DbProductManager.Core;
using StoreManager.Common;

namespace StoreManager.DbProductManager.AppViews
{
    public class DbProductTreeViewItem : TreeViewItem
    {
        DbTreeViewNotifications _dbTreeViewNotifications;
        CheckBox _checkBox = null;
        TextBlock _textBlock = null;
        StackPanel _stack = null;

        Product _product = null;
        bool? _isChecked = false;

        public string Text
        {
            get { return _textBlock.Text; }
        }

        public bool? IsChecked
        {
            get { return _checkBox.IsChecked; }
        }

        public DbProductTreeViewItem(Product product, bool? isChecked = false)
        {
            _dbTreeViewNotifications = Global.DbTreeViewNotifications;
            _product = product;
            _isChecked = isChecked;
            CreateTreeViewItemTemplate();
        }

        private void CreateTreeViewItemTemplate()
        {
            _stack = new StackPanel();
            _stack.Orientation = Orientation.Horizontal;

            _checkBox = new CheckBox();
            _checkBox.IsChecked = _isChecked;
            _checkBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            _checkBox.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _checkBox.Width = 16;
            _checkBox.Height = 16;
            _checkBox.Margin = new Thickness(2);
            _stack.Children.Add(_checkBox);

            _textBlock = new TextBlock();
            _textBlock.Text = _product.Title;
            _textBlock.Margin = new Thickness(2);
            _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _stack.Children.Add(_textBlock);

            this.ContextMenu = GetContextMenu();
            Header = _stack;
        }

        private ContextMenu GetContextMenu()
        {
            DeleteCtxMenuItem delete = new DeleteCtxMenuItem();
            delete.Header = "Delete";
            delete.Click += new RoutedEventHandler(deleteProduct_Click);
            ContextMenu menu = new ContextMenu();
            menu.Items.Add(delete);
            return menu;
        }

        void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var deleteCtxMenuItem = e.Source as DeleteCtxMenuItem;
            var product = deleteCtxMenuItem.Tag as Product;

            //To get the Tree from which the context menu is called  
            DbProductTreeViewItem dbProductTreeViewItem = ((ContextMenu)deleteCtxMenuItem.Parent).PlacementTarget as DbProductTreeViewItem;

            _dbTreeViewNotifications.OnDbDeleteProduct(new DbProductDeleteEventArgs() { DbProductTreeViewItem = dbProductTreeViewItem });
        }
    }
}
