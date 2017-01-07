using System;
using ServiceDataContracts;
using StoreManager.DbProductManager.AppViews;

namespace StoreManager.DbProductManager.Core
{
    public class DbProductDeleteEventArgs : EventArgs
    {
        public DbProductTreeViewItem DbProductTreeViewItem { get; set; }
    }

    public class DbCategoryDeleteEventArgs : EventArgs
    {
        public DbCategoryTreeViewItem DbCategoryTreeViewItem { get; set; }
    }

    public class DbCategoryRefreshEventArgs : EventArgs
    {
        public DbCategoryTreeViewItem DbCategoryTreeViewItem { get; set; }
    }

    public class DbTreeViewNotifications
    {
        public event EventHandler<DbProductDeleteEventArgs> DeleteDbProduct;
        public event EventHandler<DbCategoryDeleteEventArgs> DeleteDbCategory;
        public event EventHandler<DbCategoryRefreshEventArgs> RefreshDbCategory;

        public DbTreeViewNotifications()
        {

        }

        public void OnDbDeleteProduct(DbProductDeleteEventArgs e)
        {
            if (DeleteDbProduct != null)
            {
                DeleteDbProduct(this, e);
            }
        }

        public void OnDbDeleteCategory(DbCategoryDeleteEventArgs e)
        {
            if (DeleteDbCategory != null)
            {
                DeleteDbCategory(this, e);
            }
        }

        public void OnDbRefreshCategory(DbCategoryRefreshEventArgs e)
        {
            if (RefreshDbCategory != null)
            {
                RefreshDbCategory(this, e);
            }
        }
    }
}
