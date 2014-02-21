namespace AnimalStore.Web.API.Wrappers
{
    public interface IConfiguration
    {
        string GetNationwideSearchResultsDescriptionMessageForAllBreeds();
        string GetNationwideSearchResultsDescriptionMessageForSpecificBreed();
        string GetNationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace();
        int GetSearchResultsMinimumMatchingNumber();
    }
}