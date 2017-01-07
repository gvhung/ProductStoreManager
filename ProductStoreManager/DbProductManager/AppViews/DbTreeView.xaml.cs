using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ServiceDataContracts;
using StoreManager.Common;
using StoreManager.DbProductManager.Core;
using StoreManager.DbProductManager.DbLayer;

namespace StoreManager.DbProductManager.AppViews
{
    /// <summary>
    /// Interaction logic for DbProductTree.xaml
    /// </summary>
    public partial class DbTreeView : Window
    {
        DbCategoryTreeViewItem DbRootTreeViewNode;
        DbTreeViewNotifications dbTreeViewNotifications;
        ProductStoreDbContext dbContext;

        public DbTreeView()
        {
            InitializeComponent();
            //foxEbookDbService = new FoxEbookDbService();
            InitApplication();
        }

        private void InitApplication()
        {
            dbContext = new ProductStoreDbContext();
            DbRootTreeViewNode = CreateRootDbTreeViewNode();
            dbTreeViewNotifications = Global.DbTreeViewNotifications;
            dbTreeViewNotifications.DeleteDbCategory += new EventHandler<DbCategoryDeleteEventArgs>(notifications_DeleteCategory);
            dbTreeViewNotifications.DeleteDbProduct += new EventHandler<DbProductDeleteEventArgs>(notifications_DeleteProduct);
            dbTreeViewNotifications.RefreshDbCategory += new EventHandler<DbCategoryRefreshEventArgs>(notifications_RefreshCategory);
        }

        private DbCategoryTreeViewItem CreateRootDbTreeViewNode()
        {
            Category rootCategory = new Category() { Name = "Database", ParentId = -1 };
            DbRootTreeViewNode = CreateTreeItem(rootCategory);
            dbTreeView.Items.Add(DbRootTreeViewNode);
            return DbRootTreeViewNode;
        }

        private DbCategoryTreeViewItem CreateTreeItem(Category category, bool? isChecked = false, int count = 0)
        {
            DbCategoryTreeViewItem item = new DbCategoryTreeViewItem(category, isChecked, count);
            item.Items.Add(new DbProcessingTreeViewItem());
            return item;
        }


        void notifications_DeleteProduct(object sender, DbProductDeleteEventArgs e)
        {
            DbProductTreeViewItem productTreeViewItem = e.DbProductTreeViewItem;

            //To get the parent of the productTreeViewItem which is a CategoryTreeViewItem 
            DbCategoryTreeViewItem Parent = productTreeViewItem.Parent as DbCategoryTreeViewItem;

            //Removing the TreeItem from Parent 
            Parent.Items.Remove(productTreeViewItem);
        }

        void notifications_DeleteCategory(object sender, DbCategoryDeleteEventArgs e)
        {
            DbCategoryTreeViewItem categoryTreeViewItem = e.DbCategoryTreeViewItem;

            //If Root Node
            if (categoryTreeViewItem.Parent is TreeView)
            {
                categoryTreeViewItem.Items.Clear();
            }

            //If not Root Node
            if (categoryTreeViewItem.Parent is TreeViewItem)
            {
                TreeViewItem parent = (TreeViewItem)categoryTreeViewItem.Parent;
                parent.Items.Remove(categoryTreeViewItem);
            }
        }

        async void notifications_RefreshCategory(object sender, DbCategoryRefreshEventArgs e)
        {
            DbCategoryTreeViewItem categoryTreeViewItem = e.DbCategoryTreeViewItem;
            Category category = categoryTreeViewItem.Tag as Category;
            DbCategoryTreeViewItem parent = categoryTreeViewItem.Parent as DbCategoryTreeViewItem;

            //At the Root Node
            if (parent == null)
            {
                categoryTreeViewItem.Items.Clear();
                categoryTreeViewItem.Items.Add(new DbProcessingTreeViewItem());
                await AddSubCategoriesUnderRootCategory(categoryTreeViewItem);
            }
            else
            {
                int index = parent.Items.IndexOf(categoryTreeViewItem);

                // Get refreshed Category From database
                dbContext.CategoryItems.Include("Products").ToList();
                var refreshedCategory = dbContext.CategoryItems.Where(cat => cat.Name == category.Name).FirstOrDefault();

                // Delete Selected category node as we got refreshed Category From database
                parent.Items.RemoveAt(index);

                // Insert refreshedCategory to treeview and add products under refreshedCategories
                if (refreshedCategory != null)
                {
                    var refreshedCategoryTreeViewItem = CreateTreeItem(refreshedCategory, false, refreshedCategory.Products.Count);
                    parent.Items.Insert(index, refreshedCategoryTreeViewItem);
                    await AddProductsUnderCategory(refreshedCategoryTreeViewItem, refreshedCategory);
                    refreshedCategoryTreeViewItem.IsExpanded = true;
                }
            }
        }

        
        private async void dbTreeView_Expanded(object sender, RoutedEventArgs e)
        {
            DbCategoryTreeViewItem item = e.Source as DbCategoryTreeViewItem;
            if ((item.Items.Count == 1) && (item.Items[0] is DbProcessingTreeViewItem))
            {
                var category = item.Tag as Category;

                //If Root Node
                if (category.ParentId == -1)
                {
                    await AddSubCategoriesUnderRootCategory(item);
                }
                else
                {
                    await AddProductsUnderCategory(item, category);
                }
            }
        }

        private async Task AddSubCategoriesUnderRootCategory(DbCategoryTreeViewItem item)
        {
            var getCategoriesFromDbTask = new Task<IList<Category>>(() =>
            {
                return dbContext.CategoryItems.ToList();
            });
            getCategoriesFromDbTask.Start();
            var categories = await getCategoriesFromDbTask;

            item.Items.Clear();
            foreach (var category in categories.Where(cat => cat.ParentId != -1))
            {
                DbCategoryTreeViewItem categoryTreeviewItem;
                if (category.Products.Count == 0)
                {
                    categoryTreeviewItem = new DbCategoryTreeViewItem(category, false, category.Products.Count);
                }
                else
                {
                    categoryTreeviewItem = CreateTreeItem(category, false, category.Products.Count);
                }
                categoryTreeviewItem.Tag = category;
                DbRootTreeViewNode.Items.Add(categoryTreeviewItem);
            }
        }

        private async Task AddProductsUnderCategory(DbCategoryTreeViewItem item, Category category)
        {
            var synchronizationContext = TaskScheduler.FromCurrentSynchronizationContext();
            var cancellationToken = new CancellationToken();

            await Task.Factory.StartNew(() =>
            {
                item.Items.Clear();
                foreach (var product in category.Products)
                {
                    var treeViewItem = new DbProductTreeViewItem(product, false);
                    treeViewItem.Tag = product;
                    item.Items.Add(treeViewItem);
                }
            }, cancellationToken, TaskCreationOptions.None, synchronizationContext);
        }
    }
}
