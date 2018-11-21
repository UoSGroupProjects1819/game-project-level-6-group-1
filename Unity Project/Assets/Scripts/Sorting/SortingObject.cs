using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sorting Object", menuName = "Items/Sorting Item")]
public class SortingObject : ScriptableObject
{
    public ItemType itemType = ItemType.None;
    [SerializeField] private float energyConsume;

    [HideInInspector] public enum ItemType { None, Vegetable, Fruit };

}
