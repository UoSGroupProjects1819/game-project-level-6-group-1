using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSorting : MonoBehaviour
{
    [Header("Player Rewards")]
    [SerializeField] private Item[] vegetableRewards;
    [SerializeField] private Item[] fruitRewards;

    [SerializeField] private GameObject[] sortingObjects;
    [SerializeField] private GameObject itemsParent;

    [HideInInspector] public int itemsSortedCorrectly;

    private bool spawnedObject = false;
    private bool rewarded = false;
    private GameObject spawnedItem;

    #region Singleton
    public static ItemsSorting instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        //if (GameManager.instance.enableSorting)
            SpawnNewObject();

        RewardPlayer();
    }

    private void Update()
    {
        if (itemsSortedCorrectly == 5)
        {
            RewardPlayer();
            itemsSortedCorrectly = 0;
        }
    }

    public void SortedCorrectly(GameObject item)
    {
        Energy.instance.RemoveEnergy(1);
        itemsSortedCorrectly++;

        Destroy(item);
        SpawnNewObject();
    }

    public void SortedIncorrectly(GameObject item)
    {
        Energy.instance.RemoveEnergy(1);

        Destroy(item);
        SpawnNewObject();
    }

    private void RewardPlayer()
    {
        int rand = Random.Range(0, 2);
        Item item = null;

        if (rand == 0)
        {
            item = vegetableRewards[Random.Range(0, vegetableRewards.Length)];
        }

        if (rand == 1)
        {
            item = fruitRewards[Random.Range(0, fruitRewards.Length)];
        }

        if (item == null)
            Debug.LogError("No reward found! Could not chose a reward!");

        bool wasPickedUp = Inventory.instance.Add(item);
        rewarded = true;

        Debug.Log("Rewarding player: " + item.name);
    }

    private void SpawnNewObject()
    {
        StartCoroutine(SpawnNewItem());
    }

    IEnumerator SpawnNewItem()
    {
        yield return new WaitForSeconds(.5f);

        GameObject newSortItem = Instantiate(sortingObjects[Random.Range(0, sortingObjects.Length)], itemsParent.transform, false);
        spawnedItem = newSortItem;

        StopCoroutine(SpawnNewItem());
    }
}
