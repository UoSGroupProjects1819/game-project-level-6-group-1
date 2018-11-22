﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSortingItem : MonoBehaviour
{
    public ItemType currentItem = ItemType.None;
    public enum ItemType { None, Vegetable, Fruit };

    private void Start()
    {
        switch (currentItem)
        {
            case ItemType.None:
                Debug.LogError("No sorting item type selected on: " + gameObject.name);
                break;
        }
    }

    public void OnMouseDrag()
    {
        if (Energy.instance.hasEnergy)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePosition);
            objPos.z = 0;

            transform.position = objPos;
        }
        else { return; }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            if (coll.transform.tag == "Vegetable" && currentItem == ItemType.Vegetable)
            {
                Sorting.instance.SortedCorrectly(gameObject);
            }
            else { Sorting.instance.SortedIncorrectly(gameObject); }

            if (coll.tag == "Fruit" && currentItem == ItemType.Fruit)
            {
                Sorting.instance.SortedCorrectly(gameObject);
            }
            //else { ItemsSorting.instance.SortedIncorrectly(gameObject); }
        }
    }
}
