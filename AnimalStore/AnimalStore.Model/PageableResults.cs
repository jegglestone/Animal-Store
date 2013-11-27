using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnimalStore.Model
{
    [DataContract]
    public class PageableResults<T>
    {
        [DataMember]
        public IEnumerable<T> Data { get; set; }

        [DataMember]
        public int TotalPages { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public string NextPage { get; set; }

        [DataMember]
        public string PrevPage { get; set; }
    }
}
