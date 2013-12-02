using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Data.Repositories;
using System.Linq;
using System.Web.Http;
using System;

namespace AnimalStore.Web.API.Controllers
{
    public class DogsController : ApiController, IPageableResults<Dog>
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
        public PageableResults<Dog> GetPaged(int page = 1, int pageSize = 25)
        {
            var dogs = _dogsRepository.GetAll()
                .OrderByDescending(a => a.CreatedOn);

            var totalCount = dogs.Count();
            var totalPages = (int)Math.Ceiling((double) totalCount / pageSize);

            var pagedResults = dogs.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var nextUrl = page < totalPages - 1 ? "http://localhost:49425/api/dogs?page=" + (page + 1) + "&pageSize=" + pageSize : "";
            var prevUrl = page > 1 ? "http://localhost:49425/api/dogs?page=" + (page - 1) + "&pageSize=" + pageSize : "";

            return new PageableResults<Dog> { 
                Data = pagedResults,
                NextPage = nextUrl,
                PrevPage = prevUrl,
                CurrentPageNumber = page,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        // GET api/Dogs/Breed/

        //TODO: Unit test. Do something with breedName
        [HttpGet]
        public PageableResults<Dog> GetPaged(int breedId, int page, int pageSize, string breedName = null)
        {
            var dogs = _dogsRepository.GetAll()
                .Where(x => x.Breed.Id == breedId)
                .OrderByDescending(a => a.CreatedOn);

            var totalCount = dogs.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var pagedResults = dogs.Skip((page - 1) * pageSize)
                .Take(pageSize);

            // TODO: page urls
            var nextUrl = "";
            var prevUrl = "";

            return new PageableResults<Dog>
            {
                Data = pagedResults,
                NextPage = nextUrl,
                PrevPage = prevUrl,
                CurrentPageNumber = page,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
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
