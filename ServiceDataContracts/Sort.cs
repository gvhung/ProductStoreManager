using System.Runtime.Serialization;
using System;

namespace ServiceDataContracts
{
    [DataContract]
    public class Sort
    {
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SEOFriendlyName { get; set; }

        [DataMember]
        public int Rank { get; set; }

        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public int Weight { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }
    }
}