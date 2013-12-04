using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnimalStore.Model
{
    [DataContract]
    public class Category
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        public virtual ICollection<Breed> Breeds { get; set; }
    }
}
