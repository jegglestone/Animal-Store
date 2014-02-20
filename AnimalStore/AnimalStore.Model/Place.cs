using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Model
{
    public class Place
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public string Type { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public double longitude { get; set; }

        public double Latitude { get; set; }

        public int CountryID { get; set; }
    }
}
