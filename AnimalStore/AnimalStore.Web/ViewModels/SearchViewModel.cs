using System.Web.Mvc;

namespace AnimalStore.Web.ViewModels
{
    public class SearchViewModel
    {
        public SelectList BreedsSelectList { get; set; }
        // public SelectList Locations {get; set; }
        public int SelectedBreed { get; set; }
        public string Location { get; set; }
        public bool IsNationalSearch { get; set; }
        public const string AnyBreed = "Breed (any)";
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
    }
}