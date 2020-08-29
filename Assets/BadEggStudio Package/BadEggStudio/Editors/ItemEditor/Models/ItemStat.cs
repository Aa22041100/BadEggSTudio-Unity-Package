using UnityEngine;

namespace BadEggStudio.ItemEditor {
	[System.Serializable]
	public 	struct ItemStats {
		public int minDamage;
		public int maxDamage;
        public int minDef;
        public int maxDef;
		public float range;
		public int STR;
        public int AGI;
        public int VIT;
        public int INT;
		public int DEX;
        public int LUK;
	}
}