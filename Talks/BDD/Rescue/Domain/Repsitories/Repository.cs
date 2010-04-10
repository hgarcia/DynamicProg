using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;

namespace Domain.Repsitories
{
    public class Repository<T> where T : class
    {
        protected readonly ISession Session;

        public Repository(ISession session)
        {
            Session = session;
        }

        public virtual void Save(T item)
        {
            Save(new[] { item });
        }

        public virtual void Save(IEnumerable items)
        {
            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    foreach (var item in items)
                    {
                        Session.Update(item);
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public virtual T GetById(object id)
        {
            using (Session.BeginTransaction())
            {
                return Session.Get<T>(id);
            }
        }

        public virtual int Add(T item)
        {
            return Add(new[] { item })[0];
        }

        public virtual int[] Add(IEnumerable items)
        {
            var ids = new List<int>();
            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    foreach (var item in items)
                    {
                        ids.Add((int)Session.Save(item));
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return ids.ToArray();
        }

        public virtual void Delete(T item)
        {
            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    Session.Delete(item);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}