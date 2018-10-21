using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    [SerializeField] private SpriteRenderer sprRenderer;
    [SerializeField] private Sprite finishedSprite;
    [SerializeField] private Sprite growingSprite;
    [SerializeField] private float growthTime;
    [SerializeField] private float targetTime;
    [SerializeField] Vector3 desiredSize;

    private GameObject playerPlanet;

	void Start ()
    {
        StartCoroutine(ManageGrowth(growthTime));

        playerPlanet = GameObject.FindGameObjectWithTag("Player");
	}

    private void OnMouseDrag()
    {
        /* Moving the object, after it has been placed. */
        // Get the mouse position, and convert it to world point.
        Vector3 movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movePos.z = 0;

        // Calculate rotation, so that object faces center of the planet.
        Vector3 difference = playerPlanet.transform.position - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // Apply the rotation and position.
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle + 90.0f));
        transform.position = movePos;
    }

    IEnumerator ManageGrowth (float time)
    {
        Vector3 startingSize = gameObject.transform.localScale;
        float currentTime = 0.0f;

        targetTime = Time.time + growthTime;

        do
        {
            gameObject.transform.localScale = Vector3.Lerp(startingSize, desiredSize, currentTime / targetTime);
            currentTime = Time.time;
            yield return null;
        } while (currentTime <= targetTime);

        sprRenderer.sprite = finishedSprite;
        StopCoroutine(ManageGrowth(0));
    }
}
