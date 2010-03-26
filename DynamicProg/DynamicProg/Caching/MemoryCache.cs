using System;
using System.Web;
using System.Web.Caching;
using DynamicProg.Logging;

namespace DynamicProg.Caching
{
    ///<summary>
    /// An in memory implementation of <see cref="ICache"/>
    ///</summary>
    public class MemoryCache : ICache
    {
        private readonly Cache _cache;
        private readonly ILogger _logger;
        private readonly CachingMemoryCacheConfig _configReader;

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
        /// Constructor
        ///</summary>
        public MemoryCache(ILogger logger)
        {
            _configReader = CachingConfiguration.GetConfig().MemoryCache;

            if (IsDisabled()) return;
            
            _logger = logger;

            if (_cache != null) return;

            _cache = HttpContext.Current == null ? 
                HttpRuntime.Cache : 
                HttpContext.Current.Cache;
        }

        /// <summary>
        /// Adds an object to the cache without any dependency or expiration date
        /// </summary>
        /// <param name="key">The key for the item in the cache it needs to be unique.</param>
        /// <param name="item">The object to insert into the cache</param>
        /// <returns>Boolean to indicate success or failure</returns>
        public bool Add(string key, object item)
        {
            if (IsDisabled()) return false;
            
            if (String.IsNullOrEmpty(key))
            {
                _logger.LogWarning("Trying to use a null or empty key to store an object into the cache.");
                return false;
            }
            try
            {
                _cache.Insert(key, item);
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
        /// <typeparam name="T">The type to cast the object to</typeparam>
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
            if (_cache[key] == null)
            {
                return returnObj;
            }
            try
            {
                return (T)_cache[key];
            }
            catch (Exception e)
            {
                _logger.LogWarning("Can't get an object of type T from the cache.", e);
                return returnObj;
            }
        }

        ///<summary>
        /// Returns a string or the ToString method of an object
        ///</summary>
        ///<param name="key">The key to get the object from the Cache</param>
        /// <returns>
        /// An object of type T
        /// If casting fails, returns string.Empty.
        /// If object is not in the cache returns string.Empty.
        /// If key is not valid returns string.Empty.
        /// </returns>
        public string Get(string key)
        {
            return Get<string>(key);
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
                _cache.Insert(key, item, null, DateTime.UtcNow.Add(expiresAfter), TimeSpan.Zero);
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
                _cache.Insert(key, item, null, DateTime.MaxValue, keepForPeriod);
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
                _cache.Remove(key);
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
                var enumeration = _cache.GetEnumerator();

                while (enumeration.MoveNext())
                {
                    _cache.Remove(enumeration.Key.ToString());
                }
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