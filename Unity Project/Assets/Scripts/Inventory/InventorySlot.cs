using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;

    Item item;

    /// <summary>
    /// Add a new item to the inventory list.
    /// </summary>
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;

        removeButton.interactable = true;
    }

    /// <summary>
    /// Remove an item from the inventory list.
    /// </summary>
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    #region UI BUTTON FUNCTIONS

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void OnUseButton()
    {
        if (item != null)
        {
            item.UseItem();
        }
    }

    #endregion
}
