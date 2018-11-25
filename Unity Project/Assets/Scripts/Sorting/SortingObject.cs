using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SortingObject : MonoBehaviour, IDragHandler {

    [HideInInspector] public SortingItem scrObject;
    [HideInInspector] public int itemType = 0;

    private int energyConsume;
    private Image objectGraphic;
    private RectTransform rectTransform;
    private BoxCollider2D objectCollider;

    private void Start()
    {
        rectTransform   = GetComponent<RectTransform>();
        objectCollider  = GetComponent<BoxCollider2D>();
        objectGraphic   = GetComponentInChildren<Image>();

        InitializeVariables();
    }

    private void InitializeVariables()
    {
        itemType = scrObject.GetItemType();
        if (itemType == 0)
            Debug.Log("Item type not set/Invalid item type, on " + gameObject.name);

        objectGraphic.sprite = scrObject.objSprite;
        energyConsume = scrObject.energyConsume;
    }

    private void Update()
    {
        objectCollider.size = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
    } 

    public void OnDrag(PointerEventData eventData)
    {
        if (Energy.instance.hasEnergy)
        {
            Vector3 mousePos = eventData.position;
            mousePos.z = 0;

            transform.position = mousePos;
        } else { Debug.Log("No energy!"); return; }
    }

    //private void OnMouseDrag()
    //{
    //    if (Energy.instance.hasEnergy)
    //    {
    //        //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    //        //Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePosition);
    //        //objPos.z = 0;

    //        Vector3 mousePos = Input.mousePosition;
    //        mousePos.z = 0.0f;

    //        transform.position = mousePos;
    //    } else { Debug.Log("No energy.");  return; }
    //}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        // Prolly have the system take care of this rather than the item itself.
    }
}
