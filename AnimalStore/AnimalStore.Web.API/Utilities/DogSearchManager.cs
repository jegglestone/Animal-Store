using System.Collections.Generic;
using System.Linq;
using AnimalStore.Common.Configuration;
using AnimalStore.Model;
using AnimalStore.Web.API.Helpers;
using AnimalStore.Web.API.Strategies;

namespace AnimalStore.Web.API.Utilities
{
  public class DogSearchManager : IDogSearchManager
  {
    private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
    private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
    private readonly IDogCategoryService _dogCategoryService;
    private readonly IDogLocationFilterStrategy _dogLocationFilterStrategy;
    private readonly IConfiguration _configuration;

    public DogSearchManager(
      IDogBreedFilterStrategy dogBreedFilterStrategy
      , IDogCategoryFilterStrategy dogCategoryFilterStrategy
      , IDogCategoryService dogCategoryService
      , IDogLocationFilterStrategy dogLocationFilterStrategy
      , IConfiguration configuration)
    {
      _dogBreedFilterStrategy = dogBreedFilterStrategy;
      _dogCategoryFilterStrategy = dogCategoryFilterStrategy;
      _dogCategoryService = dogCategoryService;
      _dogLocationFilterStrategy = dogLocationFilterStrategy;
      _configuration = configuration;
    }

    public IEnumerable<Dog> GetDogsByBreed(int breedId)
    {
      var matchingDogs = _dogBreedFilterStrategy.Filter(breedId);
      return matchingDogs;
    }

    public IEnumerable<Dog> ApplyDogLocationFilteringAndSorting(IQueryable<Dog> matchingDogs, int breedId, string sortBy, int placeId = 0)
    {
      IQueryable<Dog> dogs = _dogCategoryService.AddDogsInSameCategoryToDogsCollection(matchingDogs, breedId);
      IEnumerable<Dog> dogsSorted;

      if (LocationSearchChecker.IsLocationSearch(placeId))
      {
        dogsSorted = GetDogsInSameRegion(placeId, breedId, dogs);

        dogsSorted = sortBy == null 
          ? _dogLocationFilterStrategy.Sort(dogsSorted) 
          : _dogCategoryFilterStrategy.Sort(dogs, sortBy);
      }
      else
      {
        dogsSorted = _dogCategoryFilterStrategy.Sort(dogs, sortBy);
      }

      return dogsSorted;
    }

    private IEnumerable<Dog> GetDogsInSameRegion(int placeId, int breedId, IQueryable<Dog> dogs)
    {
      var dogsResults = _dogLocationFilterStrategy.Filter(dogs, placeId);

      var dogsInSameRegion = dogsResults as IList<Dog> ?? dogsResults.ToList();
      return dogsInSameRegion.Count(x => x.BreedId == breedId) >= _configuration.GetSearchResultsMinimumMatchingNumber()
        ? dogsInSameRegion.Where(x => x.BreedId == breedId)
        : dogsInSameRegion;
    }
  }
}