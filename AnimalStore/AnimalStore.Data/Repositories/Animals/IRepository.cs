﻿using System;
using System.Linq;

namespace AnimalStore.Data.Repositories.Animals
{
  public interface IRepository<T> : IDisposable 
        where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Detach(T entity);
    }
}
