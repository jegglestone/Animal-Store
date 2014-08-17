namespace AnimalStore.Common.Configuration
{
  public interface IConfiguration
  {
    string GetNationwideSearchResultsDescriptionMessageForAllBreeds();
    string GetNationwideSearchResultsDescriptionMessageForSpecificBreed();
    string GetNationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace();
    string GetEnvironment();
    int GetSearchResultsMinimumMatchingNumber();
    int GetSearchRadiusDefaultDistanceInMetres();
  }
}