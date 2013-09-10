using AnimalStore.Model;
using AnimalStore.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AnimalStore.Web.API.Controllers
{
    public class AnimalsController : ApiController
    {
        private readonly IRepository<Animal> _animalRepository;

        public AnimalsController(IRepository<Animal> animalRepository)
        {
            _animalRepository = animalRepository;
        }

        // GET api/animals
        [HttpGet]
        public IEnumerable<Animal> Get()
        {
            var animals = _animalRepository.GetAll()
                .OrderByDescending(a => a.CreatedOn)
                .Take(25);

            return animals;
        }

        // GET api/animals/5
        [HttpGet]
        public Animal Get(int id)
        {
            return _animalRepository.GetById(id);
        }

        // POST api/animals
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/animals/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/animals/5
        [HttpDelete]
        public void Delete(int id)
        {
            _animalRepository.Delete(id);
        }
    }
}
