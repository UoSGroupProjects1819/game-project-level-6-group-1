using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingCategory : MonoBehaviour {

    public Category category = Category.None;
    public enum Category { None, Vegetable, Fruit };

    private int itemType;

    private void Start()
    {
        switch (category)
        {
            case Category.None:
                itemType = 0;
                break;
            case Category.Vegetable:
                itemType = 1;
                break;
            case Category.Fruit:
                itemType = 2;
                break;
        }

        if (itemType == 0)
            Debug.LogError("No category selected for: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "SortingObject")
        {
            if (coll.GetComponent<SortingObject>())
            {
                if (itemType == coll.GetComponent<SortingObject>().itemType)
                {
                    Sorting.instance.SortedCorrectly(coll.gameObject);
                }
                else
                {
                    Sorting.instance.SortedIncorrectly(coll.gameObject);
                }
            }
        }
        else { return; }
    }
}
