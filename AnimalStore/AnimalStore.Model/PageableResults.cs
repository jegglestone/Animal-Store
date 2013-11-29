using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnimalStore.Model
{
    [DataContract]
    public class PageableResults<T>
    {
        [DataMember(Name = "data")]
        public IEnumerable<T> Data { get; set; }

        [DataMember(Name = "id")]
        public int TotalPages { get; set; }

        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }

        [DataMember(Name = "nextPage")]
        public string NextPage { get; set; }

        [DataMember(Name = "prevPage")]
        public string PrevPage { get; set; }
    }
}
