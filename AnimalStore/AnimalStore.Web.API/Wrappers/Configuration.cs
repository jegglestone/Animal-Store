using AnimalStore.Web.API.Models;
using System.Configuration;

namespace AnimalStore.Web.API.Wrappers
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
    }
}