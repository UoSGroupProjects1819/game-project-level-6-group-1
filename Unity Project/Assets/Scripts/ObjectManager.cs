using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour {

    [SerializeField] private SpriteRenderer sprRenderer;
    [SerializeField] private Sprite finishedSprite;
    [SerializeField] private Sprite growingSprite;
    [SerializeField] private float growthTime;
    [SerializeField] private float targetTime;
    [SerializeField] private Vector3 desiredSize;
    [SerializeField] private GameObject collectStar;
    [SerializeField] private TMPro.TMP_Text timeText;

    private float remainTime;
    private float currentTime;
    private bool finishedGrowing;
    private GameObject playerPlanet;
    private GameObject timePanel;

    void Start()
    {
        collectStar.SetActive(false);
        StartCoroutine(ManageGrowth(growthTime));

        playerPlanet = GameObject.FindGameObjectWithTag("Player");


        Vector3 difference = playerPlanet.transform.position - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle + 90.0f));

        timePanel = timeText.transform.parent.gameObject;
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

    private void Update()
    {
        if (Camera.main.orthographicSize < 3)
        {
            timePanel.GetComponent<Image>().CrossFadeAlpha(1.0f, 0.2f, true);
            timeText.CrossFadeAlpha(1.0f, 0.2f, true);
            DisplayTime(remainTime, timeText);
        }
        else
        {
            timePanel.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.2f, true);
            timeText.CrossFadeAlpha(0.0f, 0.2f, true);
        } 
    }

    private void DisplayTime (float timeInSeconds, TMPro.TMP_Text targetText)
    {
        int seconds = (int)(timeInSeconds % 60);
        int minutes = (int)(timeInSeconds / 60) % 60;
        int hours = (int)(timeInSeconds / 3600) % 24;

        string timeString = string.Format("{0:00}h {1:00}m {2:00}s", hours, minutes, seconds);

        timeText.text = timeString;
    }

    IEnumerator ManageGrowth (float time)
    {
        Vector3 startingSize = gameObject.transform.localScale;
        currentTime = 0.0f;

        targetTime = Time.time + growthTime;

        do
        {
            gameObject.transform.localScale = Vector3.Lerp(startingSize, desiredSize, currentTime / targetTime);
            currentTime = Time.time;
            remainTime = targetTime - currentTime;
            yield return null;
        } while (currentTime <= targetTime);

        sprRenderer.sprite = finishedSprite;

        timeText.transform.parent.gameObject.SetActive(false);
        collectStar.SetActive(true);
        finishedGrowing = true;

        StopCoroutine(ManageGrowth(0));
    }
}
