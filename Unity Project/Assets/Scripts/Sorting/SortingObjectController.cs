using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingObjectController : MonoBehaviour {

    [HideInInspector] public SortingItem scrObject;
    private int itemType = 0;

    private void Start()
    {
        if (scrObject.currentItem == SortingItem.ItemType.Vegetable)
            itemType = 1;
        else if (scrObject.currentItem == SortingItem.ItemType.Fruit)
            itemType = 2;
        else
            itemType = 0;

        if (itemType == 0)
            Debug.LogError("Item type not set/Invalid item type.");
    }
}
