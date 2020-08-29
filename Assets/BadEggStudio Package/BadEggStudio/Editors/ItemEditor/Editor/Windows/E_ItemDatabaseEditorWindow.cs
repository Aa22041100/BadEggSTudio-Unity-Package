using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace BadEggStudio.ItemEditor {
    public class E_ItemDatabaseEditorWindow : EditorWindow {    
        public static E_ItemDatabaseEditorWindow curEditor;
        public ItemDatabase itemdb;
        private int viewIndex = 1;
        private string wantedName = "Enter a name";
        private ItemDatabase[] itemListsAssets;
        private Vector2 scrollPos;
        
        [MenuItem ("BadEggStudio/Editors/ItemDB Editor %#i")]
        static void Init() 
        {
            curEditor = (E_ItemDatabaseEditorWindow)EditorWindow.GetWindow(typeof (E_ItemDatabaseEditorWindow));
            curEditor.titleContent.text = "Item Databases";
        }
        
        void OnEnable() 
        {
            this.minSize = new Vector2(660, 300);
            this.maxSize = new Vector2(660, 300);
            if(EditorPrefs.HasKey("ObjectPath")) 
            {
                string objectPath = EditorPrefs.GetString("ObjectPath");
                itemdb = AssetDatabase.LoadAssetAtPath (objectPath, typeof(ItemDatabase)) as ItemDatabase;
            }
            RefreshItemList();
        }
        
        void OnGUI() 
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            if(itemdb == null)
                GUILayout.Label("Item Databases Editor", EditorStyles.label);
            else
                GUILayout.Label("Item Databases Editor - " + itemdb.name, EditorStyles.label);

            if (GUILayout.Button("Open Database", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) 
            {
                OpenItemList();
            }
            if (itemdb != null && GUILayout.Button("Close Database", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) 
            {
                CloseItemList();
            }
            GUILayout.EndHorizontal();
            
            if (itemdb == null) 
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.LabelField ("Enter Database Name:", EditorStyles.label, GUILayout.ExpandWidth(false));
                wantedName = EditorGUILayout.TextField ("", wantedName, EditorStyles.textField, GUILayout.ExpandWidth(true));
                if (GUILayout.Button("Create DB", GUILayout.Width(100)))
                {
                    if (wantedName != "Enter a name" && !string.IsNullOrEmpty(wantedName))
                    {
                        CreateItemDatabase(wantedName);
                        wantedName = "Enter a name";
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Item Databases Editor Editor Message", "Please enter a valid name!", "OK");
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField ("ItemDatabase (ScriptableObjects)", EditorStyles.boldLabel, GUILayout.ExpandWidth(true));
                if(GUILayout.Button("Refresh", GUILayout.Width(100)))
                {
                    RefreshItemList();
                }
                GUILayout.EndHorizontal();
                scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                if(itemListsAssets != null)
                {
                    foreach (ItemDatabase itemdb in itemListsAssets)
                    {
                        if(itemdb != null)
                        {
                            GUILayout.BeginHorizontal(EditorStyles.helpBox);
                            EditorGUILayout.LabelField (itemdb.name, EditorStyles.label, GUILayout.ExpandWidth(true));
                            if(GUILayout.Button("open", GUILayout.Width(53)))
                            {
                                this.itemdb = itemdb;
                            }
                            if(GUILayout.Button("x", GUILayout.Width(20)))
                            {
                                ConfirmDeleteItemList(itemdb);
                            }
                            GUILayout.EndHorizontal();
                        }
                    }
                }
                GUILayout.EndScrollView();
                GUILayout.EndVertical();
            }
                
            if (itemdb != null) 
            {
                GUILayout.BeginHorizontal(EditorStyles.toolbar);         
                if (GUILayout.Button("Prev", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) 
                {
                    if (viewIndex > 1)
                        viewIndex --;
                }
                if (GUILayout.Button("Next", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) 
                {
                    if (viewIndex < itemdb.items.Count) 
                    {
                        viewIndex ++;
                    }
                }

                GUILayout.Space(15);
                
                if (GUILayout.Button("Add Item", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) 
                {
                    AddItem();
                }
                if (GUILayout.Button("Delete Item", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) 
                {
                    DeleteItem(viewIndex - 1);
                }
                            
                GUILayout.Space(15);

                if (itemdb.items.Count > 0) 
                {
                    viewIndex = EditorGUILayout.IntSlider(viewIndex, 1, itemdb.items.Count, GUILayout.ExpandWidth(true), GUILayout.Height(14));
                    //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemdb.Count);
                    EditorGUILayout.LabelField("of   " +  itemdb.items.Count.ToString() + "  items", "", EditorStyles.label, GUILayout.ExpandWidth(false));
                }
        
                GUILayout.EndHorizontal();

                if (itemdb.items.Count > 0) 
                {             
                    // GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    // GUILayout.BeginVertical();
                    // GUILayout.Label("Item Details", EditorStyles.boldLabel);       
                    // itemdb.items[viewIndex-1].itemName = EditorGUILayout.TextField ("Item Name", itemdb.items[viewIndex-1].itemName as string, GUILayout.ExpandWidth(true));
                    // GUILayout.BeginHorizontal();
                    // itemdb.items[viewIndex-1].isBuyable = (bool)EditorGUILayout.Toggle("isBuyable", itemdb.items[viewIndex-1].isBuyable, GUILayout.ExpandWidth(true));
                    // if(itemdb.items[viewIndex-1].isBuyable)
                    //     itemdb.items[viewIndex-1].buyValue = EditorGUILayout.IntField("buyValue", itemdb.items[viewIndex-1].buyValue, GUILayout.ExpandWidth(true));
                    // GUILayout.EndHorizontal();
                    // GUILayout.BeginHorizontal();
                    // itemdb.items[viewIndex-1].isSellable = (bool)EditorGUILayout.Toggle("isSellable", itemdb.items[viewIndex-1].isSellable, GUILayout.ExpandWidth(true));
                    // if(itemdb.items[viewIndex-1].isSellable)
                    //     itemdb.items[viewIndex-1].sellValue = EditorGUILayout.IntField("sellValue", itemdb.items[viewIndex-1].sellValue, GUILayout.ExpandWidth(true));
                    // GUILayout.EndHorizontal();
                    // GUILayout.BeginHorizontal();
                    // itemdb.items[viewIndex-1].isUpgradeable = (bool)EditorGUILayout.Toggle("isUpgradeable ", itemdb.items[viewIndex-1].isUpgradeable, GUILayout.ExpandWidth(true));
                    // if(itemdb.items[viewIndex-1].isUpgradeable)
                    //     itemdb.items[viewIndex-1].upgradeValue = EditorGUILayout.IntField("upgradeValue", itemdb.items[viewIndex-1].upgradeValue, GUILayout.ExpandWidth(true));
                    // GUILayout.EndHorizontal();
                    // GUILayout.BeginHorizontal();
                    // itemdb.items[viewIndex-1].isDropable = (bool)EditorGUILayout.Toggle("isDropable ", itemdb.items[viewIndex-1].isDropable, GUILayout.ExpandWidth(true));
                    // if(itemdb.items[viewIndex-1].isDropable)
                    //     itemdb.items[viewIndex-1].dropRate = EditorGUILayout.FloatField("dropRate", itemdb.items[viewIndex-1].dropRate, GUILayout.ExpandWidth(true));              
                    // GUILayout.EndHorizontal();
                    // GUILayout.BeginHorizontal();
                    // itemdb.items[viewIndex-1].isTradable = (bool)EditorGUILayout.Toggle("isTradable ", itemdb.items[viewIndex-1].isTradable, GUILayout.ExpandWidth(true));
                    // GUILayout.EndHorizontal();
                    // GUILayout.BeginHorizontal();
                    // itemdb.items[viewIndex-1].isAuctionable = (bool)EditorGUILayout.Toggle("isAuctionable ", itemdb.items[viewIndex-1].isAuctionable, GUILayout.ExpandWidth(true));
                    // GUILayout.EndHorizontal();
                    // GUILayout.EndVertical();
                    // GUILayout.BeginVertical();
                    // itemdb.items[viewIndex-1].itemIcon = EditorGUILayout.ObjectField(itemdb.items[viewIndex-1].itemIcon, typeof (Sprite), false, GUILayout.Height(125), GUILayout.Width(125)) as Sprite;
                    // GUILayout.EndVertical();
                    // GUILayout.EndHorizontal();

                    // GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    // GUILayout.BeginVertical();				
                    // GUILayout.Label("Damage Stats", EditorStyles.boldLabel);
                    // itemdb.items[viewIndex-1].itemStats.minDamage = EditorGUILayout.IntField("minDamage", itemdb.items[viewIndex-1].itemStats.minDamage, GUILayout.ExpandWidth(true));
                    // itemdb.items[viewIndex-1].itemStats.maxDamage = EditorGUILayout.IntField("maxDamage", itemdb.items[viewIndex-1].itemStats.maxDamage, GUILayout.ExpandWidth(true));
                    // itemdb.items[viewIndex-1].itemStats.range = EditorGUILayout.FloatField("range", itemdb.items[viewIndex-1].itemStats.range, GUILayout.ExpandWidth(true));
                    // GUILayout.EndVertical();

                    // GUILayout.BeginVertical();
                    // GUILayout.Label("Skill Stats", EditorStyles.boldLabel);
                    // itemdb.items[viewIndex-1].itemStats.strength = EditorGUILayout.IntField("strength", itemdb.items[viewIndex-1].itemStats.strength, GUILayout.ExpandWidth(true));
                    // itemdb.items[viewIndex-1].itemStats.mobility = EditorGUILayout.IntField("mobility", itemdb.items[viewIndex-1].itemStats.mobility, GUILayout.ExpandWidth(true));
                    // itemdb.items[viewIndex-1].itemStats.dexterity = EditorGUILayout.IntField("dexterity", itemdb.items[viewIndex-1].itemStats.dexterity, GUILayout.ExpandWidth(true));
                    // itemdb.items[viewIndex-1].itemStats.charisma = EditorGUILayout.IntField("charisma", itemdb.items[viewIndex-1].itemStats.charisma, GUILayout.ExpandWidth(true));
                    // GUILayout.EndVertical();
                    // GUILayout.EndHorizontal();    
                } 
                else 
                {
                    GUILayout.Label ("This Item List is Empty.");
                }
            }
            if (GUI.changed) 
            {
                if(itemdb != null)
                    EditorUtility.SetDirty(itemdb);
            }
        }
        
        void CreateItemDatabase(string _name) 
        {
            viewIndex = 1;
            itemdb = E_ItemEditorUtils.Create(_name);
            if (itemdb) 
            {
                itemdb.items = new List<Item>();
                string relPath = AssetDatabase.GetAssetPath(itemdb);
                EditorPrefs.SetString("ObjectPath", relPath);
                AddItem();
            }
        }
        
        void OpenItemList() 
        {
            string absPath = EditorUtility.OpenFilePanel ("Select Item List", "", "");
            if (absPath.StartsWith(Application.dataPath)) 
            {
                string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
                itemdb = AssetDatabase.LoadAssetAtPath (relPath, typeof(ItemDatabase)) as ItemDatabase;
                if (itemdb.items == null)
                    itemdb.items = new List<Item>();
                if (itemdb) {
                    EditorPrefs.SetString("ObjectPath", relPath);
                }
            }
        }

        void CloseItemList()
        {
            itemdb = null;
            EditorPrefs.SetString("ObjectPath", null);
            RefreshItemList();
        }

        void AddItem() 
        {
            Item newItem = new Item();
            newItem.itemName = "New Item";
            itemdb.items.Add (newItem);
            viewIndex = itemdb.items.Count;
        }
        
        void RefreshItemList()
        {
            itemListsAssets = E_ItemEditorUtils.LoadLists();
        }
        
        void DeleteItem(int index) 
        {
            itemdb.items.RemoveAt (index);
        }

        void DeleteItemList(ItemDatabase _inv)
        {
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_inv));
            RefreshItemList();
        }

        void ConfirmDeleteItemList(ItemDatabase _inv)
        {
            int option = EditorUtility.DisplayDialogComplex("Are you sure to delete this Item List?",
                    "Please choose one of the following options.",
                    "Delete Item",
                    "Cancel",
                    "Help");

            switch (option)
            {
                case 0:
                    E_ItemDatabaseEditorWindow curWindow = (E_ItemDatabaseEditorWindow)EditorWindow.GetWindow(typeof(E_ItemDatabaseEditorWindow));
                    curWindow.DeleteItemList(_inv);
                    break;
                case 1:              
                    break;
                case 2:
                    Debug.Log("Do you want to completely delete the item list?");
                    break;
                default:
                    Debug.LogError("Unrecognized option.");
                    break;
            }
        }
    }
}