using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace ServiceDataContracts
{
    [DataContract]
    public class Category
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ParentId { get; set; }

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
        public virtual IList<Product> Products { get; set; }
    }
}
