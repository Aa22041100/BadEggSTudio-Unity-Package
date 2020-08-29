using System.Collections.Generic;
using UnityEngine;

namespace BadEggStudio.ItemEditor {
    public class ItemManager : MonoBehaviour {

        [SerializeField]
        private List<ItemDatabase> _itemdbs = new List<ItemDatabase>();
        public List<ItemDatabase> itemdbs {
            get {
                return _itemdbs;
            }
            set {
                _itemdbs = value;
            }
        }


        // Start is called before the first frame update
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {
            
        }
    }
}
