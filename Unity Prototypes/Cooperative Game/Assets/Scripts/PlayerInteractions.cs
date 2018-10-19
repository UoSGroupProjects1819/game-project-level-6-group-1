using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

    [SerializeField] [Tooltip("This is the position, where the object will move to when picked up.")]
    private GameObject backpackObject;
    [SerializeField] [Tooltip("Time that needs to pass before player can interact again. This value > .1, otherwise players will pick up and drop items at the same frame.")]
    private float pickupDelay = .5f;

    private PlayerMovement pMovement;

    private GameObject currentItem;
    private GameObject tempItem;

    private bool canInteract    = true;
    private bool isHolding      = false;
    private bool onPickup       = false;

    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();

        if (backpackObject == null)
            Debug.LogError("Missing backpack position for player: " + gameObject.name);
    }

    private void Update()
    {
        if (!isHolding)
        {
            if (pMovement.isPlayerOne && Input.GetKeyDown(KeyCode.Space)
                && onPickup && canInteract)
            {
                PickupItem(tempItem);
            }
            else if (pMovement.isPlayerTwo && Input.GetKeyDown(KeyCode.Return)
                && onPickup && canInteract)
            {
                PickupItem(tempItem);
            }
        }

        if (isHolding)
        {
            if (pMovement.isPlayerOne && Input.GetKeyDown(KeyCode.Space) && canInteract)
            {
                DropItem();
            }
            else if (pMovement.isPlayerTwo && Input.GetKeyDown(KeyCode.Return) && canInteract)
            {
                DropItem();
            }
        }
    }

    private void PickupItem(GameObject item)
    {
        Debug.Log("Picked up");

        canInteract = false;
        isHolding = true;
        currentItem = item;
        item.transform.parent = backpackObject.transform;
        item.transform.position = backpackObject.transform.position;

        StartCoroutine(InteractionDelay(pickupDelay));
    }

    private void DropItem()
    {
        Debug.Log("Dropped item");

        canInteract = false;
        isHolding = false;
        currentItem.transform.position = transform.position;
        currentItem.transform.parent = null;
        currentItem = null;

        StartCoroutine(InteractionDelay(pickupDelay));
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Collided.");

        if (coll.gameObject.tag == "Pickup")
        {
            Debug.Log("With a pickup.");
            tempItem = coll.gameObject;
            onPickup = true;
        }

        if (coll.gameObject.tag == "ForceField")
        {
            DropItem();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (tempItem != null)
            tempItem = null;

        onPickup = false;
    }

    IEnumerator InteractionDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        canInteract = true;
    }
}
