namespace AnimalStore.Web.ViewModels
{
  using Models;
  using System.Web.Mvc;

  public class SearchViewModel
  {
    public SelectList BreedsSelectList { get; set; }
    public int SelectedBreed { get; set; }
    public string Location { get; set; }
    public int PlaceId { get; set; }
    public bool IsNationalSearch { get; set; }
    public const string AnyBreed = "Breed (any)";
    public int PageNumber { get; set; }
    public string SortBy { get; set; }
    public UserProfile UserProfile { get; set; }
  }
}