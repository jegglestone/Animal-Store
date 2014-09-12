using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AnimalStore.Data.Repositories.Animals;
using AnimalStore.Model;
using AnimalStore.Web.API.Filters;

namespace AnimalStore.Web.API.Controllers
{
    [NullFilter]
    public class BreedsController : ApiController, IController<Breed>
    {
        private readonly IRepository<Breed> _breedsRepository;

        public BreedsController(IRepository<Breed> breedRepository)
        {
            _breedsRepository = breedRepository;
        }

        // GET api/Breeds/
        [HttpGet]
        public IEnumerable<Breed> Get()
        {
            var breeds = _breedsRepository.GetAll().ToList().OrderByDescending(x => x.Name).Reverse();

            return breeds;
        }

        // GET api/Breeds/5
        [HttpGet]
        public Breed Get(int id)
        {
            return _breedsRepository.GetById(id);
        }
    }
}
