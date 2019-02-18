using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    [SerializeField] private int inventorySpace = 20;

    #region Singleton
    public static Inventory instance;
    private void Awake()
    {   
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        onItemChangedCallback.Invoke();
    }

    public bool Add(InventoryItem item)
    {
        if (inventoryItems.Count >= inventorySpace)
        {
            Debug.Log("Inventory full.");
            return false;
        }

        inventoryItems.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(InventoryItem item)
    {
        inventoryItems.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
