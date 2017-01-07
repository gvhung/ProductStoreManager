using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace ServiceDataContracts
{
    [DataContract]
    public class FoxeCategory
    {
        public FoxeCategory()
        {
            Pages = new List<FoxePage>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SEOFriendlyName { get; set; }

        [DataMember]
        public int Weight { get; set; }

        [DataMember]
        public int Rank { get; set; }

        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }

        [DataMember]
        public int PageCount { get; set; }

        [DataMember]
        public int ProductCount { get; set; }

        [DataMember]
        public int FirstPageProductCount { get; set; }

        [DataMember]
        public int LastPageProductCount { get; set; }


        [DataMember]
        public int? ParentCategoryId { get; set; }

        [DataMember]
        public FoxeCategory ParentCategory { get; set; }

        [DataMember]
        public virtual ICollection<FoxeCategory> ChildCategories { get; set; }

        [DataMember]
        public virtual ICollection<FoxeProduct> Products { get; set; }

        [DataMember]
        public virtual ICollection<FoxePage> Pages { get; set; }
    }
}
