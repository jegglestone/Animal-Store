using AnimalStore.Model;

namespace AnimalStore.Web.API.Controllers
{
    public interface IPageableResults<T> where T : Animal
    {
        PageableResults<T> GetPaged(int page = 1, int pageSize = 25);
        PageableResults<T> GetPaged(int breedId, int page, int pageSize, string sortBy = null, int placeId = 0);
    }
}
