using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite itemIcon = null;

    public virtual void UseItem()
    {
        // Use the item, held in the inventory.
        
        Debug.Log("Using: " + name);
    }
}
