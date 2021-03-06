﻿using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;

namespace AnimalStore.Data.Repositories.Animals
{
    public class DogsRepository : GenericRepository<Dog>
    {
        public DogsRepository(IUnitOfWork unitOfWork) :
            base(unitOfWork)
        {
        }
    }
}