using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<GameObject> inventoryItems = new List<GameObject>();
    public GameObject testObject;

    [Tooltip("Parent gameobject for all inventory items, this is a workaround to make sure all items update when playing the game.")]
    public Transform inventoryParent;
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

        Add(testObject);
    }

    public bool Add(GameObject item)
    {
        if (inventoryItems.Count >= inventorySpace)
        {
            Debug.Log("Inventory full.");
            return false;
        }

        inventoryItems.Add(item);
        item.transform.parent = inventoryParent;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(GameObject item)
    {
        inventoryItems.Remove(item);
        Destroy(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
