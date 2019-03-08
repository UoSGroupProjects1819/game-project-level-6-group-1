using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedController : MonoBehaviour
{
    [Header("Temp")]
    public Sprite inventorySprite;
    public float growthTime;

    [Header("Assigned by script.")]
    public InventoryItem item;
    public float percentComplete;
    public Image invImg;

    [SerializeField] private float remainTime = 0;
    private float targetTime = 0;
    private float currentTime = 0;

    private bool finishedGrowing;
    private bool isGrowing;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        //if (!isGrowing && !finishedGrowing)
            StartGrowth();
    }

    public void StartGrowth()
    {
        StopAllCoroutines();
        StartCoroutine(SeedGrowth(growthTime));
    }

    IEnumerator SeedGrowth(float growthTime)
    {
        //isGrowing = true;
        currentTime = Time.time;
        targetTime = Time.time + growthTime;

        do
        {
            currentTime = Time.time;

            percentComplete = currentTime / targetTime;
            invImg.fillAmount = percentComplete;

            if (currentTime >= targetTime)
                finishedGrowing = true;

            yield return null;
        } while (!finishedGrowing);

        percentComplete = Mathf.Round(percentComplete);

        finishedGrowing = true;
        yield break;
    }
}
