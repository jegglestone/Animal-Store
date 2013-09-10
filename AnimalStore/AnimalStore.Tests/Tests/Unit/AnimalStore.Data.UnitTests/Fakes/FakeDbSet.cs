using System.Collections.Generic;
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
