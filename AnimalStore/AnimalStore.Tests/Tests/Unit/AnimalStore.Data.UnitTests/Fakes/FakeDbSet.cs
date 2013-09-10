<<<<<<< HEAD
﻿using System.Collections.Generic;
using System.Data.Entity;
using AnimalStore.Model;

namespace AnimalStore.Data.UnitTests.Fakes
{
    public abstract class FakeDbSet<T> : IDbSet<T>
        where T : class, new()
    {
        public List<T> _collection { get; set; }

        public T Add(T entity)
        {
            throw new System.NotImplementedException();
        }

        public T Attach(T entity)
        {
            throw new System.NotImplementedException();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new System.NotImplementedException();
        }

        public T Create()
        {
            throw new System.NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get { throw new System.NotImplementedException(); }
        }

        public T Remove(T entity)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public System.Type ElementType
        {
            get { throw new System.NotImplementedException(); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { throw new System.NotImplementedException(); }
        }

        public System.Linq.IQueryProvider Provider
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
=======
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AnimalStore.Data.UnitTests.Fakes
{
    /// <summary>
    /// A fake DbSet allowing us to create and maintain a collection for testing our Repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FakeDbSet<T> : IDbSet<T>
        where T : class, new()
    {
        readonly ObservableCollection<T> _items;
        readonly IQueryable _query;

        public FakeDbSet()
        {
            _items = new ObservableCollection<T>();
            _query = _items.AsQueryable();
        }

        public T Add(T entity)
        {
            _items.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            _items.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return System.Activator.CreateInstance<TDerivedEntity>();
        }

        public T Create()
        {
            return new T();
        }

        public abstract T Find(params object[] keyValues);

        public ObservableCollection<T> Local
        {
            get { return _items; }
        }

        public T Remove(T entity)
        {
            _items.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

       IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public Type ElementType
        {
            get { return _query.ElementType; }
        }

        public Expression Expression
        {
            get { return _query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _query.Provider; }
        }
    }
}
>>>>>>> 8ef14f536d796b1ef223fe3dd19e0d9143aae71d
