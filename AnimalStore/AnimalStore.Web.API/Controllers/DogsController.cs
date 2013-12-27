using System.Collections.Generic;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Data.Repositories;
using System.Linq;
using System.Web.Http;
using System;
using AnimalStore.Web.API.Filters;
using AnimalStore.Web.API.Helpers;

namespace AnimalStore.Web.API.Controllers
{
    [NullFilter]
    public class DogsController : ApiController, IPageableResults<Dog>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Dog> _dogsRepository;
        private readonly IDogSearchHelper _dogSearchHelper;

        public DogsController(IRepository<Dog> dogsRepository, IUnitOfWork unitOfWork, IDogSearchHelper dogSearchHelper)
        {
            _unitOfWork = unitOfWork;
            _dogsRepository = dogsRepository;
            _dogSearchHelper = dogSearchHelper;
        }

        // GET api/dogs
        [HttpGet]
        public PageableResults<Dog> GetPaged(int page = 1, int pageSize = 25)
        {
            var dogs = _dogsRepository.GetAll()
                .OrderByDescending(a => a.CreatedOn);

            const string baseUrl = "http://localhost:49425/api/dogs?page=";

            return GetPageableDogResults(dogs, page, pageSize, baseUrl);
        }

        // GET api/Dogs/Breed/
        [HttpGet]
        public PageableResults<Dog> GetPaged(int breedId, int page, int pageSize, string breedName = null, string sortBy = null)
        {
            var sortedDogsList = _dogSearchHelper.GetSortedDogsList(breedId, sortBy);

            var baseUrl = "http://localhost:49425/api/Dogs/Breed?breedId=" + breedId + "&page=";

            return GetPageableDogResults(sortedDogsList, page, pageSize, baseUrl, breedName);
        }

        // one failing test - this needs to handle nulls
        private static PageableResults<Dog> GetPageableDogResults(IEnumerable<Dog> dogs, int page, int pageSize, string baseUrl, string breedName = null)
        {
            IEnumerable<Dog> enumerable = dogs as IList<Dog> ?? dogs.ToList();
            var totalCount = enumerable.Count();

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var pagedResults = enumerable.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var nextUrl = page < totalPages - 1 ? baseUrl + (page + 1) + "&pageSize=" + pageSize : "";
            var prevUrl = page > 1 ? baseUrl + (page - 1) + "&pageSize=" + pageSize : "";

            var resultsDescription = breedName != null ? "Results for " + breedName : "Search results";

            return new PageableResults<Dog>
            {
                Data = pagedResults,
                Description = resultsDescription,
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
        public void Post([FromBody] Dog dog)
        {
            // parse value into Animal entity
            //_animalRepository.Update(value);

            _unitOfWork.Save();
        }

        // PUT api/dogs/5
        [HttpPut]
        public void Put(int id, [FromBody] Dog dog)
        {
            // update dog

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
