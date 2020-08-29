using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BadEggStudio.ItemEditor {
    public static class E_ItemEditorUtils {
        
		public static ItemDatabase Create(string _name) {

			// Check config file setup
			if(string.IsNullOrEmpty(ItemEditorConfig.SAVE_ASSET_PATH)) {
				Debug.LogError("You have to set the SAVE_ASSET_PATH in ItemEditorConfig file first!");
				return null;
			}

			ItemDatabase itemdb = (ItemDatabase)ScriptableObject.CreateInstance<ItemDatabase> ();
			if(itemdb != null) {
				itemdb.name = _name;
				AssetDatabase.CreateAsset(itemdb, ItemEditorConfig.SAVE_ASSET_PATH + _name + ".asset");
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
				AssetDatabase.GetAssetPath(itemdb);
				return itemdb;
			} else {
				return null;
			}
		}

		public static ItemDatabase[] LoadLists() {

			// Check config file setup
			if(string.IsNullOrEmpty(ItemEditorConfig.LOAD_ASSET_PATH)) {
				Debug.LogError("You have to set the LOAD_ASSET_PATH in ItemEditorConfig file first!");
				return null;
			}

			Object[] o = Resources.LoadAll(ItemEditorConfig.LOAD_ASSET_PATH, typeof(ItemDatabase));
			List<ItemDatabase> itemdbs = new List<ItemDatabase>();

			for (int i = 0; i < o.Length; i++) {
				itemdbs.Add((ItemDatabase)o[i]);
			}

			ItemDatabase[] itemdbArr = itemdbs.ToArray();

			return itemdbArr;
		}
    }
}
