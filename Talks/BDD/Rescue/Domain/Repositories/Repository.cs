using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace Domain.Repositories
{
    public class Repository<TEntity> where TEntity : class, new() 
    {
        string path = @"C:\Development\Code\DynamicProg\Talks\BDD\Rescue\Web\";
        private IList<TEntity> getcollection<TEntity>()
        {
            var filePath = path + typeof(TEntity) + ".jsdb";
            if (File.Exists(filePath))
            {
                return JsonConvert.DeserializeObject<IList<TEntity>>(File.ReadAllText(filePath));
            }
            return new List<TEntity>();
        }
        public void Create(TEntity entity)
        {
            var collection = getcollection<TEntity>();
            collection.Add(entity);
            saveCollection(collection);
        }

        private void saveCollection(IList<TEntity> collection)
        {
            var filePath = path + typeof(TEntity) + ".jsdb";
            File.WriteAllText(filePath, JsonConvert.SerializeObject(collection));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return getcollection<TEntity>();
        }

    }
}
