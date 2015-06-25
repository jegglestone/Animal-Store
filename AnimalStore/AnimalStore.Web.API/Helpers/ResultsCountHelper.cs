namespace AnimalStore.Web.API.Helpers
{
    public static class ResultsCountHelper
    {
        public static int GetResultsFrom(
          int currentPage, int pageSizeLimit, int numberOfResults)
        {
            if (currentPage == 1)
            {
                if (numberOfResults == 0)
                    return 0;
                return 1;
            }

            return ((pageSizeLimit) * (currentPage - 1)) + 1;
        }

        public static int GetResultsTo(
          int totalRecords, int numberOfPages, int currentPage, int pageSizeLimit)
        {
            if (currentPage == 1)
            {
                if (totalRecords < pageSizeLimit)
                    return totalRecords;
                return pageSizeLimit;                
            }

            if (currentPage == numberOfPages)
            {
                if (totalRecords < (numberOfPages*pageSizeLimit))
                {
                    var remainder = totalRecords%pageSizeLimit;
                    return (numberOfPages - 1)*(pageSizeLimit) + remainder;
                }
            }

            return currentPage*pageSizeLimit;
        }
    }
}