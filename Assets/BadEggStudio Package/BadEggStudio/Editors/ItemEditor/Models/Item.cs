using System.Collections;
using UnityEngine;

namespace BadEggStudio.ItemEditor {
    [System.Serializable]
    public class Item {
			public string itemName = "New Item";
			public Sprite itemIcon = null;

			public ItemStats itemStats;

			public bool isBuyable = false;
			public int buyValue = 0;
			public bool isSellable = false;
			public int sellValue = 0;
			public bool isUpgradeable = false;
			public int upgradeValue = 0;
			public bool isDropable = false;
			public float dropRate = 0;
			public bool isTradable = false;
			public bool isAuctionable = false;
    }
} 
