using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    [Header("Player Rewards")]
    [SerializeField] private InventoryItem[] vegetableRewards;
    [SerializeField] private InventoryItem[] fruitRewards;

    [SerializeField] private SortingItem[] sortingObjects;
    [SerializeField] private GameObject sortingObjectPrefab;
    [SerializeField] private GameObject itemsParent;

    [HideInInspector] public int itemsSortedCorrectly;

    private GameObject spawnedItem;
    private InventoryItem reward;

    private Energy energyInstance;
    private Inventory inventoryInstance;

    UIManager UI;

    #region Singleton
    public static Sorting instance;
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
        UI                  = UIManager.instance;
        inventoryInstance   = Inventory.instance;
        energyInstance      = Energy.instance;

        SpawnNewObject();
    }

    private void Update()
    {
        if (itemsSortedCorrectly == 1)
        {
            reward = RewardPlayer();
            bool playerRewarded = inventoryInstance.Add(reward);

            if (!playerRewarded || reward == null)
                Debug.Log("Could not reward player!");

            if (Journal.instance.journalItems.Contains(reward))
            {
                UI.NewItemNotif(reward);
            }
            else
            {
                UI.NewRewardNotif(reward);
                Journal.instance.Add(reward);
            }

            itemsSortedCorrectly = 0;
            reward = null;
        }
    }

    public void SortedCorrectly(GameObject item)
    {
        energyInstance.RemoveEnergy(1);
        itemsSortedCorrectly++;

        Destroy(item);
        SpawnNewObject();
    }

    public void SortedIncorrectly(GameObject item)
    {
        energyInstance.RemoveEnergy(1);

        Destroy(item);
        SpawnNewObject();
    }

    private InventoryItem RewardPlayer()
    {
        float total = 0;
        InventoryItem[] itemArray = SelectCategory();

        if (itemArray == null)
            Debug.LogError("Reward array is empty, could not SelectCategory!");


        foreach (InventoryItem _item in itemArray)
        {
            total += _item.probability;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < itemArray.Length; i++)
        {
            if (randomPoint < itemArray[i].probability)
            {
                //Debug.Log("Player rewarded " + itemArray[i].name);
                return itemArray[i];
            }
            else
            {
                randomPoint -= itemArray[i].probability;
            }
        }

        //Debug.Log("Reward not found, rewarding last item.");
        return itemArray[itemArray.Length -1];
    }

    public InventoryItem[] SelectCategory()
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
        yield return new WaitForSeconds(.05f);

        spawnedItem = Instantiate(sortingObjectPrefab, itemsParent.transform, false);
        spawnedItem.GetComponent<SortingObject>().scrObject = sortingObjects[Random.Range(0, sortingObjects.Length)];
            
        //Instantiate(sortingObjects[Random.Range(0, sortingObjects.Length)], itemsParent.transform, false);

        StopCoroutine(SpawnNewItem());
    }
}
