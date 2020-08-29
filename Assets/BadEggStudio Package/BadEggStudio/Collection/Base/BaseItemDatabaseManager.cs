using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadEggStudio.Collection
{
    public class BaseItemDatabaseManager : MonoBehaviour
    {

        public ItemDatabase itemDatabase;

        // Start is called before the first frame update
        void Start()
        {

            // Init the item database manager
            InitialiseItemDatabase();

        }

        public virtual void InitialiseItemDatabase()
        {
        }

        /// <summary>
        /// Get item from item database by ID
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>Item</returns>
        public virtual BaseItem GetItem(int id) {
            if(itemDatabase)
            {
                if(itemDatabase.items != null)
                {
                    return itemDatabase.items.Find(item => item.id == id);
                }
            }
            return null;
        }

        /// <summary>
        /// Add item to the item database
        /// </summary>
        /// <param name="newItem">New item</param>
        public virtual void AddItem(BaseItem newItem) {
            if(itemDatabase)
            {
                // Check the item database is not null
                if(itemDatabase.items == null)
                {
                    Debug.LogError("Item Database is null");
                    return;
                }

                // Check the there is any duplicated item in the item database
                BaseItem checkItem = itemDatabase.items.Find(item => item.id == newItem.id);
                if(checkItem != null)
                {
                    Debug.LogError("Item ID duplicated");
                    return;
                }

                // If we pass the checking, we can add it to the item database
                itemDatabase.items.Add(newItem);

            }
        }
    }
}