using System.Collections.Generic;
using System.Linq;
using AnimalStore.Model;
using AnimalStore.Web.API.Controllers;

namespace AnimalStore.Web.Repository
{
    public class InProcSearchRepository : ISearchRepository
    {
        private readonly IController<Breed> _breedsController;

        public InProcSearchRepository(IController<Breed> breedsController)
        {
            _breedsController = breedsController;
        }

        public IList<Breed> GetBreeds()
        {
            return _breedsController.Get().ToList();
        }
    }
}