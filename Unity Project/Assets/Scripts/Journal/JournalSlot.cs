using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalSlot : MonoBehaviour {

    public Image icon;

    InventoryItem item;

    public void AddItem(InventoryItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnRemoveButton()
    {

    }

    public void OnUseButton()
    {
        if (item != null)
        {
            item.UseJournal();
        }
    }
}
