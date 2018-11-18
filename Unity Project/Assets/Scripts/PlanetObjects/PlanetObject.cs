using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetObject : MonoBehaviour {

    [Tooltip("This is the start that players will collect for additional energy.")]
    [SerializeField] private GameObject energyStar;
    [Tooltip("This is the timer that displays how much longer the object has left to grow.")]
    [SerializeField] private TMPro.TMP_Text timerText;

    [HideInInspector] public Item scrObject;

    private SpriteRenderer sprRenderer;
    private Sprite growingSprite;
    private Sprite finishedSprite;

    private float growthTime;
    private float targetTime;
    private float remainTime;
    private float currentTime;

    private bool finishedGrowing;

    private GameObject playerPlanet;
    private GameObject timerParent;
    private Vector3 desiredSize;
    private Vector3 startSize;

    private void Start()
    {
        // Set the references when object is instantiated.
        sprRenderer = GetComponent<SpriteRenderer>();
        timerParent = timerText.transform.parent.gameObject;
        playerPlanet = GameManager.instance.playerPlanet;

        transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);

        energyStar.SetActive(false);

        // Make sure we have the scriptable object to get data from.
        //if (scriptableObject != null)
        InitializeVariables();

        SetObjectRotation();
    }

    private void Update()
    {
        if (Camera.main.orthographicSize < 3)
        {
            timerParent.GetComponent<Image>().CrossFadeAlpha(1.0f, 0.2f, true);
            timerText.CrossFadeAlpha(1.0f, 0.2f, true);
            UpdateTime(remainTime, timerText);
        }
        else
        {
            timerParent.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.2f, true);
            timerText.CrossFadeAlpha(0.0f, 0.2f, true);
        }
    }

    private void InitializeVariables()
    {
        // Get the variables from the scriptable object.
        growingSprite   = scrObject.objectSprites[0];
        finishedSprite  = scrObject.objectSprites[1];

        growthTime = scrObject.growthTime;

        StartCoroutine(ManageGrowth(growthTime));
    }

    private void SetObjectRotation()
    {
        Vector3 difference = playerPlanet.transform.position - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(
            transform.rotation.x, transform.rotation.y, angle + 90.0f));
    }

    private void UpdateTime(float timeInSeconds, TMPro.TMP_Text targetText)
    {
        int seconds = (int)(timeInSeconds % 60);
        int minutes = (int)(timeInSeconds / 60) % 60;
        int hours = (int)(timeInSeconds / 3600) % 24;

        string timeString = string.Format("{0:00}h {1:00}m {2:00}s", hours, minutes, seconds);

        targetText.text = timeString;
    }

    private void OnMouseDown()
    {
        if (finishedGrowing)
        {
            energyStar.SetActive(false);
            Energy.instance.AddEnergy(1);
        }
    }

    IEnumerator ManageGrowth(float growthTime)
    {
        startSize   = gameObject.transform.localScale;
        desiredSize = new Vector3(1.0f, 1.0f, 1.0f);

        currentTime = 0.0f;
        targetTime  = Time.time + growthTime;

        sprRenderer.sprite = growingSprite;

        do
        {
            gameObject.transform.localScale = Vector3.Lerp(startSize, desiredSize, currentTime / targetTime);

            currentTime = Time.time;
            remainTime = targetTime - currentTime;

            yield return null;
        } while (currentTime <= targetTime);

        sprRenderer.sprite = finishedSprite;

        timerParent.SetActive(false);
        energyStar.SetActive(true);

        finishedGrowing = true;

        StopCoroutine(ManageGrowth(0));
    }
}
