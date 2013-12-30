using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnimalStore.Model
{
    [DataContract]
    public class PageableResults<T> 
        where T : Animal
    {
        [DataMember(Name = "data")]
        public IEnumerable<T> Data { get; set; }

        [DataMember(Name = "total_pages")]
        public int TotalPages { get; set; }

        [DataMember(Name = "search_description")]
        public string SearchDescription { get; set; }

        [DataMember(Name = "total_count")]
        public int TotalCount { get; set; }

        [DataMember(Name = "next_page")]
        public string NextPage { get; set; }

        [DataMember(Name = "prev_page")]
        public string PrevPage { get; set; }

        [DataMember(Name = "current_page_number")]
        public int CurrentPageNumber { get; set; }
    }
}
