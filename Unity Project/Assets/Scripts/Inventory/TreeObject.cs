using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tree", menuName = "Inventory/Tree")]
public class TreeObject : Item {

    [Tooltip("This is the object that will be spawned when players select it.")]
    [SerializeField] private GameObject spawnObject;

    public override void UseItem()
    {
        base.UseItem();

        GameManager.instance.itemToPlace = spawnObject;
        GameManager.instance.itemHolding = this;
    }
}
