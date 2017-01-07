using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ServiceDataContracts
{
    [DataContract]
    public class FoxeProduct
    {
        public FoxeProduct()
        {
            UploadDate = DateTime.Now;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CoverPageUrl { get; set; }

        [DataMember]
        public int Edition { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public string ISBN_10 { get; set; }

        [DataMember]
        public string ISBN_13 { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public int Pages { get; set; }

        [DataMember]
        public string Publisher { get; set; }

        [DataMember]
        public DateTime PublishedDate { get; set; }

        [DataMember]
        public DateTime UploadDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public FoxeCategory Category { get; set; }

        [DataMember]
        public int PageId { get; set; }

        [DataMember]
        public FoxePage Page { get; set; }
    }
}
