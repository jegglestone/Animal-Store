using MongoDB.Bson;

namespace AnimalStore.Model
{
    public class Place
    {
        public ObjectId _id { get; set; }

        public int PlacesID { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public string Type { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int CountryID { get; set; }
    }
}
