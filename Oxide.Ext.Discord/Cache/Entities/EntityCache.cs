using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Cache.Entities
{
    /// <summary>
    /// Cache for {T} 
    /// </summary>
    public class EntityCache<T> : Singleton<EntityCache<T>> where T : class, IDiscordCacheable<T>, new()
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

        /// <summary>
        /// Returns the cached entity with the given ID; default(T) otherwise
        /// </summary>
        /// <param name="id">ID of the entity</param>
        public T Get(Snowflake id) => _cache.TryGetValue(id, out T value) ? value : default(T);

        /// <summary>
        /// Returns a cached for the given user ID or creates a new with that ID
        /// </summary>
        /// <param name="id">User ID to lookup in the cache</param>
        /// <returns>Cached <see cref="DiscordUser"/></returns>
        public T GetOrCreate(Snowflake id) => id.IsValid() ? _cache.GetOrAdd(id, _valueFactory) : default(T);
        
        /// <summary>
        /// Updates the cached entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Update(T entity)
        {
            if (entity == null || !entity.Id.IsValid())
            {
                return entity;
            }
            
            if (!_cache.TryGetValue(entity.Id, out T existing))
            {
                _cache[entity.Id] = entity;
                return entity;
            }
            
            existing.Update(entity);
            return existing;
        }
    }
}