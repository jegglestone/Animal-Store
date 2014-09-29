namespace AnimalStore.Web.API.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Common.Configuration;
  using Helpers;
  using Model;
  using Strategies;

  public class DogSearchService : IDogSearchService
  {
    private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
    private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
    private readonly IDogCategoryService _dogCategoryService;
    private readonly IDogLocationFilter _dogLocationFilterStrategy;
    private readonly IConfiguration _configuration;

    public DogSearchService(
      IDogBreedFilterStrategy dogBreedFilterStrategy
      , IDogCategoryFilterStrategy dogCategoryFilterStrategy
      , IDogCategoryService dogCategoryService
      , IDogLocationFilter dogLocationFilterStrategy
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

    public IEnumerable<Dog> ApplyDogLocationFilteringAndSorting(
      IQueryable<Dog> matchingDogs, int breedId, string sortBy, int placeId = 0)
    {
      IQueryable<Dog> dogs = 
        _dogCategoryService.AddDogsInSameCategoryToDogsCollection(
          matchingDogs, breedId);

      IEnumerable<Dog> dogsSorted = 
        LocationSearchChecker.IsLocationSearch(placeId) 
        ? GetSortedDogsInRegion(breedId, sortBy, placeId, dogs) 
        : _dogCategoryFilterStrategy.Sort(dogs, sortBy);

      return dogsSorted;
    }

    private IEnumerable<Dog> GetSortedDogsInRegion(
      int breedId, string sortBy, int placeId, IQueryable<Dog> dogs)
    {
      IEnumerable<Dog> dogsSorted = 
        GetDogsInSameRegion(placeId, breedId, dogs);

      dogsSorted = ApplyDogsSorting(sortBy, dogsSorted, dogs);
      return dogsSorted;
    }

    private IEnumerable<Dog> ApplyDogsSorting(
      string sortBy, IEnumerable<Dog> dogsSorted, IQueryable<Dog> dogs)
    {
      return sortBy == null 
        ? _dogLocationFilterStrategy.Sort(dogsSorted) 
        : _dogCategoryFilterStrategy.Sort(dogs, sortBy);
    }

    private IEnumerable<Dog> GetDogsInSameRegion(
      int placeId, int breedId, IQueryable<Dog> dogs)
    {
      var dogsResults = _dogLocationFilterStrategy.Filter(dogs, placeId);

      var dogsInSameRegion = dogsResults as IList<Dog> ?? dogsResults.ToList();
      return dogsInSameRegion.Count(x => x.BreedId == breedId) 
        >= _configuration.GetSearchResultsMinimumMatchingNumber()
          ? dogsInSameRegion.Where(x => x.BreedId == breedId)
          : dogsInSameRegion;
    }
  }
}