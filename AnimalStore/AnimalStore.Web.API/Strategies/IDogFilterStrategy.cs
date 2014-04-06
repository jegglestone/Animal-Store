using System.Linq;
using AnimalStore.Model;

namespace AnimalStore.Web.API.Strategies
{
    public interface IDogFilterStrategy
    {
        IQueryable<Dog> Filter(int breedId);
    }
}