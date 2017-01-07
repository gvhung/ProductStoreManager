using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace ServiceDataContracts
{
    [DataContract]
    public class FoxePage
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PageNo { get; set; }

        [DataMember]
        public int DownloadedProductsCount { get; set; }

        [DataMember]
        public int DownloadableProductsCount { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }

        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public virtual FoxeCategory Category { get; set; }

        [DataMember]
        public virtual ICollection<FoxeProduct> Products { get; set; }
    }
}
