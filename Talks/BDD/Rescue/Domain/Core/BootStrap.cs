using Domain.Model;
using FluentNHibernate.AutoMap;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Domain.Core
{
    public class BootStrap
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                MsSqlConfiguration.MsSql2005
                .ConnectionString(c => c.FromConnectionStringWithKey("ApplicationServices"))
                .ShowSql()
              )
              .Mappings(

              m =>
              {
                  m.FluentMappings
                  .AddFromAssemblyOf<Pet>();

                  m.AutoMappings
                  .Add(AutoPersistenceModel.MapEntitiesFromAssemblyOf<Pet>()
                      .Where(t => t.Namespace.EndsWith("Model")));
              }
                )
              .BuildSessionFactory();

        }
    }
}
