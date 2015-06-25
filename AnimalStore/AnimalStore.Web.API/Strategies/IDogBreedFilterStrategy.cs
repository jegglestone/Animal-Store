namespace AnimalStore.Web.API.Strategies
{
  using System.Linq;
  using Model;

  public interface IDogBreedFilterStrategy : IDogFilterStrategy
  {
      IQueryable<Dog> Filter();
  }
}