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

        [DataMember(Name="created_by_user_id")]
        public int CreatedByUsedId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "headline")]
        public string Headline { get; set; }

        [DataMember(Name = "full_description")]
        public string FullDescription { get; set; }

        [DataMember(Name = "age_in_years")]
        public int AgeInYears { get; set; }

        [DataMember(Name = "age_in_months")]
        public int AgeInMonths { get; set; }

        [DataMember(Name = "is_litter")]
        public bool isLitter { get; set; }

        [DataMember(Name = "is_sold")]
        public bool isSold { get; set; }

        [DataMember(Name = "is_female")]
        public bool isFemale { get; set; }

        [DataMember(Name = "price")]
        public int Price { get; set; }

        [DataMember(Name = "breed")]
        public Breed Breed { get; set; }

        [IgnoreDataMember]
        public DateTime CreatedOn { get; set; }

        [IgnoreDataMember]
        public DateTime ModifiedOn { get; set; }        
    }
}
