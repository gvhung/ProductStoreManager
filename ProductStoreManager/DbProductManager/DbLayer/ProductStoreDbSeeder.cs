using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using ServiceDataContracts;

namespace StoreManager.DbProductManager.DbLayer
{
    public class ProductStoreDbSeeder : DropCreateDatabaseIfModelChanges<ProductStoreDbContext>
    {
        protected override void Seed(ProductStoreDbContext context)
        {
            context.CategoryItems.Add(new Category()
            {
                Name = "ROOT",
                ParentId = -1,
                CreatedOn = DateTime.Now,
                Rank = 0,
                Weight = 0
            });
            base.Seed(context);
        }
    }
}
