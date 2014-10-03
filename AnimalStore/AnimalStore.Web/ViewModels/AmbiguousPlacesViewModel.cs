namespace AnimalStore.Web.ViewModels
{
  using System.Collections.Generic;
  using Model;

  public class AmbiguousLocationViewModel
  {
    public List<Place> Places { get; set; }
    public SearchViewModel SearchViewModel { get; set; }
  }
}