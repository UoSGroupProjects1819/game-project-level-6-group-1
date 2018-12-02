using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetObject : MonoBehaviour {

    [Header("Object Properties")]
    [Tooltip("This is the start that players will collect for additional energy.")]
    [SerializeField] private GameObject energyStar;
    [Tooltip("This is the timer that displays how much longer the object has left to grow.")]
    [SerializeField] private TMPro.TMP_Text timerText;

    [Header("Debug Stuff")]
    [SerializeField] private bool enableWatering = false;

    public InventoryItem scrObject;

    private JournalItem journalReward;

    private SpriteRenderer sprRenderer;
    private Sprite growingSprite;
    private Sprite finishedSprite;

    private float growthTime;
    private float targetTime;
    private float remainTime;
    private float currentTime;
    private float elapsedTime;

    private float waterInterval;
    private float currentWaterTimer;

    private bool finishedGrowing;
    private bool isWatered;

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

        if (scrObject.journalEntry != null)
        {
            journalReward = scrObject.journalEntry;
            Journal.instance.Add(scrObject);
        }

        transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);

        isWatered = true;
        currentWaterTimer = waterInterval;

        energyStar.SetActive(false);

        InitializeVariables();
        SetObjectRotation();
    }

    private void Update()
    {
        if (isWatered && enableWatering)
            currentWaterTimer -= Time.deltaTime;

        if (currentWaterTimer <= 0)
        {
            currentWaterTimer = waterInterval;
            isWatered = false;
        }

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
        growingSprite   = scrObject.objectSprites[0];
        finishedSprite  = scrObject.objectSprites[1];

        growthTime = scrObject.growthTime;
        waterInterval = scrObject.wateringInterval;

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

        if (!isWatered)
        {
            isWatered = true;
            currentWaterTimer = waterInterval;
        }
    }

    private bool CheckForWateredBool()
    {
        if (isWatered && enableWatering)
        {
            targetTime = Time.time + remainTime;
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator ManageGrowth(float growthTime)
    {
        startSize   = gameObject.transform.localScale;
        desiredSize = new Vector3(1.0f, 1.0f, 1.0f);

        currentTime = 0.0f;
        targetTime  = Time.time + growthTime;

        currentWaterTimer = waterInterval;
        sprRenderer.sprite = growingSprite;

        do
        {
            if (!isWatered && enableWatering)
                yield return new WaitUntil(CheckForWateredBool);

            gameObject.transform.localScale = Vector3.Lerp(startSize, desiredSize, currentTime / targetTime);

            currentTime = Time.time;
            remainTime  = targetTime - currentTime;

            if (currentTime == targetTime)
                finishedGrowing = true;

            yield return null;
        } while (currentTime <= targetTime);


        sprRenderer.sprite = finishedSprite;
        finishedGrowing = true;

        timerParent.SetActive(false);
        energyStar.SetActive(true);

        StopCoroutine(ManageGrowth(0));
    }
}
