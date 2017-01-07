using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using ServiceDataContracts;
using StoreManager.Common;
using StoreManager.DbProductManager.Core;
using System.Windows.Media.Imaging;

namespace StoreManager.DbProductManager.AppViews
{
    public class RefreshCtxMenuItem : MenuItem
    {

    }

    public class DeleteCtxMenuItem : MenuItem
    {

    }

    public class DbCategoryTreeViewItem : TreeViewItem
    {
        DbTreeViewNotifications _dbTreeViewNotifications;

        CheckBox _checkBox = null;
        TextBlock _textBlock = null;
        TextBlock _countBlock = null;
        StackPanel _stack = null;
        ImageType _imageType;
        Image _image = null;

        Category _category = null;
        bool? _isChecked = false;
        int _count;

        public string Text
        {
            get { return _textBlock.Text; }
        }

        public int Count
        {
            get { return _count; }
        }

        public bool? IsChecked
        {
            get { return _checkBox.IsChecked; }
        }

        public DbCategoryTreeViewItem(Category category, bool? isChecked = false, int count = 0,ImageType imageType = ImageType.None)
        {
            _dbTreeViewNotifications = Global.DbTreeViewNotifications;
            _count = count;
            _isChecked = isChecked;
            _category = category;
            _imageType = imageType;
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

            if (_imageType == ImageType.Database)
            {
                _image = new Image();
                _image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                _image.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                _image.Width = 40;
                _image.Height = 20;
                _image.Margin = new Thickness(2);
                _image.Source = new BitmapImage(new Uri(@"Resources/Images/databaes.jpg", UriKind.RelativeOrAbsolute));
                _stack.Children.Add(_image);
            }

            _textBlock = new TextBlock();
            _textBlock.Text = _category.Name;
            _textBlock.Margin = new Thickness(2);
            _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _stack.Children.Add(_textBlock);

            _countBlock = new TextBlock();
            _countBlock.Text = "(" + _count.ToString() + ")";
            _countBlock.Margin = new Thickness(2);
            _countBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _stack.Children.Add(_countBlock);

            this.Tag = _category;
            this.ContextMenu = GetContextMenu();
            Header = _stack;
        }

        private ContextMenu GetContextMenu()
        {
            RefreshCtxMenuItem refresh = new RefreshCtxMenuItem();
            refresh.Tag = _category;
            refresh.Header = "Refresh";
            refresh.Click += new RoutedEventHandler(refreshCategory_Click);

            DeleteCtxMenuItem delete = new DeleteCtxMenuItem();
            delete.Tag = _category;
            delete.Header = "Delete";
            delete.Click += new RoutedEventHandler(deleteCategory_Click);

            ContextMenu menu = new ContextMenu();
            menu.Items.Add(refresh);
            menu.Items.Add(delete);
            return menu;
        }

        void deleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var deleteCtxMenuItem = e.Source as DeleteCtxMenuItem;
            //To get the Tree from which the context menu is called  
            DbCategoryTreeViewItem dbCategoryTreeViewItem = ((ContextMenu)deleteCtxMenuItem.Parent).PlacementTarget as DbCategoryTreeViewItem;
            _dbTreeViewNotifications.OnDbDeleteCategory(new DbCategoryDeleteEventArgs() { DbCategoryTreeViewItem = dbCategoryTreeViewItem });
        }

        void refreshCategory_Click(object sender, RoutedEventArgs e)
        {
            var refreshCtxMenuItem = e.Source as RefreshCtxMenuItem;
            //To get the Tree from which the context menu is called  
            DbCategoryTreeViewItem dbCategoryTreeViewItem = ((ContextMenu)refreshCtxMenuItem.Parent).PlacementTarget as DbCategoryTreeViewItem;
            _dbTreeViewNotifications.OnDbRefreshCategory(new DbCategoryRefreshEventArgs() { DbCategoryTreeViewItem = dbCategoryTreeViewItem });
        }
    }
}
