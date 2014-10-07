namespace AnimalStore.Common.Configuration
{
  public interface IConfiguration
  {
    string GetNationwideSearchResultsDescriptionMessageForAllBreeds();
    string GetNationwideSearchResultsDescriptionMessageForSpecificBreed();
    string GetNationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace();
    string GetLocalSearchResultsDescriptionMessageForAllBreeds();
    string GetEnvironment();
    int GetSearchResultsMinimumMatchingNumber();
    int GetSearchRadiusDefaultDistanceInMetres();
  }
}