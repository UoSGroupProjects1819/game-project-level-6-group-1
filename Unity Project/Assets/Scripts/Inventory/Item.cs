using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Planet Item")]
public class Item : ScriptableObject {
    /* MAKE THE VARIABLES PRIVATE AT SOME POINT && USE GETTERES */

    [Header("Item Properties")]
    [Tooltip("This is the name of the object, it can be changed to anything. Later maybe we can let people name items?")]
    new public string name = "New Item";

    [Tooltip("This is the icon of the object, it will be displayed in the inventory when players pick it up.")]
    public Sprite itemIcon = null;

    [Tooltip("This is the object that will be spawned when players select it.")]
    [SerializeField] private GameObject spawnObject;

    [Tooltip("Foobar")]
    public Sprite[] objectSprites = new Sprite[2];

    [Tooltip("Foobar")]
    public float growthTime;

    [Tooltip("Foobar")]
    public float wateringInterval;

    [Tooltip("Foobar")]
    public float probability;

    // Called when pressed in the inventory.
    public virtual void UseItem()
    {
        // Use the item
        UIManager.instance.ToggleInventoryUI();
        GameManager.instance.currentState = GameManager.GameState.PlaceItem;

        GameManager.instance.itemToPlace = spawnObject;
        GameManager.instance.itemHolding = this;

        Debug.Log("Using: " + name);
    }

    public float GetProbability
    {
        get { return probability; }
    }
}
