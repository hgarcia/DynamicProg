using System.Collections.Generic;
using System.Collections;
using NHibernate.Criterion;

namespace DynamicProg.NHibernate
{
    public interface IRepository
    {
        void Flush();
        IEnumerable<Y> GetField<T, Y>(string propertyName);
        IList<object[]> GetFields<T>(string[] fieldsName, bool distinct);
        IList<object[]> GetFields<T>(string[] fieldsName, bool distinct, string orderBy, bool descending);
        T GetById<T>(object id);
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(ICriterion[] criteria);

        T Save<T, Y>(Y objectToSave) where Y : class;

        void Save<T>(T objectToSave);

        void Save(IEnumerable objectsToSave);

        void Update<T>(T objectToUpdate);

        void Update(IEnumerable objectsToUpdate);

        void Delete<T>(T objectToDelete);

        void Delete(IEnumerable objectsToDelete);
    }
}