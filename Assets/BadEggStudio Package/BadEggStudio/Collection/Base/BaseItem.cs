using System;
using UnityEngine;

namespace BadEggStudio.Collection {
    
    [Serializable]
    public class BaseItem
    {
        public string name;
        public int id;
        public string translationKey;
        public float buyPrice;
        public float sellPrice;
        public Sprite itemSprite;

        // Item Effects
        public BaseBonus bonus;

    }

}

