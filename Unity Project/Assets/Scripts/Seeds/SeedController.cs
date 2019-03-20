using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeedController : MonoBehaviour
{
    [Header("Temp")]
    public Sprite inventorySprite;
    public float growthTime;

    [Header("Assigned by script.")]
    public InventoryItem item;
    public float percentComplete;
    public float waterInterval;
    public Image invImg;
    public float waterClickLength = 2.0f;
    public ParticleSystem waterGlow;

    public Outline itemOutline;

    private float remainTime = 0;
    private float targetTime = 0;
    private float currentTime = 0;
    private float currentWaterTimer = 0;

    private float pointerDownTimer;

    private bool finishedGrowing;
    private bool isGrowing;
    private bool isWatered;

    private bool pointerDown;


    // Start is called before the first frame update
    void Start()
    {
        currentWaterTimer = waterInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWatered && isGrowing && !finishedGrowing)
            currentWaterTimer -= Time.deltaTime;

        if (currentWaterTimer <= 0)
        {
            isWatered = false;
            //itemOutline.enabled = true;
            waterGlow.gameObject.SetActive(true);
            currentWaterTimer = waterInterval;
        }
    }

    public void LongInteract()
    {
        
    }

    public void Interact()
    {
        if (!isGrowing && !finishedGrowing)
            StartGrowth();

        if (isGrowing && !isWatered)
            WaterItem();
    }

    public void StartGrowth()
    {
        StopAllCoroutines();
        StartCoroutine(SeedGrowth(growthTime));
    }

    private void WaterItem()
    {
        currentWaterTimer = waterInterval;
        //itemOutline.enabled = false;
        waterGlow.gameObject.SetActive(false);
        isWatered = true;
    }

    IEnumerator SeedGrowth(float growthTime)
    {
        isGrowing = true;
        currentTime = Time.time;
        targetTime = Time.time + growthTime;

        do
        {
            if (isWatered)
            {
                currentTime = Time.time;

                percentComplete = currentTime / targetTime;
                invImg.fillAmount = percentComplete;

                if (currentTime >= targetTime)
                    finishedGrowing = true;
            }
            
            yield return null;
        } while (!finishedGrowing && isGrowing);

        percentComplete = Mathf.Round(percentComplete);

        finishedGrowing = true;
        isGrowing = false;
        yield break;
    }
}
