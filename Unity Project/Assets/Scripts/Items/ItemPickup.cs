using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public Item item;

    private void OnMouseDown()
    {
        Interact();
    }

    private void Interact()
    {
        Debug.Log("Interacted with: " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
