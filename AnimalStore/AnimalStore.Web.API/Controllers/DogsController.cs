using System.Collections.Generic;
using System.Configuration;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Data.Repositories;
using System.Linq;
using System.Web.Http;
using System;
using AnimalStore.Web.API.Filters;
using AnimalStore.Web.API.Helpers;
using AnimalStore.Web.API.Models;
using AnimalStore.Web.API.Wrappers;

namespace AnimalStore.Web.API.Controllers
{
    [NullFilter]
    public class DogsController : ApiController, IPageableResults<Dog>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Dog> _dogsRepository;
        private readonly IRepository<Breed> _breedsRepository;
        private readonly IDogSearchHelper _dogSearchHelper;
        private readonly IConfiguration _configuration;

        public DogsController(IRepository<Dog> dogsRepository, IRepository<Breed> breedsRepository, IUnitOfWork unitOfWork, IDogSearchHelper dogSearchHelper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _dogsRepository = dogsRepository;
            _dogSearchHelper = dogSearchHelper;
            _breedsRepository = breedsRepository;
            _configuration = configuration;
        }

        // GET api/dogs
        [HttpGet]
        public PageableResults<Dog> GetPaged(int page = 1, int pageSize = 25)
        {
            var dogs = _dogsRepository.GetAll()
                .OrderByDescending(a => a.CreatedOn);

            var baseUrl = ConfigurationManager.AppSettings[AppSettingKeys.BaseUrlPagedDogs] + "?page=";

            return GetPageableDogResults(dogs, page, pageSize, baseUrl);
        }

        // GET api/Dogs/Breed/
        [HttpGet]
        public PageableResults<Dog> GetPaged(int breedId, int page, int pageSize, string sortBy = null)
        {
            var sortedDogsList = _dogSearchHelper.GetSortedDogsList(breedId, sortBy);

            var baseUrl = ConfigurationManager.AppSettings[AppSettingKeys.BaseUrlPagedDogsByBreed] + "?breedId=" + breedId + "&page=";

            var breedName = _breedsRepository.GetById(breedId).Name;

            return GetPageableDogResults(sortedDogsList, page, pageSize, baseUrl, breedName);
        }

        private PageableResults<Dog> GetPageableDogResults(IEnumerable<Dog> dogs, int page, int pageSize, string baseUrl, string breedName = null)
        {
            IEnumerable<Dog> enumerable = dogs as IList<Dog> ?? dogs.ToList();
            var totalCount = enumerable.Count();

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var pagedResults = enumerable.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var nextUrl = PageableResultsNextPreviousUrlHelper.BuildNextPageUrl(baseUrl, page, totalPages, pageSize, breedName);
            var prevUrl = PageableResultsNextPreviousUrlHelper.BuildPreviousPageUrl(baseUrl, page, totalPages, pageSize, breedName);

            var resultsFrom = ResultsCountHelper.GetResultsFrom(page, pageSize);
            var resultsTo = ResultsCountHelper.GetResultsTo(totalCount, totalPages, page, pageSize);

            var resultsDescription = breedName != null
                ? string.Format(_configuration.GetNationwideSearchResultsDescriptionMessageForSpecificBreed(), resultsFrom, resultsTo, totalCount, breedName)
                : string.Format(_configuration.GetNationwideSearchResultsDescriptionMessageForAllBreeds(), resultsFrom, resultsTo, totalCount);

            return new PageableResults<Dog>
            {
                Data = pagedResults,
                SearchDescription = resultsDescription,
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
            var dog = _dogsRepository.GetById(id);
            dog.Breed = _breedsRepository.GetById(dog.BreedId);

            return dog;
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