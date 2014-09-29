using AnimalStore.Common.Constants;
namespace AnimalStore.Web.API.Helpers
{
    public static class PageableResultsNextPreviousUrlHelper
    {
        public static string BuildNextPageUrl(string baseUrl, int page, int totalPages, int pageSize, string breedName = null)
        {
            string nextPageUrl = page < totalPages ? baseUrl + (page + 1) + "&pageSize=" + pageSize : "";

            if (breedName != null)
            {
                if (nextPageUrl != string.Empty) nextPageUrl += AppendBreedName(breedName);
            }

            return nextPageUrl;
        }

        public static string BuildPreviousPageUrl(string baseUrl, int page, int totalPages, int pageSize, string breedName = null)
        {
            string prevUrl = page > 1 ? baseUrl + (page - 1) + "&pageSize=" + pageSize : "";

            if (breedName != null)
            {
                if (prevUrl != string.Empty) prevUrl += AppendBreedName(breedName);
            }

            return prevUrl;
        }

        private static string AppendBreedName(string breedName)
        {
            return "&breedName=" + breedName + "&format=" + MediaTypeFormats.Values.JSON; //TODO: can we find out from the request if this should be XML?
        }
    }
}