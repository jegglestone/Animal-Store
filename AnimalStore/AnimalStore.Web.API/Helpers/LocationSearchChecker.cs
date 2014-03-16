using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimalStore.Web.API.Helpers
{
    public static class LocationSearchChecker
    {
        public static bool IsLocationSearch(int placeId)
        {
            if (placeId != 0)
                return true;
            return false;
        }

    }
}
