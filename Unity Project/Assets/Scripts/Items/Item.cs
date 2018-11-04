using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    [Header("Item Properties")]
    [Tooltip("This is the name of the object, it can be changed to anything. Later maybe we can let people name items?")]
    new public string name = "New Item";
    [Tooltip("This is the icon of the object, it will be displayed in the inventory when players pick it up.")]
    public Sprite itemIcon = null;


    public virtual void UseItem()
    {
        // Use the item, held in the inventory.
        
        Debug.Log("Using: " + name);
    }
}
