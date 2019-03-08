using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Image iconBack;
    public Button removeButton;

    private GameObject item;

    /// <summary>
    /// Add a new item to the inventory list.
    /// </summary>
    public void AddItem(GameObject newItem)
    {
        SeedController seedController = newItem.GetComponent<SeedController>();

        item = newItem;
        icon.sprite =   seedController.inventorySprite;
                        seedController.invImg = icon;

        icon.fillAmount = 0;

        icon.enabled = true;
        iconBack.enabled = true;
        //seedController.StartGrowth();

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
        iconBack.enabled = false;

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
            item.GetComponent<SeedController>().Interact();
        }
    }

    #endregion
}
