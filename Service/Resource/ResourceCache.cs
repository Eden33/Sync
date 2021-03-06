﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model.Data;

namespace Service.Resource
{
    public class ResourceCache
    {
        private Dictionary<Type, Dictionary<int, Item>> cache = new Dictionary<Type, Dictionary<int, Item>>();

        #region methods to retrieve cached resources

        public T GetItem<T>(int id) where T : Item
        {
            Dictionary<int, Item> theDict = GetDict<T>();
            Item cached = null;
            theDict.TryGetValue(id, out cached);
            return (T) cached;
        }

        public List<T> GetAllItems<T>() where T : Item
        {
            Dictionary<int, Item> theDict = GetDict<T>();
            List<KeyValuePair<int, Item>> l = theDict.ToList();
            List<T> theList = new List<T>();
            foreach(KeyValuePair<int, Item> p in l)
            {
                theList.Add((T) p.Value);
            }
            return theList;
        }

        #endregion

        #region methods to cache resources

        public void CacheItem<T>(T item) where T : Item
        {
            Dictionary<int, Item> theDict = GetDict<T>();
            if (!theDict.ContainsKey(item.Id))
            {
                theDict.Add(item.Id, item);
            }
        }

        public void CacheAllItems<T>(IEnumerable<T> items) where T : Item
        {
            Dictionary<int, Item> theDict = GetDict<T>();
            foreach (T item in items)
            {
                if (!theDict.ContainsKey(item.Id))
                {
                    theDict.Add(item.Id, item);
                }
            }
        }

        #endregion

        #region private methods

        private Dictionary<int, Item> GetDict<T>() where T : Item
        {
            Dictionary<int, Item> theDict = null;
            if(!cache.TryGetValue(typeof(T), out theDict))
            {
                theDict = new Dictionary<int,Item>();
                cache.Add(typeof(T), theDict);
            }
            return theDict;
        }

        #endregion
    }
}
