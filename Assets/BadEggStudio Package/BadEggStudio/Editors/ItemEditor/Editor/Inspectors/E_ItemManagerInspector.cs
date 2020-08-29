using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BadEggStudio.ItemEditor {

	[CustomEditor(typeof(ItemManager))]
    public class E_ItemManagerInspector : Editor {
		ItemManager myTarget;
		private List<ItemDatabase> itemdbs = new List<ItemDatabase>();

        /// <summary>
        /// Editor script entry point
        /// </summary>
		void OnEnable() {
			myTarget =(ItemManager)target;
			Undo.undoRedoPerformed += OnUndoRedo;
			RefreshItemDatabases();
		}

        /// <summary>
        /// Editor script exit point
        /// </summary>
		void OnDisable() {
			Undo.undoRedoPerformed -= OnUndoRedo;
		}

        /// <summary>
        /// Editor script core function for drawing inspector UI
        /// </summary>
		public override void OnInspectorGUI() {
			myTarget =(ItemManager) target;

			// Header toolbar
			GUILayout.BeginHorizontal(EditorStyles.toolbar);
			if(GUILayout.Button("Open Editor", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) {
				// TODO:: Exceute my own method here
				EditorApplication.ExecuteMenuItem("BadEggStudio/Editors/ItemDB Editor");
			}
			if(GUILayout.Button("Open Item Database", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) {
				OpenItemDatabase();
			}
			if(GUILayout.Button("Refresh", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) {
				RefreshItemDatabases();
			}
			GUILayout.Label("", GUILayout.ExpandWidth(true));
			GUILayout.EndHorizontal();

			// Show active or inactive item databases
			GUILayout.BeginVertical(EditorStyles.helpBox);

			// Active item databases
			GUILayout.Label("Active Item Databases", EditorStyles.boldLabel);
			if(myTarget.itemdbs.Count == 0) {
				GUILayout.BeginHorizontal(EditorStyles.helpBox);
				GUILayout.Label("No Item DBs have been loaded", GUILayout.ExpandWidth(true));
				GUILayout.EndHorizontal();
			} else {
				foreach(ItemDatabase itemdb in myTarget.itemdbs) {
					if(itemdb != null) {
						GUILayout.BeginHorizontal(EditorStyles.helpBox);
						GUILayout.Label(itemdb.name, GUILayout.ExpandWidth(true));
						if(GUILayout.Button("-", GUILayout.Width(20))) {
							Undo.RecordObject(myTarget, "Item Database Remove List");
							myTarget.itemdbs.Remove(itemdb);
							this.RefreshItemDatabases();
							break;
						}
						GUILayout.EndHorizontal();
					}
				}
			}

			// Inactive item databases
			GUILayout.Label("Inactive Item Databases", EditorStyles.boldLabel);
			if(itemdbs.Count == 0) {
				GUILayout.BeginHorizontal(EditorStyles.helpBox);
				GUILayout.Label("All Loaded", GUILayout.ExpandWidth(true));
				GUILayout.EndHorizontal();
			} else {
				foreach(ItemDatabase itemdb in itemdbs) {
					GUILayout.BeginHorizontal(EditorStyles.helpBox);
					GUILayout.Label(itemdb.name, GUILayout.ExpandWidth(true));
					if(GUILayout.Button("+", GUILayout.Width(20))) {
						Undo.RecordObject(myTarget, "Item Database Add List");
						myTarget.itemdbs.Add(itemdb);
						this.RefreshItemDatabases();
						break;
					}
					GUILayout.EndHorizontal();
				}
			}

			GUILayout.EndVertical();
        }

        /// <summary>
        /// Open clicked item database
        /// </summary>
		void OpenItemDatabase()  {
			string absPath = EditorUtility.OpenFilePanel("Select Item List", "", "");
			if(absPath.StartsWith(Application.dataPath)) {
				string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
				myTarget.itemdbs.Add(AssetDatabase.LoadAssetAtPath(relPath, typeof(ItemDatabase)) as ItemDatabase);
			}
		}

        /// <summary>
        /// Undo Handling
        /// </summary>
        void OnUndoRedo() {

        }
        
        /// <summary>
        /// Refreshing loaded item databases
        /// </summary>
		void RefreshItemDatabases() {
			ItemDatabase[] itemdbs = E_ItemEditorUtils.LoadLists();
			
			this.itemdbs.Clear();
			foreach(ItemDatabase itemdb in itemdbs) {
				if(!myTarget.itemdbs.Contains(itemdb)) {
					this.itemdbs.Add(itemdb);
				}
			}
		}
    }
}
