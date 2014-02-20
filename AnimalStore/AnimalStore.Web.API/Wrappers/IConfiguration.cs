namespace AnimalStore.Web.API.Wrappers
{
    public interface IConfiguration
    {
        string GetNationwideSearchResultsDescriptionMessageForAllBreeds();
        string GetNationwideSearchResultsDescriptionMessageForSpecificBreed();
        int GetSearchResultsMinimumMatchingNumber();
    }
}