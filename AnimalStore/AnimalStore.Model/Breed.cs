using System.Runtime.Serialization;
namespace AnimalStore.Model
{
    [DataContract]
    public class Breed
    {
        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "species")]
        public Species Species { get; set; }

        public Category Category { get; set; }
    }
}
