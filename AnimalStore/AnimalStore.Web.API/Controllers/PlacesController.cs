using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.Filters;

namespace AnimalStore.Web.API.Controllers
{
    public class PlacesController : ApiController, IController<Place>
    {
        private readonly IRepository<Place> _placesRepository;

        public PlacesController(IRepository<Place> placeRepository)
        {
            _placesRepository = placeRepository;
        }

        //
        // GET: /Places/

        [HttpGet]
        public IEnumerable<Place> Get()
        {
            var places = _placesRepository.GetAll().ToList().OrderBy(x => x.Name);

            return places;
        }

        public Place Get(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
