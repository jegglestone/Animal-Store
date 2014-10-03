namespace AnimalStore.Web.API.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Http;
  using Data.Repositories.Places;
  using Model;

  public class PlacesController : ApiController, IController<Place>
  {
    private readonly IPlacesRepository _placesRepository;

    public PlacesController(IPlacesRepository placeRepository)
    {
      _placesRepository = placeRepository;
    }

    //
    // GET: /Places/
    [HttpGet]
    public IEnumerable<Place> Get()
    {
      var places = _placesRepository
        .GetAll()
        .ToList()
        .OrderBy(x => x.Name);

      return places;
    }

    //
    // GET: /Places/{id}
    [HttpGet]
    public Place Get(int id)
    {
      return _placesRepository
        .GetAll()
        .FirstOrDefault(x => x.PlacesID == id);
    }

    //
    // GET: /Places?location={location}
    [HttpGet]
    public IEnumerable<Place> GetByLocation(string location)
    {
      var places = _placesRepository.GetAll().Where(
        x => String.Equals(x.Name, location, StringComparison.CurrentCultureIgnoreCase)
             || String.Equals(x.AltName, location, StringComparison.CurrentCultureIgnoreCase)
             || String.Equals(x.County, location, StringComparison.CurrentCultureIgnoreCase)
             || String.Equals(x.Postcode, location, StringComparison.CurrentCultureIgnoreCase)
             || x.Postcode.Contains(location));

      return places;
    }
  }
}

