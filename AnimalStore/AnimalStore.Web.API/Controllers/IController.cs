using System.Collections.Generic;

namespace AnimalStore.Web.API.Controllers
{
    public interface IController<out T>
    {
        IEnumerable<T> Get();
        T Get(int id);
    }
}
