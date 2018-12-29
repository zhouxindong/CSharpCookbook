using System;
using System.Collections.Generic;

namespace CSharpCookbook
{
    public class ObjCache<T, U>
        where U : new ()
    {
        private Dictionary<T, WeakReference> _cache = null;
        public ObjCache()
        {
            _cache = new Dictionary<T, WeakReference>();
        }

        public ObjCache(int capacity)
        {
            _cache = new Dictionary<T, WeakReference>(capacity);
        }

        public bool IsObjectAlive(ref T key)
        {
            if (_cache.ContainsKey(key))
            {
                return _cache[key].IsAlive;
            }
            return false;
        }

        public U this[T key]
        {
            get
            {
                if (!_cache.ContainsKey(key) || !IsObjectAlive(ref key))
                {
                    this[key] = new U();
                }

                return (U)_cache[key].Target;
            }

            set
            {
                var wr = new WeakReference(value, false);
                if (_cache.ContainsKey(key))
                {
                    _cache[key] = wr;
                }
                else
                {
                    _cache.Add(key, wr);
                }
            }
        }

        public int AliveObjsInCache()
        {
            int count = 0;
            foreach (var item in _cache)
            {
                if (item.Value.IsAlive)
                    count++;
            }

            return count;
        }

        public bool DoesKeyExist(T key)
        {
            return _cache.ContainsKey(key);
        }

        public bool DoesObjExist(WeakReference obj)
        {
            return _cache.ContainsValue(obj);
        }

        public int TotalCacheSlots()
        {
            return _cache.Count;
        }
    }
}