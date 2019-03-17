using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Image icon;
    public Image iconBack;
    public Button removeButton;
    public Outline outline;

    private GameObject item;
    private bool pointerDown = false;
    private float pointerDownTimer;

    /// <summary>
    /// Add a new item to the inventory list.
    /// </summary>
    public void AddItem(GameObject newItem)
    {
        SeedController seedController = newItem.GetComponent<SeedController>();

        item = newItem;
        icon.sprite =   seedController.inventorySprite;
                        seedController.invImg = icon;

        seedController.itemOutline = outline;

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

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer >= 2)
            {
                if (item != null)
                {
                    //Debug.Log("Long Interact");
                    item.GetComponent<SeedController>().Interact();
                }

                Reset();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        //Debug.Log("PointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        //Debug.Log("PointerUp");
    }

    public void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        //Debug.Log("Reset");
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
