using System;
using System.Runtime.Serialization;
using DynamicProg.Data;
using DynamicProg.Logging;
using LaTrompa;

namespace DynamicProg.Caching
{
    ///<summary>
    /// An implementation of <see cref="ICache"/> that saves objects to the database
    /// Used mostly to store strings, if you want to save an actual this object needs to implement <see cref="ISerializable"/>.
    ///</summary>
    public class DbCache : ICache
    {
        private static string _tableName = "Table";

        private string _simpleInsertQuery;
        private string _complexInsertQuery;
        private string _selectQuery;
        private string _removeQuery;
        private string _clearQuery;
        private string _renewQuery;

        private readonly Caching_db_cache _configReader;
        private readonly ILogger _logger;
        private readonly SqlDb _db;

        private void SetupQueries()
        {
            _renewQuery = string.Format(@"UPDATE {0} SET SecondsToExpire = @secondsToExpire WHERE [Key] = @key", _tableName);
            _clearQuery = string.Format(@"DELETE FROM {0}", _tableName);
            _removeQuery = string.Format(@"DELETE FROM {0} WHERE [Key] = @key", _tableName);
            _selectQuery = string.Format(@"SELECT [Value], [Created], SecondsToExpire, Rolling FROM {0} WHERE [Key] = @key", _tableName);
            _complexInsertQuery = string.Format(@"INSERT INTO {0} ([Key], [Value], SecondsToExpire, Rolling) VALUES (@key, @value, @secondsToExpire, @rolling)", _tableName);
            _simpleInsertQuery = string.Format(@"INSERT INTO {0} ([Key], [Value], SecondsToExpire, Rolling) VALUES (@key, @value, null, 0)", _tableName);
        }

        private bool IsDisabled()
        {
            if (_configReader.IsEnabled)
            {
                return false;
            }
            _logger.LogWarning("DbCache is disabled");
            return true;
        }

        ///<summary>
        ///Constructor
        ///</summary>
        public DbCache(ILogger logger)
        {
            _configReader = CachingConfiguration.GetConfig().DbCache;
            if (IsDisabled())
            {
                return;
            }
            _tableName = _configReader.TableName;
            _logger = logger;
            SetupQueries();

            _db = String.IsNullOrEmpty(_configReader.ConnectionString) ? 
                new SqlDb(Utils.GetConnectionString("ConnectionString")) : 
                new SqlDb(_configReader.ConnectionString);
        }
        /// <summary>
        /// Adds the .ToString() method of an object to the cache without any dependency or expiration date
        /// </summary>
        /// <param name="key">The key for the item in the cache it needs to be unique.</param>
        /// <param name="item">The object to insert into the cache</param>
        /// <returns>Boolean to indicate success or failure</returns>
        public bool Add(string key, object item)
        {
            if (IsDisabled())
            {
                return false;
            }
            if (String.IsNullOrEmpty(key) || item == null)
            {
                _logger.LogWarning("Trying to use a null or empty key or item to store into the cache.");
                return false;
            }
            try
            {
                _db.Query(_simpleInsertQuery)
                    .AddParameter("key", key)
                    .AddParameter("value", item).NonQuery();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning(string.Format("Error when adding to the cache with key {0}.", key), e);
                return false;
            }
        }

