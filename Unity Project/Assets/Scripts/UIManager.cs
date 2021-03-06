﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("UI Manager Properties")]
    public GameObject[] UI_PANELS;

    [Header("Item Details Page")]
    [SerializeField] private TMPro.TMP_Text itemName;
    [SerializeField] private TMPro.TMP_Text growthTime;
    [SerializeField] private Image itemIcon;


    [Header("UI Elements")] 
    [SerializeField] private GameObject StartingSeeds;
    [SerializeField] private GameObject UI_Sidebar;
    [SerializeField] private GameObject UI_MSidebar;
    [SerializeField] private GameObject UI_Inventory;
    //[SerializeField] private GameObject UI_Sorting;
    [SerializeField] private GameObject UI_Journal;
    [SerializeField] private GameObject UI_PlanetBanner;
    [SerializeField] private GameObject UI_NewReward;
    [SerializeField] private TMPro.TMP_Text[] planetTexts;

    [Header("Journal Pages")]
    [SerializeField] private GameObject StatsPage;
    [SerializeField] private GameObject ItemPage;

    [SerializeField] private Canvas gameCanvas;

    [Header("Debug")]
    [SerializeField] private TMPro.TMP_Text[] statsTexts;
    [SerializeField] private TMPro.TMP_Text gameVer, currTime;

    //[HideInInspector] public System.DateTime startDate;

    private string planetName;

    private RewardNotification newItem;
    private RewardNotification newReward;

    private GameManager GM;

    #region Singleton
    public static UIManager instance = null;
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
        GM = GameManager.instance;

        //startDate = GM.GetStartDate;
        newReward   = UI_NewReward.GetComponent<RewardNotification>();

        gameVer.text = GM.GetBuildVersion;

        // Temporary
        //statsTexts[0].text = startDate.ToShortDateString();
        statsTexts[1].text = "Not yet functional.";
        statsTexts[2].text = "Not yet functional.";

        UI_NewReward.SetActive(false);
        StartingSeeds.SetActive(false);
        
        StartCoroutine(BeginGameUI());
    }

    public void UpdateUI()
    {
        planetName = GM.PlanetName;

        if (planetName == "")
            planetName = "Eos";

        foreach (TMPro.TMP_Text _text in planetTexts)
        {
            _text.text = planetName;
        }
    }

    private void Update()
    {
        currTime.text = System.DateTime.Now.ToShortTimeString();
    }

    public void ShowStartingSeeds()
    {
        StartingSeeds.SetActive(true);
    }

    //public void ChooseSeed(InventoryItem startingSeed)
    //{
    //    Inventory.instance.Add(startingSeed);
    //    StartingSeeds.SetActive(false);
    //}

    #region Button Functions
    public void _UIBasket()
    {
        foreach (GameObject _obj in UI_PANELS)
        {
            // Probably should have some code to tell each of those,
            // that they are no longer visible? Or something.
            _obj.SetActive(false);
        }
    }

    public void _ToggleInventoryUI()
    {
        GM.stopCameraMovement = !GM.stopCameraMovement;
        UI_Inventory.SetActive(!UI_Inventory.activeSelf);
        //UI_Sidebar.SetActive(!UI_Inventory.activeSelf);
    }

    public void _ToggleSidebarUI()
    {
        UI_Sidebar.SetActive(!UI_Sidebar.activeSelf);
        UI_MSidebar.SetActive(!UI_MSidebar.activeSelf);
    }

    public void _ToggleJournalUI()
    {
        UI_Sidebar.SetActive(!UI_Sidebar.activeSelf);
        UI_Journal.SetActive(!UI_Journal.activeSelf);
    }
    #endregion

    #region Notifications
    public void NewRewardNotif(InventoryItem item)
    {
        UI_NewReward.SetActive(true);
        newReward.DisplayNotification(item);
    }
    #endregion

    #region Journal Functions
    public void DisplayItem(InventoryItem item)
    {
        if (!ItemPage.activeSelf)
        {
            ItemPage.SetActive(!ItemPage.activeSelf);
            StatsPage.SetActive(!StatsPage.activeSelf);
        }

        itemName.text = item.name;
        itemIcon.sprite = item.itemIcon;
        growthTime.text = item.growthTime.ToString();
    }

    public void _StatsPage()
    {
        ItemPage.SetActive(false);
        StatsPage.SetActive(true);
    }
    #endregion

    IEnumerator BeginGameUI()
    {
        //StartCoroutine(FadeOutBanner(2.0f, 0.7f));

        yield return new WaitForSeconds(.2f);

        //UI_Sidebar.SetActive(true);
    }

    //IEnumerator FadeOutBanner(float bannerDelay, float fadeTime)
    //{
    //    yield return new WaitForSeconds(bannerDelay);
    //    UI_PlanetBanner.GetComponent<Image>().CrossFadeAlpha(0.0f, fadeTime, true);

    //    yield return new WaitForSeconds(fadeTime);
    //    UI_PlanetBanner.SetActive(false);
    //}
}
