using System;
using System.Collections;
using System.Collections.Generic;
using DynamicProg.DI;
using NHibernate;
using NHibernate.Criterion;

namespace DynamicProg.NHibernate
{
    public class Repository : IRepository
    {
        #region Fields

        protected readonly ISessionBuilder _sessionBuilder;

        #endregion Fields

        #region Constructors

        public Repository() : this(IocContainer.GetClassInstance<ISessionBuilder>())
        {
        }

        public Repository(ISessionBuilder sessionFactory)
        {
            _sessionBuilder = sessionFactory;
        }

        #endregion Constructors

        #region Protected Methods

        protected ISession GetSession()
        {
            ISession session = _sessionBuilder.GetSession();
            return session;
        }

        #endregion Protected Methods

        #region Public Methods

        public void Flush()
        {
            GetSession().Flush();
        }

        public IEnumerable<Y> GetField<T, Y>(string propertyName)
        {
            var session = GetSession();
            var query = string.Format("Select distinct e.{0} from {1} e", propertyName, typeof (T).Name);
            return session.CreateQuery(query).List<Y>();
        }
        public IList<object[]> GetFields<T>(string[] fieldsName, bool distinct)
        {
            return GetFields<T>(fieldsName, distinct, string.Empty, false);
        }

        public IList<object[]> GetFields<T>(string[] fieldsName, bool distinct, string orderBy, bool descending)
        {
            string distinctClause = string.Empty;
            if (distinct)
            {
                distinctClause = "distinct";
            }
            string orderByClause = string.Empty;
            if (!string.IsNullOrEmpty(orderBy))
            {
                orderByClause = string.Format("Order By e.{0}", orderBy);
                if (descending)
                {
                    orderByClause += " DESC";
                }
            }
            using (ISession session = GetSession())
            {
                string query = string.Format("Select {2} e.{0} from {1} e {3}", String.Join(", e.", fieldsName),
                                             typeof (T).Name, distinctClause, orderByClause);
                return session.CreateQuery(query).List<object[]>();
            }
        }

        public T GetById<T>(object id)
        {
            ISession session = GetSession();
            return session.Get<T>(id);
        }

        public IEnumerable<T> GetAll<T>()
        {
            ISession session = GetSession();
            return session.CreateCriteria(typeof (T)).List<T>() ?? new List<T>();
        }

        public IEnumerable<T> GetAll<T>(ICriterion[] criterions)
        {
            var session = GetSession();
            var criteria = session.CreateCriteria(typeof (T));
            foreach(var criterion in criterions)
            {
                criteria.Add(criterion);
            }
            return criteria.List<T>();
        }

        /// <exception cref="ArgumentNullException"><c>objectToSave</c> is null.</exception>
        public T Save<T,Y>(Y objectToSave) where Y : class
        {
            if (objectToSave == null)
            {
                throw new ArgumentNullException("objectToSave");
            }
            ITransaction tx = null;
            object objectid;
            using (var session = GetSession())
            {
                try
                {
                    tx = session.BeginTransaction();
                    objectid = session.Save(objectToSave);
                    tx.Commit();
                }
                catch (Exception)
                {
                    if (tx != null)
                    {
                        tx.Rollback();
                    }
                    throw;
                }
            }
            return (T) objectid;
        }

        /// <param name="objectToSave">The object to save</param>
        /// <exception cref="System.Exception">Exception is thrown if there is an error</exception>
        public void Save<T>(T objectToSave)
        {
            Save(new[] {objectToSave});
        }

        /// <exception cref="System.Exception">Exception is thrown if there is an error</exception>
        /// <exception cref="ArgumentNullException"><c>objectsToSave</c> is null.</exception>
        public void Save(IEnumerable objectsToSave)
        {
            if (objectsToSave == null)
            {
                throw new ArgumentNullException("objectsToSave");
            }
            ITransaction tx = null;
            using (ISession session = GetSession())
            {
                try
                {
                    tx = session.BeginTransaction();
                    foreach (object o in objectsToSave)
                    {
                        session.Save(o);
                    }
                    tx.Commit();
                }
                catch (Exception)
                {
                    if (tx != null)
                    {
                        tx.Rollback();
                    }
                    throw;
                }
            }
        }

        /// <exception cref="System.Exception">Exception is thrown if there is an error</exception>
        /// <returns>The new id of the object</returns>
        public void Update<T>(T objectToUpdate)
        {
            Update(new[] {objectToUpdate});
        }

        /// <exception cref="System.Exception">Exception is thrown if there is an error</exception>
        /// <exception cref="ArgumentNullException"><c>objectsToUpdate</c> is null.</exception>
        public void Update(IEnumerable objectsToUpdate)
        {
            if (objectsToUpdate == null)
            {
                throw new ArgumentNullException("objectsToUpdate");
            }

            ITransaction tx = null;
            using (ISession session = GetSession())
            {
                try
                {
                    tx = session.BeginTransaction();
                    foreach (object o in objectsToUpdate)
                    {
                        session.Update(o);
                    }
                    tx.Commit();
                }
                catch (Exception)
                {
                    if (tx != null)
                    {
                        tx.Rollback();
                    }

                    throw;
                }
            }
        }

        /// <exception cref="System.Exception">Exception is thrown if there is an error</exception>
        /// <returns>The new id of the object</returns>
        public void Delete<T>(T objectToDelete)
        {
            Delete(new[] {objectToDelete});
        }

        /// <exception cref="System.Exception">Exception is thrown if there is an error</exception>
        /// <exception cref="ArgumentNullException"><c>objectsToDelete</c> is null.</exception>
        public void Delete(IEnumerable objectsToDelete)
        {
            if (objectsToDelete == null)
            {
                throw new ArgumentNullException("objectsToDelete");
            }
            ITransaction tx = null;
            using (ISession session = GetSession())
            {
                try
                {
                    tx = session.BeginTransaction();
                    foreach (object o in objectsToDelete)
                    {
                        session.Delete(o);
                    }
                    tx.Commit();
                }
                catch (Exception)
                {
                    if (tx != null)
                    {
                        tx.Rollback();
                    }
                    throw;
                }
            }
        }

        #endregion Public Methods
    }
}