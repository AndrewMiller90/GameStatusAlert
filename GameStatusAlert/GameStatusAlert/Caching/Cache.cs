using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatusAlert.Caching {
    /// <summary>
    /// Caching mechanism using Key Value pairs. Cache invalidates based on an ICachePolicy
    /// </summary>
    internal sealed class Cache : ICache {
        private List<CacheEntry> CachedObjects = new List<CacheEntry>();
        private ICachePolicy Policy;
        private object operationLock = new object();
        private object accessLock = new object();
        public Cache(ICachePolicy policy) {
            if (policy.SlidingExpiration == null && policy.AbsoluteExpiration == null) {
                throw new ArgumentException("Policy must have at least one expiration set", nameof(policy));
            }
            Policy = policy;
        }
        /// <summary>
        /// Checks if a given key is contained in the cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key) {
            return GetCacheEntry(key) != null;
        }
        /// <summary>
        /// Gets the value of a cache entry with a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>null on fail to find</returns>
        public object GetValue(string key) {
            return GetCacheEntry(key)?.Value;
        }
        /// <summary>
        /// Checks if a given key is contained in the cache. On failure creates the key and assignes the result of constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="constructor"></param>
        /// <returns>object from cache on success or result of constructor on fail</returns>
        public object GetValueOrCreateEntry(string key, Func<object> constructor) {
            lock (operationLock) {
                var cacheEntry = GetCacheEntry(key);
                var obj = constructor();
                if (cacheEntry == null) {
                    CreateCacheEntry(key, obj);
                }
                return obj;
            }
        }
        /// <summary>
        /// Sets the value at key in cache. Creates cache entry if not present.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, object value) {
            lock (operationLock) {
                var entry = GetCacheEntry(key);
                if (entry != null) {
                    entry.Value = value;
                } else {
                    CreateCacheEntry(key, value);
                }
            }
        }
        /// <summary>
        /// Gets the CacheEntry object for further usage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private CacheEntry GetCacheEntry(string key) {
            InvalidateCache();
            return CachedObjects.Where(x => x.Key.Equals(key)).FirstOrDefault();
        }
        /// <summary>
        /// Creates a new CacheEntry and adds it to the cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void CreateCacheEntry(string key, object value) {
            lock (accessLock) {
                CachedObjects.Add(new CacheEntry(key, value));
            }
        }
        /// <summary>
        /// Removes cache entries according to the cache policy
        /// </summary>
        private void InvalidateCache() {
            lock (accessLock) {
                IEnumerable<CacheEntry> filterQuery = CachedObjects;
                if (Policy.AbsoluteExpiration != null) {
                    filterQuery = filterQuery.Where(x => DateTime.Now - x.Created <= Policy.AbsoluteExpiration);
                }
                if (Policy.SlidingExpiration != null) {
                    filterQuery = filterQuery.Where(x => DateTime.Now - x.LastAccess <= Policy.SlidingExpiration);
                }
                InvalidateObjects(CachedObjects.Except(filterQuery));
                CachedObjects = filterQuery.ToList();
            }
        }
        //TODO: Test invalidation
        private void InvalidateObjects(IEnumerable<CacheEntry> toInvalidate) {
            foreach(var item in toInvalidate) {
                if (typeof(ICacheCleanup).IsAssignableFrom(item.Value.GetType())) {
                    ((ICacheCleanup)item.Value).CleanUp();
                }
            }
        }

        private sealed class CacheEntry {
            private object _value;
            public string Key { get; private set; }
            public object Value {
                get {
                    UpdateLastAccess();
                    return _value;
                }
                set {
                    UpdateLastAccess();
                    _value = value;
                }
            }
            public DateTime LastAccess { get; private set; }
            public DateTime Created { get; private set; } = DateTime.Now;
            public CacheEntry(string key, object value) {
                Key = key;
                Value = value;
            }
            private void UpdateLastAccess() {
                LastAccess = DateTime.Now;
            }
        }
    }
}
