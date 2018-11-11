using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingItem : MonoBehaviour
{
    [SerializeField] private ItemType currentItem = ItemType.None;
    private enum ItemType { None, Vegetable, Fruit };

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
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePosition);
        objPos.z = 0;

        transform.position = objPos;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Vegetable" && currentItem == ItemType.Vegetable)
        {
            ItemsSorting.instance.SortedCorrectly(gameObject);
        }

        if (coll.tag == "Fruit" && currentItem == ItemType.Fruit)
        {
            ItemsSorting.instance.SortedCorrectly(gameObject);
        }
    }
}
