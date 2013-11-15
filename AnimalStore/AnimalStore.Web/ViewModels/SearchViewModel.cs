using System.Collections.Generic;
using System.Web.Mvc;
using AnimalStore.Model;

namespace AnimalStore.Web.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Breed> Breeds { get; set; }
     // public IList <Location> Locations {get; set; }

        public SelectList BreedSelectList { get; set; }
    }
}