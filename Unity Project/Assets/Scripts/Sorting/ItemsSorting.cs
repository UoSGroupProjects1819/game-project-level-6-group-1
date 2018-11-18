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
        if (itemsSortedCorrectly == 1)
        {
            bool playerRewarded = Inventory.instance.Add(RewardPlayer());

            if (playerRewarded)
                rewarded = true;

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

    private Item RewardPlayer()
    {
        float total = 0;
        Item[] itemArray = SelectCategory();

        if (itemArray == null)
            Debug.LogError("Reward array is empty, could not SelectCategory!");


        foreach (Item _item in itemArray)
        {
            total += _item.probability;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < itemArray.Length; i++)
        {
            if (randomPoint < itemArray[i].probability)
            {
                Debug.Log("Player rewarded " + itemArray[i].name);
                return itemArray[i];
            }
            else
            {
                randomPoint -= itemArray[i].probability;
            }
        }

        Debug.Log("Reward not found, rewarding last item.");
        return itemArray[itemArray.Length -1];
    }

    public Item[] SelectCategory()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
            return vegetableRewards;
        else if (rand == 1)
            return fruitRewards;
        else
            return null;
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
