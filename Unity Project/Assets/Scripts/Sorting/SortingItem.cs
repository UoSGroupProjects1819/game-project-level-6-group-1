using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sorting Object", menuName = "Items/Sorting Item")]
public class SortingItem : ScriptableObject
{
    public Sprite objSprite;
    public int energyConsume;
    public ItemType itemType = ItemType.None;

    [HideInInspector] public enum ItemType { None, Vegetable, Fruit };

    public int GetItemType()
    {
        int itemCat = 0;

        switch (itemType)
        {
            case ItemType.None:
                itemCat = 0;
                break;
            case ItemType.Vegetable:
                itemCat = 1;
                break;
            case ItemType.Fruit:
                itemCat = 2;
                break;
        }

        return itemCat;
    }
}
