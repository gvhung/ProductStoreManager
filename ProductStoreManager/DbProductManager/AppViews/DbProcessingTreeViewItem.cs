using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using ServiceDataContracts;
using System.Windows.Media.Imaging;

namespace StoreManager.DbProductManager.AppViews
{
    public class DbProcessingTreeViewItem : TreeViewItem
    {
        StackPanel _stack = null;
        Image _image = null;

        public DbProcessingTreeViewItem()
        {
            CreateTreeViewItemTemplate();
        }

        private void CreateTreeViewItemTemplate()
        {
            _stack = new StackPanel();
            _stack.Orientation = Orientation.Horizontal;

            _image = new Image();
            _image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            _image.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _image.Width = 40;
            _image.Height = 20;
            _image.Margin = new Thickness(2);
            _image.Source = new BitmapImage(new Uri(@"Resources/Images/processing.png", UriKind.RelativeOrAbsolute));
            _stack.Children.Add(_image);

            Header = _stack;
        }
    }
}
