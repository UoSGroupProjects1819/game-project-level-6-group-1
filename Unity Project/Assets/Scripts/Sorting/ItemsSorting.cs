using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSorting : MonoBehaviour
{
    [SerializeField] private Item[] rewards;
    [SerializeField] private GameObject[] sortingObjects;
    [SerializeField] private TMPro.TMP_Text debugSortedCounter;
    [SerializeField] private GameObject itemsParent;

    [HideInInspector] public int itemsSortedCorrectly;

    private bool spawnedObject = false;
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
        if (GameManager.instance.enableSorting)
            SpawnNewObject();

        StartCoroutine(TestReward());
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
        Debug.Log("Sorted correctly: " + item.name);

        Energy.instance.RemoveEnergy(1);
        itemsSortedCorrectly++;
        UpdateUI();

        Destroy(item);
        SpawnNewObject();
    }

    public void SortedIncorrectly(GameObject item)
    {
        Debug.Log("Sorted incorrectly: " + item.name);

        Energy.instance.RemoveEnergy(1);
        UpdateUI();

        Destroy(item);
        SpawnNewObject();
    }

    private void RewardPlayer()
    {
        Item item = rewards[0];
        bool wasPickedUp = Inventory.instance.Add(item);

        Debug.Log("Rewarding player: " + item.name);
    }

    private void SpawnNewObject()
    {
        StartCoroutine(SpawnNewItem());
    }

    private void UpdateUI()
    {
        debugSortedCounter.text = itemsSortedCorrectly.ToString();
    }

    IEnumerator SpawnNewItem()
    {
        yield return new WaitForSeconds(1.5f);

        GameObject newSortItem = Instantiate(sortingObjects[Random.Range(0, sortingObjects.Length)], itemsParent.transform, false);
        spawnedItem = newSortItem;

        StopCoroutine(SpawnNewItem());
    }

    IEnumerator TestReward()
    {
        yield return new WaitForSeconds(.1f);

        RewardPlayer();

        StopCoroutine(TestReward());
    }
}
