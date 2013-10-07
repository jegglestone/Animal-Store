﻿using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;

namespace AnimalStore.Data.Repositories
{
    public class BreedsRepository : GenericRepository<Breed>
    {
        public BreedsRepository(IUnitOfWork unitOfWork) : 
            base(unitOfWork)
        {
        }
    }
}
