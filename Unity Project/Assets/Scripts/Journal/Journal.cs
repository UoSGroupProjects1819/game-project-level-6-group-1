using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour {

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<InventoryItem> journalItems = new List<InventoryItem>();
    [SerializeField] private int journalSlots = 20;

    #region Singleton
    public static Journal instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
    #endregion

    public bool Add(InventoryItem item)
    {
        if (journalItems.Count >= journalSlots)
        {
            Debug.Log("Journal Spaces Full");
            return false;
        }

        if(journalItems.Contains(item))
        {
            Debug.Log("Item exists in Journal.");
            return false;
        }

        journalItems.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }
}
