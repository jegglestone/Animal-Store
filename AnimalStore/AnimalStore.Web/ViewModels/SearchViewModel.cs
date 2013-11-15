using System.Web.Mvc;

namespace AnimalStore.Web.ViewModels
{
    public class SearchViewModel
    {
        public SelectList BreedsSelectList { get; set; }
     // public IList <Location> Locations {get; set; }
        public int SelectedBreed { get; set; }
        public string Location { get; set; }
    }
}