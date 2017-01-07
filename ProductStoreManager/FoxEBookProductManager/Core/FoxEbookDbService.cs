using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ServiceDataContracts;
using StoreManager.FoxEBookProductManager.DbLayer;

namespace StoreManager.FoxEBookProductManager.Core
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class FoxEbookDbService : IFoxEbookDbService
    {
        FoxEbookDbContext dbcontext;
        public FoxEbookDbService(bool LazyLoading = true)
        {
            Database.SetInitializer<FoxEbookDbContext>(new FoxEbookDbSeeder());
            dbcontext = new FoxEbookDbContext();

            if (!LazyLoading)
                dbcontext.CategoryItems.Include(cat => cat.Products).ToList();
        }



        public IList<FoxeCategory> GetCategoryMenuItems()
        {
            return dbcontext.CategoryItems.ToList();
        }

        public IList<FoxeSort> GetSortMenuItems()
        {
            return dbcontext.SortItems.ToList();
        }

        public void AddCategoryMenuItems(IList<FoxeCategory> categoryItems)
        {
            foreach (FoxeCategory categoryItem in categoryItems)
            {
                AddCategoryMenuItem(categoryItem);
            }
        }

        public void AddSortMenuItems(IList<FoxeSort> sortItems)
        {
            foreach (FoxeSort sortItem in sortItems)
            {
                try
                {
                    dbcontext.SortItems.Add(sortItem);
                    dbcontext.SaveChanges();
                }
                catch (Exception e)
                {
                    // LOG ERROR
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void AddProducts(IList<FoxeProduct> products)
        {
            foreach (FoxeProduct product in products)
            {
                try
                {
                    dbcontext.Products.Add(product);
                    dbcontext.SaveChanges();
                }
                catch (Exception e)
                {
                    // LOG ERROR
                    Console.WriteLine(e.ToString());
                }
            }
        }


        public void AddCategoryMenuItem(FoxeCategory categoryItem)
        {
            try
            {
                dbcontext.CategoryItems.Add(categoryItem);
                dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                // LOG ERROR
                Console.WriteLine(e.ToString());
            }
        }
    }
}
