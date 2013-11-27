using AnimalStore.Model;

namespace AnimalStore.Web.API.Controllers
{
    public interface IPageableResults<T>
    {
        PageableResults<T> GetPaged(int page = 1, int pageSize = 25);
    }
}
