using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Cache for <see cref="T"/> Cache 
    /// </summary>
    public class EntityCache<T> : Singleton<EntityCache<T>> where T : class, IDiscordCacheable, new()
    {
        private readonly ConcurrentDictionary<Snowflake, T> _cache = new ConcurrentDictionary<Snowflake, T>();
        private readonly Func<Snowflake, T> _valueFactory = id => new T { Id = id }; 
        
        /// <summary>
        /// Readonly Cache of <see cref="DiscordUser"/>
        /// </summary>
        public readonly IReadOnlyDictionary<Snowflake, T> Cache;

        private EntityCache()
        {
            Cache = new ReadOnlyDictionary<Snowflake, T>(_cache);
        }

        public T Get(Snowflake id) => _cache.TryGetValue(id, out T value) ? value : null;
        
        /// <summary>
        /// Returns a cached <see cref="T"/> for the given user ID or creates a new <see cref="T"/> with that ID
        /// </summary>
        /// <param name="id">User ID to lookup in the cache</param>
        /// <returns>Cached <see cref="DiscordUser"/></returns>
        public T GetOrCreate(Snowflake id) => id.IsValid() ? _cache.GetOrAdd(id, _valueFactory) : default(T);
    }
}