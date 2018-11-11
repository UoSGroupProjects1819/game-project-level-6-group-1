using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    [SerializeField] private SpriteRenderer sprRenderer;
    [SerializeField] private Sprite finishedSprite;
    [SerializeField] private Sprite growingSprite;
    [SerializeField] private float growthTime;
    [SerializeField] private float targetTime;
    [SerializeField] private Vector3 desiredSize;
    [SerializeField] private GameObject collectStar;

    private bool finishedGrowing;
    private GameObject playerPlanet;

	void Start ()
    {
        collectStar.SetActive(false);
        StartCoroutine(ManageGrowth(growthTime));

        playerPlanet = GameObject.FindGameObjectWithTag("Player");


        Vector3 difference = playerPlanet.transform.position - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle + 90.0f));
	}

    private void OnMouseDown()
    {
        if (finishedGrowing)
        {
            collectStar.SetActive(false);
            Energy.instance.AddEnergy(1);
        }
    }

    //private void OnMouseDrag()
    //{
    //    /* Moving the object, after it has been placed. */
    //    // Get the mouse position, and convert it to world point.
    //    Vector3 movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    movePos.z = 0;

    //    // Calculate rotation, so that object faces center of the planet.
    //    Vector3 difference = playerPlanet.transform.position - transform.position;
    //    float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

    //    // Apply the rotation and position.
    //    transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle + 90.0f));
    //    transform.position = movePos;
    //}

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
        collectStar.SetActive(true);
        finishedGrowing = true;
        StopCoroutine(ManageGrowth(0));
    }
}
