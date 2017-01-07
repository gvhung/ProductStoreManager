using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ServiceDataContracts;
using StoreManager.Common;
using StoreManager.FoxEBookProductManager.Core;
using StoreManager.FoxEBookProductManager.DbLayer;
using StoreManager.FoxEBookProductManager.JobManager;
using StoreManager.LogHandler;

namespace StoreManager.FoxEBookProductManager.AppViews
{
    /// <summary>
    /// Interaction logic for FoxebookTree.xaml
    /// </summary>
    public partial class FoxEBookTreeView : Window
    {
        FoxeCategoryTreeViewItem foxeRootTreeViewNode;
        FoxeTreeViewNotifications foxeTreeViewNotifications;
        FoxEbookDbContext foxeContext;
        JobNotifications jobNotifications;

        public FoxEBookTreeView()
        {
            InitializeComponent();
            InitApplication();
        }

        private void InitApplication()
        {
            LogManager.Init();
            // Program.DatabaseSetup();
            foxeContext = new FoxEbookDbContext();
            foxeContext.Database.Log = Console.Write;
            foxeTreeViewNotifications = Global.FoxeTreeViewNotifications;
            jobNotifications = Global.JobNotifications;
            jobNotifications.JobCompleted += new EventHandler<JobCompletedEventArgs>(jobNotifications_JobCompleted);
            //foxeTreeViewNotifications.DownloadFoxeProducts+=new EventHandler<FoxeDownloadProductsEventArgs>(foxeTreeViewNotifications_DownloadFoxeProducts); 
            foxeTreeViewNotifications.GotoParent += new EventHandler<FoxeGotoParentEventArgs>(foxeTreeViewNotifications_GotoParent);
            foxeRootTreeViewNode = CreateRootFoxeTreeViewNode();
            gvJobsSummary.ItemsSource = JobQueue.JobsSummary;
            gvLog.ItemsSource = LogManager.LogsList;
            Global.StartScheduler();
        }

        void jobNotifications_JobCompleted(object sender, JobCompletedEventArgs e)
        {
            //string msgtext = "Downloaded " + e.FoxeProducts.Count + " Products";
            //string txt = "Status";
            //MessageBoxButton button = MessageBoxButton.YesNoCancel;
            //MessageBoxResult result = MessageBox.Show(msgtext, txt, button); 
            try
            {
                foxeContext.Products.AddRange(e.FoxeProducts);
                foxeContext.SaveChanges();
            }
            catch (Exception exp)
            {
                LogManager.WriteToLog(new LogMessage() { Message = exp.Message });
            }

        }

        //void foxeTreeViewNotifications_DownloadFoxeProducts(object sender, FoxeDownloadProductsEventArgs e)
        //{
        //    if (e is FoxeDownloadProductsUnderCategoryEventArgs)
        //    {
        //        FoxeCategory foxeCategory = (e as FoxeDownloadProductsUnderCategoryEventArgs).FoxeCategory;
        //        DownloadPoroductsUndercategory(foxeCategory);
        //    }
        //    if (e is FoxeDownloadProductsUnderPageEventArgs)
        //    {
        //        FoxePage foxePage = (e as FoxeDownloadProductsUnderPageEventArgs).FoxePage;
        //        DownloadPoroductsUnderPage(foxePage);
        //    }
        //}

        //private void DownloadPoroductsUnderPage(FoxePage foxePage)
        //{
        //    throw new NotImplementedException();
        //}

        //private void DownloadPoroductsUndercategory(FoxeCategory foxeCategory)
        //{
        //    throw new NotImplementedException();
        //}

        void foxeTreeViewNotifications_GotoParent(object sender, FoxeGotoParentEventArgs e)
        {
            var treeViewItem = e.TreeViewItem;
            if (treeViewItem != null)
            {
                TreeViewItem parent = treeViewItem.Parent as TreeViewItem;
                if (parent != null)
                {
                    parent.Focus();
                }
            }
        }

        private FoxeCategoryTreeViewItem CreateRootFoxeTreeViewNode()
        {
            //FoxeCategory rootCategory = foxeContext.CategoryItems.Where(cat => cat.ParentCategory == null).First();
            var query = foxeContext.CategoryItems.Where(cat => cat.ParentCategory == null);
            FoxeCategory rootCategory = query.First();
            foxeRootTreeViewNode = CreateExapndableCategoryTreeItem(rootCategory);
            foxeTreeView.Items.Add(foxeRootTreeViewNode);
            return foxeRootTreeViewNode;
        }


        private FoxeCategoryTreeViewItem CreateExapndableCategoryTreeItem(FoxeCategory foxeCategory)
        {
            FoxeCategoryTreeViewItem item = new FoxeCategoryTreeViewItem(foxeCategory);
            //item.Items.Add(new FoxeProcessingTreeViewItem());
            if (foxeCategory.ChildCategories.Count > 1 || foxeCategory.Pages.Count >= 1)
            {
                item.Items.Add(new FoxeProcessingTreeViewItem());
            }
            return item;
        }

