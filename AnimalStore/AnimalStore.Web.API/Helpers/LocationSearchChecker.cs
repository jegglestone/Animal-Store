namespace AnimalStore.Web.API.Helpers
{
  public static class LocationSearchChecker
  {
    public static bool IsLocationSearch(int placeId)
    {
      return placeId != 0;
    }
  }
}
