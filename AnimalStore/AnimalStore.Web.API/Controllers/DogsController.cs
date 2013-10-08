using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AnimalStore.Web.API.Controllers
{
    public class DogsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Dog> _dogsRepository;

        public DogsController(IRepository<Dog> dogsRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dogsRepository = dogsRepository;
        }

        // GET api/dogs
        [HttpGet]
        public IEnumerable<Dog> Get()
        {
            var dogs = _dogsRepository.GetAll()
                .OrderByDescending(a => a.CreatedOn)
                .Take(25);

            return dogs;
        }

        // GET api/dogs/5
        [HttpGet]
        public Dog Get(int id)
        {
            return _dogsRepository.GetById(id);
        }

        // POST api/dogs
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // parse value into Animal entity
            //_animalRepository.Update(value);

            _unitOfWork.Save();
        }

        // PUT api/dogs/5
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
            // parse value into Animal entity
            //_animalRepository.Add(Animal);

            _unitOfWork.Save();
        }

        // DELETE api/dogs/5
        [HttpDelete]
        public void Delete(int id)
        {
            _dogsRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
