using AnimalStore.Web.API.Models;
using System.Configuration;

namespace AnimalStore.Web.API.Wrappers
{
    public class Configuration : IConfiguration
    {
        private static string nationwideSearchResultsDescriptionMessageForAllBreeds
        {
            get { return ConfigurationManager.AppSettings[Models.AppSettingKeys.NationwideSearchResultsDescriptionMessageForAllBreeds]; }
        }

        private static string nationwideSearchResultsDescriptionMessageForSpecificBreed
        {
            get { return ConfigurationManager.AppSettings[Models.AppSettingKeys.NationwideSearchResultsDescriptionMessageForSpecificBreed]; }
        }

        private static string nationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace
        {
            get { return ConfigurationManager.AppSettings[Models.AppSettingKeys.NationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace]; }
        }

        private static int searchResultsMinimumMatchingNumber
        {
            get { return int.Parse(ConfigurationManager.AppSettings[AppSettingKeys.SearchResultsMinimumMatchingNumber]); }
        }

        private static int searchRadiusDefaultDistanceInMetres
        {
            get { return int.Parse(ConfigurationManager.AppSettings[AppSettingKeys.SearchRadiusDefaultDistanceInMetres]); }
        }

        public string GetNationwideSearchResultsDescriptionMessageForAllBreeds()
        {
            return nationwideSearchResultsDescriptionMessageForAllBreeds;
        }

        public string GetNationwideSearchResultsDescriptionMessageForSpecificBreed()
        {
            return nationwideSearchResultsDescriptionMessageForSpecificBreed;
        }

        public string GetNationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace()
        {
            return nationwideSearchResultsDescriptionMessageForSpecificBreedAndPlace;
        }

        public int GetSearchResultsMinimumMatchingNumber()
        {
            return searchResultsMinimumMatchingNumber;
        }

        public int GetSearchRadiusDefaultDistanceInMetres()
        {
            return searchRadiusDefaultDistanceInMetres;
        }
    }
}