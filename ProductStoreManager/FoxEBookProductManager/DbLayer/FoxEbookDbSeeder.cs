using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using ServiceDataContracts;

namespace StoreManager.FoxEBookProductManager.DbLayer
{
    public class FoxEbookDbSeeder : DropCreateDatabaseIfModelChanges<FoxEbookDbContext>
    {
        protected override void Seed(FoxEbookDbContext context)
        {
            
            base.Seed(context);
        }
    }
}
