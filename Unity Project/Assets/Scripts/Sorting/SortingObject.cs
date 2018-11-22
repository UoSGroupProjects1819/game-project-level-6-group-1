using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingObject : MonoBehaviour {

    [HideInInspector] public SortingItem scrObject;

    private int itemType = 0;
    private int energyConsume;
    private SpriteRenderer sprRenderer;
    private RectTransform rectTransform;
    private BoxCollider2D objectCollider;

    private void Start()
    {
        rectTransform   = GetComponent<RectTransform>();
        objectCollider  = GetComponent<BoxCollider2D>();
        sprRenderer     = GetComponentInChildren<SpriteRenderer>();

        InitializeVariables();
    }

    private void InitializeVariables()
    {
        itemType = scrObject.GetItemType();
        if (itemType == 0)
            Debug.Log("Item type not set/Invalid item type, on " + gameObject.name);

        sprRenderer.sprite = scrObject.objSprite;
        energyConsume = scrObject.energyConsume;
    }

    private void Update()
    {
        objectCollider.size = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
    }

    private void OnMouseDrag()
    {
        if (Energy.instance.hasEnergy)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePosition);
            objPos.z = 0;

            transform.position = objPos;
        } else { Debug.Log("No energy.");  return; }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        // Prolly have the system take care of this rather than the item itself.
    }
}
