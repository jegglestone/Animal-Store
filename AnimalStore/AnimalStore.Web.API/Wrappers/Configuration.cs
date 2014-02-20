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

        private static int searchResultsMinimumMatchingNumber
        {
            get { return int.Parse(ConfigurationManager.AppSettings[AppSettingKeys.SearchResultsMinimumMatchingNumber]); }
        }


        public string GetNationwideSearchResultsDescriptionMessageForAllBreeds()
        {
            return nationwideSearchResultsDescriptionMessageForAllBreeds;
        }

        public string GetNationwideSearchResultsDescriptionMessageForSpecificBreed()
        {
            return nationwideSearchResultsDescriptionMessageForSpecificBreed;
        }

        public int GetSearchResultsMinimumMatchingNumber()
        {
            return searchResultsMinimumMatchingNumber;
        }
    }
}