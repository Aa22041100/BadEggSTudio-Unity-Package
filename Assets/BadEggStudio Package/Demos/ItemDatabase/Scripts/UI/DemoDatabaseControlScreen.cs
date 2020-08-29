using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BadEggStudio.Collection;

public class DemoDatabaseControlScreen : MonoBehaviour
{

    int dbCursor = 0;

    [Header("Item Database References")]
    public BaseItemDatabaseManager itemDatabaseManager;
    public BaseItem currentItem;

    [Header("UI References")]
    public Image itemImage;
    public Text itemNameText;
    public Text itemTranslationText;
    public Text itemBuyPriceText;
    public Text itemSellPriceText;
    public Text itemHealthText;
    public Text itemManaText;
    public Text itemAtkText;
    public Text itemDefText;
    
    [Header("New Items Settings")]
    public Sprite newItemLogo;
    public string newItemName;
    public string newItemTranslationKey;
    public float newItemBuyPrice;
    public float newItemSellPrice;
    public BaseBonus newItemBonus;

    string itemNameFormat = "Name: {0}";
    string itemTranslateKeyFormat = "Translation Key: {0}";
    string itemBuyPriceFormat = "Buy: {0}";
    string itemSellPriceFormat = "Sell: {0}";
    string itemHealthFormat = "HP + {0}";
    string itemManaFormat = "Mana + {0}";
    string itemAtkFormat = "Atk + {0}";
    string itemDefFormat = "Def + {0}";

    // Start is called before the first frame update
    void Start()
    {
        currentItem = itemDatabaseManager.itemDatabase.items[dbCursor];
        UpdateItem();
    }

    void UpdateItem()
    {
        if (currentItem != null)
        {
            itemImage.overrideSprite = currentItem.itemSprite;
            itemNameText.text = string.Format(itemNameFormat, currentItem.name);
            itemTranslationText.text = string.Format(itemTranslateKeyFormat, currentItem.translationKey);
            itemBuyPriceText.text = string.Format(itemBuyPriceFormat, currentItem.buyPrice);
            itemSellPriceText.text = string.Format(itemSellPriceFormat, currentItem.sellPrice);
            itemHealthText.text = string.Format(itemHealthFormat, currentItem.bonus.health);
            itemManaText.text = string.Format(itemManaFormat, currentItem.bonus.mana);
            itemAtkText.text = string.Format(itemAtkFormat, currentItem.bonus.atk);
            itemDefText.text = string.Format(itemDefFormat, currentItem.bonus.def);
        }
    }

    public void OnNewItemClicked() {
        BaseItem item = new BaseItem();
        item.id = itemDatabaseManager.itemDatabase.items.Count;
        item.name = newItemName;
        item.itemSprite = newItemLogo;
        item.translationKey = newItemTranslationKey;
        item.buyPrice = newItemBuyPrice;
        item.sellPrice = newItemSellPrice;
        item.bonus = newItemBonus;
        
        itemDatabaseManager.AddItem(item);
        UpdateItem();
    }

    public void OnPreviousItemClicked() {

        // if the current item is the first item of the database, skip it
        if(dbCursor == 0)
        {
            return;
        }

        int nextItemId = dbCursor--;
        currentItem = itemDatabaseManager.GetItem(nextItemId);
        UpdateItem();
    }

    public void OnNextItemClicked() {
        // if the current item is the last item of the database, skip it
        if(dbCursor == itemDatabaseManager.itemDatabase.items.Count - 1)
        {
            return;
        }

        int nextItemId = dbCursor++;
        currentItem = itemDatabaseManager.GetItem(nextItemId);
        UpdateItem();
    }
}