        private FoxePageTreeViewItem CreateExapndablePageTreeItem(FoxePage foxePage)
        {
            FoxePageTreeViewItem item = new FoxePageTreeViewItem(foxePage);
            //item.Items.Add(new FoxeProcessingTreeViewItem());
            if (foxePage.Products.Count >= 1)
            {
                item.Items.Add(new FoxeProcessingTreeViewItem());
            }
            return item;
        }

        private async void foxeTreeView_Expanded(object sender, RoutedEventArgs e)
        {
            if (e.Source is FoxeCategoryTreeViewItem)
            {
                FoxeCategoryTreeViewItem item = e.Source as FoxeCategoryTreeViewItem;
                await UsrExapandedCategoryTreeViewItem(item);
            }

            if (e.Source is FoxePageTreeViewItem)
            {
                FoxePageTreeViewItem item = e.Source as FoxePageTreeViewItem;
                await UsrExapandedPageTreeViewItem(item);
            }
        }

        private async Task UsrExapandedCategoryTreeViewItem(FoxeCategoryTreeViewItem item)
        {
            if ((item.Items.Count == 1) && (item.Items[0] is FoxeProcessingTreeViewItem))
            {
                if (item.Tag is FoxeCategory)
                {
                    var foxeCategory = item.Tag as FoxeCategory;

                    // At Root Category Node
                    if (foxeCategory.IsRoot())
                    {
                        // Expanding Root Category --(should give)--> Child Categories
                        await AddSubCategoriesUnderRootCategory(item, foxeCategory);
                    }

                    //  At Left Category Node
                    if (foxeCategory.IsLeaf())
                    {
                        //Expanding Child Category --(should give)--> Pages
                        await AddPagesUnderSubCategory(item, foxeCategory);
                    }
                }
            }
        }

        private async Task AddSubCategoriesUnderRootCategory(FoxeCategoryTreeViewItem rootCategoryTreeViewItem, FoxeCategory rootFoxeCategory)
        {
            var taskGetSubCategoriesForRootcategory = new Task<IList<FoxeCategory>>(() =>
            {
                return rootFoxeCategory.ChildCategories.ToList();
            });

            taskGetSubCategoriesForRootcategory.Start();
            var foxeSubCategories = await taskGetSubCategoriesForRootcategory;

            rootCategoryTreeViewItem.Items.Clear();

            foreach (var foxeCategory in foxeSubCategories)
            {
                rootCategoryTreeViewItem.Items.Add(CreateExapndableCategoryTreeItem(foxeCategory));
            }
        }

        private async Task AddPagesUnderSubCategory(FoxeCategoryTreeViewItem subCategoryTreeViewItem, FoxeCategory foxeSubCategory)
        {
            var synchronizationContext = TaskScheduler.FromCurrentSynchronizationContext();
            var cancellationToken = new CancellationToken();

            await Task.Factory.StartNew(() =>
            {
                subCategoryTreeViewItem.Items.Clear();
                foreach (FoxePage foxePage in foxeSubCategory.Pages)
                {
                    subCategoryTreeViewItem.Items.Add(CreateExapndablePageTreeItem(foxePage));
                    //var pageTreeViewItem = new FoxePageTreeViewItem(foxePage);
                    //subCategoryTreeViewItem.Tag = foxePage;
                    //subCategoryTreeViewItem.Items.Add(pageTreeViewItem);
                }
            }, cancellationToken, TaskCreationOptions.None, synchronizationContext);
        }

        private async Task UsrExapandedPageTreeViewItem(FoxePageTreeViewItem item)
        {
            if ((item.Items.Count == 1) && (item.Items[0] is FoxeProcessingTreeViewItem))
            {
                if (item.Tag is FoxePage)
                {
                    var foxePage = item.Tag as FoxePage;

                    // Expanding Page --(should give)--> Products
                    await AddProductsUnderPage(item, foxePage);
                }
            }
        }

        private async Task AddProductsUnderPage(FoxePageTreeViewItem pageTreeViewItem, FoxePage foxePage)
        {
            var synchronizationContext = TaskScheduler.FromCurrentSynchronizationContext();
            var cancellationToken = new CancellationToken();

            await Task.Factory.StartNew(() =>
            {
                pageTreeViewItem.Items.Clear();
                foreach (var foxeProduct in foxePage.Products)
                {
                    var productTreeViewItem = new FoxeProductTreeViewItem(foxeProduct);
                    productTreeViewItem.Tag = foxeProduct;
                    pageTreeViewItem.Items.Add(productTreeViewItem);
                }
            }, cancellationToken, TaskCreationOptions.None, synchronizationContext);
        }

        private void btnPauseResume_Checked(object sender, RoutedEventArgs e)
        {
            btnPauseResume.Content = "Resume";
            Global.PauseScheduler();
        }

        private void btnPauseResume_Unchecked(object sender, RoutedEventArgs e)
        {
            btnPauseResume.Content = "Pause";
            Global.ResumeScheduler();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            JobQueue.Clear();
        }

    }
}
