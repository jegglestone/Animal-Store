namespace AnimalStore.Model
{
  using System.Runtime.Serialization;
  using MongoDB.Bson;

  [DataContract]
  public class Place
  {
    public ObjectId _id { get; set; }

    [DataMember(Name="places_id")]
    public int PlacesID { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "alt_name")]
    public string AltName { get; set; }

    [DataMember(Name = "type")]
    public string Type { get; set; }

    [DataMember(Name = "county")]
    public string County { get; set; }

    [DataMember(Name = "country")]
    public string Country { get; set; }

    [DataMember(Name = "postcode")]
    public string Postcode { get; set; }

    [DataMember(Name = "longitude")]
    public double Longitude { get; set; }

    [DataMember(Name = "latitude")]
    public double Latitude { get; set; }

    public int CountryID { get; set; }
  }
}
