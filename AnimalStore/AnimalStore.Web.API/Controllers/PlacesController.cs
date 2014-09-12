using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AnimalStore.Data.Repositories.Places;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Controllers
{
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
    public int GetByLocation(string location)
    {
      var places = _placesRepository.GetAll();

      return places.FirstOrDefault(
        x => x.Name == location 
          || x.AltName == location
          || x.County == location
          || x.Postcode == location).PlacesID;
    }
  }
}
