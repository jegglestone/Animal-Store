using System.Runtime.Serialization;
using AnimalStore.Model.Interfaces;
using System;

namespace AnimalStore.Model
{
    [DataContract]
    public class Animal : IAuditInfo
    {
        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "desc")]
        public string Desc { get; set; }

        [DataMember(Name = "ageInYears")]
        public int AgeInYears { get; set; }

        [DataMember(Name = "ageInMonths")]
        public int AgeInMonths { get; set; }

        [DataMember(Name = "isLitter")]
        public bool isLitter { get; set; }

        [DataMember(Name = "isSold")]
        public bool isSold { get; set; }

        [DataMember(Name = "isFemale")]
        public bool isFemale { get; set; }

        [DataMember(Name = "price")]
        public int Price { get; set; }

        [DataMember(Name = "breed")]
        public Breed Breed { get; set; }

        [DataMember(Name = "createdOn")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "modifiedOn")]
        public DateTime ModifiedOn { get; set; }
    }
}