        /// <summary>
        /// Returns an object from the cache after casting it as type T.
        /// </summary>
        /// <param name="key">The key used to retrieve the object from the cache</param>
        /// <returns>
        /// An object of type T
        /// If casting fails, returns default(T).
        /// If object is not in the cache returns default(T).
        /// If key is not valid returns default(T).
        /// </returns>
        public string Get(string key)
        {
            var returnObj = string.Empty;
            if (IsDisabled())
            {
                return returnObj;
            }
            if (String.IsNullOrEmpty(key))
            {
                _logger.LogWarning("Trying to use a null or empty key to get an object from the cache.");
                return returnObj;
            }
            try
            {
                var dt = _db.Query(_selectQuery).AddParameter("key", key).GetDataTable();
                var created = (DateTime)dt.Rows[0]["Created"];
                var secondsToExpire = Utils.ToDouble(dt.Rows[0]["SecondsToExpire"]);
                var rolling = Utils.ToBool(dt.Rows[0]["Rolling"]);
                returnObj = Utils.ToString(dt.Rows[0]["Value"]);

                if (secondsToExpire > 0)
                {
                    _db.Query(_removeQuery).AddParameter("key", key).NonQuery();
                    if (created.AddSeconds(secondsToExpire) <= DateTime.Now && rolling)
                    {
                        _db.Query(_complexInsertQuery).AddParameter("key", key).AddParameter("value", returnObj)
                            .AddParameter("secondsToExpire", secondsToExpire)
                            .AddParameter("rolling", true).NonQuery();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                return returnObj;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Can't get an object of type T from the cache.", e);
                return returnObj;
            }
        }

        /// <summary>
        /// Returns an object from the cache after casting it as type T.
        /// </summary>
        /// <param name="key">The key used to retrieve the object from the cache</param>
        /// <returns>
        /// An object of type T
        /// If casting fails, returns default(T).
        /// If object is not in the cache returns default(T).
        /// If key is not valid returns default(T).
        /// </returns>
        public T Get<T>(string key)
        {
            var returnObj = default(T);
            if (IsDisabled())
            {
                return returnObj;
            }
            if (String.IsNullOrEmpty(key))
            {
                _logger.LogWarning("Trying to use a null or empty key to get an object from the cache.");
                return returnObj;
            }
            try
            {
                var dt = _db.Query(_selectQuery).GetDataTable();
                var created = (DateTime)dt.Rows[0]["Created"];
                var secondsToExpire = Utils.ToDouble(dt.Rows[0]["SecondsToExpire"]);
                var rolling = Utils.ToBool(dt.Rows[0]["Rolling"]);
                returnObj = (T)dt.Rows[0]["Value"];

                if (secondsToExpire > 0)
                {
                    if (created.AddSeconds(secondsToExpire) <= DateTime.Now && rolling)
                    {
                        _db.Query(_renewQuery).AddParameter("key", key).AddParameter("secondsToExpire", secondsToExpire).
                            NonQuery();
                    }
                    else
                    {
                        _db.Query(_removeQuery).AddParameter("key", key).NonQuery();
                        return returnObj;
                    }
                }
                return returnObj;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Can't get an object of type T from the cache.", e);
                return returnObj;
            }
        }

        ///<summary>
        /// Adds an object to the cache for an absolute period of time. 
        /// The object will be removed for sure after that period of time.
        /// The object may also be removed sooner if memory is needed.
        ///</summary>
        /// <param name="key">The key for the item in the cache it needs to be unique.</param>
        /// <param name="item">The object to insert into the cache</param>
        ///<param name="expiresAfter">A <see cref="TimeSpan"/> representing the period to store the object for</param>
        /// <returns>Boolean to indicate success or failure</returns>
        public bool AddAndKeepFor(string key, object item, TimeSpan expiresAfter)
        {
            if (IsDisabled())
            {
                return false;
            }
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogWarning("Trying to use a null or empty key to set an object into the cache.");
                return false;
            }
            try
            {
                _db.Query(_complexInsertQuery).AddParameter("key", key).AddParameter("value", item)
                    .AddParameter("secondsToExpire", expiresAfter.TotalSeconds)
                    .AddParameter("rolling", false).NonQuery();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning(string.Format("Error when adding to the cache with key {0}.", key), e);
                return false;
            }
        }

        ///<summary>
        /// Adds an object to the cache for a rolling period of time. 
        /// The object will be removed for sure after that period of time if there is no activity with it.
        /// The object may also be removed sooner if memory is needed.
        ///</summary>
        /// <param name="key">The key for the item in the cache it needs to be unique.</param>
        /// <param name="item">The object to insert into the cache</param>
        ///<param name="keepForPeriod">A <see cref="TimeSpan"/> representing the period to keep the object in the cache after initial set and each request</param>
        /// <returns>Boolean to indicate success or failure</returns>
        public bool AddAndKeepWhileUsed(string key, object item, TimeSpan keepForPeriod)
        {
            if (IsDisabled())
            {
                return false;
            }
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogWarning("Trying to use a null or empty key to set an object into the cache.");
                return false;
            }
            try
            {
                _db.Query(_complexInsertQuery).AddParameter("key", key).AddParameter("value", item)
                    .AddParameter("secondsToExpire", keepForPeriod.TotalSeconds)
                    .AddParameter("rolling", true).NonQuery();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning(string.Format("Error when adding to the cache with key {0}.", key), e);
                return false;
            }
        }

        ///<summary>
        /// Removes an object from the cache
        ///</summary>
        /// <param name="key">The key for the item in the cache.</param>
        /// <returns>Boolean to indicate success or failure</returns>
        public bool Remove(string key)
        {
            if (IsDisabled())
            {
                return false;
            }
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogWarning("Trying to use a null or empty key to set an object into the cache.");
                return false;
            }

            try
            {
                _db.Query(_removeQuery).NonQuery();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning(string.Format("Error when Removing from the cache with key {0}.", key), e);
                return false;
            }
        }

        ///<summary>
        /// Removes all items in the cache
        ///</summary>
        /// <returns>Boolean to indicate success or failure</returns>
        public bool Clear()
        {
            if (IsDisabled())
            {
                return false;
            }
            try
            {
                _db.Query(_clearQuery).NonQuery();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error when clearing the cache", e);
                return false;
            }
        }
    }
}