using System.Collections.Generic;
using System.Linq.Expressions;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Data.Repositories;
using System.Linq;
using System.Web.Http;
using System;
using AnimalStore.Web.API.Strategies;

namespace AnimalStore.Web.API.Controllers
{
    public class DogsController : ApiController, IPageableResults<Dog>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
        private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
        private readonly IRepository<Dog> _dogsRepository;
        private readonly IRepository<Breed> _breedsRepository;

        public DogsController(IRepository<Dog> dogsRepository, IRepository<Breed> breedsRepository, IUnitOfWork unitOfWork, IDogBreedFilterStrategy dogBreedFilterStrategy, IDogCategoryFilterStrategy dogCategoryFilterStrategy)
        {
            _unitOfWork = unitOfWork;
            _dogBreedFilterStrategy = dogBreedFilterStrategy;
            _dogsRepository = dogsRepository;
            _breedsRepository = breedsRepository;
            _dogCategoryFilterStrategy = dogCategoryFilterStrategy;
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

        //TODO: Unit test. Do something with breedName. Add url tests
        //TODO: What happens if either list is null or empty
        [HttpGet]
        public PageableResults<Dog> GetPaged(int breedId, int page, int pageSize, string breedName = null)
        {
            Expression<Func<Dog, DateTime>> sortExpression = dog => dog.CreatedOn;

            var matchingDogs = _dogBreedFilterStrategy.Filter(breedId, sortExpression);

            var categoryId = _breedsRepository.GetById(breedId).Category.Id;

            var dogsInSameCategory = _dogCategoryFilterStrategy.Filter(categoryId, sortExpression);

            var dogs = matchingDogs.Concat(dogsInSameCategory);
            var baseUrl = "http://localhost:49425/api/Dogs/Breed?breedId=" + breedId + "&page=";

            return GetPageableDogResults(dogs, page, pageSize, baseUrl);
        }

        private static PageableResults<Dog> GetPageableDogResults(IEnumerable<Dog> dogs, int page, int pageSize, string baseUrl)
        {
            IEnumerable<Dog> enumerable = dogs as IList<Dog> ?? dogs.ToList();
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var pagedResults = enumerable.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var nextUrl = page < totalPages - 1 ? baseUrl + (page + 1) + "&pageSize=" + pageSize : "";
            var prevUrl = page > 1 ? baseUrl + (page - 1) + "&pageSize=" + pageSize : "";

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
