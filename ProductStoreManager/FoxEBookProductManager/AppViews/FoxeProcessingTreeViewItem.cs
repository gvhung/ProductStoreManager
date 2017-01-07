using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using ServiceDataContracts;
using System.Windows.Media.Imaging;

namespace StoreManager.FoxEBookProductManager.AppViews
{
    public class FoxeProcessingTreeViewItem : BaseTreeViewItem
    {
        protected Image ProcessingImage { get; set; }
        protected TextBlock ProcessingTxtBlk { get; set; }
        public FoxeProcessingTreeViewItem()
        {
            CreateTreeViewItemTemplate();
        }

        private void CreateTreeViewItemTemplate()
        {
            base.StackPanel.Children.Clear();
            ProcessingTxtBlk = new TextBlock();
            ProcessingTxtBlk.Text = "Processing ...";
            ProcessingTxtBlk.Margin = new Thickness(2);
            ProcessingTxtBlk.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            base.StackPanel.Children.Add(ProcessingTxtBlk);

            ProcessingImage = new Image();
            ProcessingImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ProcessingImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            ProcessingImage.Width = 40;
            ProcessingImage.Height = 20;
            ProcessingImage.Margin = new Thickness(2);
            ProcessingImage.Source = new BitmapImage(new Uri(@"Resources/Images/processing.png", UriKind.RelativeOrAbsolute));
            base.StackPanel.Children.Add(ProcessingImage);

            Header = base.StackPanel;
        }
    }
}
