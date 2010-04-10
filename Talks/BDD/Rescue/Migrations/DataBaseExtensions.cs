using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Migrator.Framework;

namespace Migrations
{
    public static class DataBaseExtensions
    {
        public static void RemoveTableFor<TEntity>(this ITransformationProvider dataBase) where TEntity : class
        {
            var tableName = typeof (TEntity).Name;
            dataBase.RemoveTable(tableName);
        }
        public static void AddTableFor<TEntity>(this ITransformationProvider dataBase) where TEntity : class
        {
            dataBase.AddTableFor<TEntity>(new List<Column>());
        }

        public static void AddTableFor<TEntity>(this ITransformationProvider dataBase, IList<Column> columns) where TEntity : class
        {
            var entity = typeof(TEntity);
            var tableName = entity.Name;
            var propertiesInfo = entity.GetProperties();
            var thisColumns = new List<Column>();
            thisColumns.AddRange(columns);
            foreach (var propertyInfo in propertiesInfo)
            {
                if (ColumnUndefined(thisColumns, propertyInfo))
                {
                    AddColumn(thisColumns, propertyInfo);                 
                }
            }
            dataBase.AddTable(tableName, thisColumns.ToArray());
        }

        private static void AddColumn(IList<Column> columns, PropertyInfo propertyInfo)
        {
            if (propertyInfo.Name.ToLower().Equals("id"))
            {
                columns.Add(new Column(propertyInfo.Name, DbType.String, ColumnProperty.PrimaryKeyWithIdentity));
            }
            else
            {
                columns.Add(new Column(propertyInfo.Name, DbType.String));
            }
        }

        private static bool ColumnUndefined(IEnumerable<Column> columns, PropertyInfo propertyInfo)
        {
            return columns.First(c=>c.Name == propertyInfo.Name) == null;
        }
    }
}