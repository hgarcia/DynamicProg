using System;

namespace DynamicProg.Caching
{
    ///<summary>
    /// Defines the contract for our caching objects
    ///</summary>
    public interface ICache
    {
        /// <summary>
        /// Adds an object to the cache without any dependency or expiration date
        /// </summary>
        /// <param name="key">The key for the item in the cache it needs to be unique.</param>
        /// <param name="item">The object to insert into the cache</param>
        /// <returns>Boolean to indicate success or failure</returns>
        bool Add(string key, object item);

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
        T Get<T>(string key);

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
        string Get(string key);

        ///<summary>
        /// Adds an object to the cache for an absolute period of time. 
        /// The object will be removed for sure after that period of time.
        /// The object may also be removed sooner if memory is needed.
        ///</summary>
        /// <param name="key">The key for the item in the cache it needs to be unique.</param>
        /// <param name="item">The object to insert into the cache</param>
        ///<param name="expiresAfter">A <see cref="TimeSpan"/> representing the period to store the object for</param>
        /// <returns>Boolean to indicate success or failure</returns>
        bool AddAndKeepFor(string key, object item, TimeSpan expiresAfter);

        ///<summary>
        /// Adds an object to the cache for a rolling period of time. 
        /// The object will be removed for sure after that period of time if there is no activity with it.
        /// The object may also be removed sooner if memory is needed.
        ///</summary>
        /// <param name="key">The key for the item in the cache it needs to be unique.</param>
        /// <param name="item">The object to insert into the cache</param>
        ///<param name="keepForPeriod">A <see cref="TimeSpan"/> representing the period to keep the object in the cache after initial set and each request</param>
        /// <returns>Boolean to indicate success or failure</returns>
        bool AddAndKeepWhileUsed(string key, object item, TimeSpan keepForPeriod);

        ///<summary>
        /// Removes an object from the cache
        ///</summary>
        /// <param name="key">The key for the item in the cache.</param>
        /// <returns>Boolean to indicate success or failure</returns>
        bool Remove(string key);

        ///<summary>
        /// Removes all items in the cache
        ///</summary>
        /// <returns>Boolean to indicate success or failure</returns>
        bool Clear();
    }
}