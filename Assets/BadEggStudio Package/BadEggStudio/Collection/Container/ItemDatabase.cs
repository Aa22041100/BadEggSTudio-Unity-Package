using System.Collections.Generic;
using UnityEngine;

namespace BadEggStudio.Collection {

    [CreateAssetMenu(fileName = "Item_Database", menuName = "BadEggStudio/Items/Create Database", order = 1)]
    public class ItemDatabase : ScriptableObject {
        public List<BaseItem> items;
    }
}
