using System.Configuration;
using AnimalStore.Model.Settings;

namespace AnimalStore.Common.Configuration
{
  public class Configuration : IConfiguration
  {
    private static string _nationwideSearchResultsDescriptionMessageForAllBreeds
    {
      get { return ConfigurationManager.AppSettings[AppSettingKeys.NationwideSearchResultsDescriptionMessageForAllBreeds]; }
    }

    private static string _nationwideSearchResultsDescriptionMessageForSpecificBreed
    {
      get { return ConfigurationManager.AppSettings[AppSettingKeys.NationwideSearchResultsDescriptionMessageForSpecificBreed]; }
    }

    private static string _nationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace
    {
      get { return ConfigurationManager.AppSettings[AppSettingKeys.NationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace]; }
    }

    private static int _searchResultsMinimumMatchingNumber
    {
      get { return int.Parse(ConfigurationManager.AppSettings[AppSettingKeys.SearchResultsMinimumMatchingNumber]); }
    }

    private static int _searchRadiusDefaultDistanceInMetres
    {
      get { return int.Parse(ConfigurationManager.AppSettings[AppSettingKeys.SearchRadiusDefaultDistanceInMetres]); }
    }

    private static string _environment
    {
      get { return ConfigurationManager.AppSettings[AppSettingKeys.Environment]; }
    }

    public string GetNationwideSearchResultsDescriptionMessageForAllBreeds()
    {
      return _nationwideSearchResultsDescriptionMessageForAllBreeds;
    }

    public string GetNationwideSearchResultsDescriptionMessageForSpecificBreed()
    {
      return _nationwideSearchResultsDescriptionMessageForSpecificBreed;
    }

    public string GetNationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace()
    {
      return _nationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace;
    }

    public int GetSearchResultsMinimumMatchingNumber()
    {
      return _searchResultsMinimumMatchingNumber;
    }

    public int GetSearchRadiusDefaultDistanceInMetres()
    {
      return _searchRadiusDefaultDistanceInMetres;
    }

    public string GetEnvironment()
    {
      return _environment;
    }
  }
}