using System.Globalization;
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

        [DataMember(Name = "breed_id")]
        public int BreedId { get; set; }

        [DataMember(Name = "breed")]
        public Breed Breed { get; set; }

        // This can be private because it's only ever accessed by the serialiser.
        [DataMember(Name = "formatted_created_on_date")]
        private string FormattedCreatedOnDate { get; set; }

        [IgnoreDataMember]
        public DateTime CreatedOn     
        {
            get { return DateTime.ParseExact(FormattedCreatedOnDate, "o", CultureInfo.InvariantCulture); }
            set { FormattedCreatedOnDate = value.ToString("o"); }
        }

        [DataMember(Name = "formatted_modified_on_date")]
        private string FormattedModifiedOnDate { get; set; }

        [IgnoreDataMember]
        public DateTime ModifiedOn
        {
            get { return DateTime.ParseExact(FormattedModifiedOnDate, "o", CultureInfo.InvariantCulture); }
            set { FormattedModifiedOnDate = value.ToString("o"); }
        }
    }
}
