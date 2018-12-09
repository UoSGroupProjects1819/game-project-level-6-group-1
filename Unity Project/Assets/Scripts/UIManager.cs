using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("Item Details Page")]
    [SerializeField] private TMPro.TMP_Text itemName;
    [SerializeField] private TMPro.TMP_Text growthTime;
    [SerializeField] private Image itemIcon;


    [Header("UI Elements")]
    [SerializeField] private GameObject UI_Sidebar;
    [SerializeField] private GameObject UI_MSidebar;
    [SerializeField] private GameObject UI_Inventory;
    [SerializeField] private GameObject UI_Sorting;
    [SerializeField] private GameObject UI_Journal;
    [SerializeField] private GameObject UI_PlanetBanner;
    [SerializeField] private GameObject UI_RewardNotification;
    [SerializeField] private TMPro.TMP_Text[] planetTexts;

    [Header("Journal Pages")]
    [SerializeField] private GameObject StatsPage;
    [SerializeField] private GameObject ItemPage;

    [SerializeField] private Canvas gameCanvas;

    [Header("Debug")]
    [SerializeField] private TMPro.TMP_Text[] statsTexts;
    [SerializeField] private TMPro.TMP_Text gameVer;

    private string planetName;

    private RewardNotification _RewardNotification;
    private GameManager _GameManager;

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
        _RewardNotification = UI_RewardNotification.GetComponent<RewardNotification>();
        _GameManager = GameManager.instance;

        gameVer.text = _GameManager.gameVer;
        planetName = _GameManager.planetName;

        foreach (TMPro.TMP_Text _text in planetTexts)
        {
            _text.text = planetName;
        }

        statsTexts[0].text = System.DateTime.Now.ToShortDateString();
        statsTexts[1].text = "Not yet functional.";
        statsTexts[2].text = "Not yet functional.";


        StartCoroutine(BeginGameUI());
    }

    public void _ToggleSortingUI()
    {
        UI_Sorting.SetActive(!UI_Sorting.activeSelf);
        UI_Sidebar.SetActive(!UI_Sorting.activeSelf);
    }

    public void _ToggleInventoryUI()
    {
        _GameManager.enableCameraMovement = !_GameManager.enableCameraMovement;
        UI_Inventory.SetActive(!UI_Inventory.activeSelf);
        UI_Sidebar.SetActive(!UI_Inventory.activeSelf);
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

    public void RewardNotif(InventoryItem item)
    {
        _RewardNotification.transform.gameObject.SetActive(true);
        _RewardNotification.DisplayNotification(item);
    }

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

    [System.Obsolete("This function will be removed in future updates, avoid using it. Leftover after old UI.")]
    public void GameUI()
    {
        UI_MSidebar.SetActive(true);
    }

    /* Organise the two below */

    IEnumerator BeginGameUI()
    {
        StartCoroutine(FadeOutBanner(2.0f, 0.7f));

        yield return new WaitForSeconds(2.5f);

        UI_Sidebar.SetActive(true);
    }

    IEnumerator FadeOutBanner(float bannerDelay, float fadeTime)
    {
        yield return new WaitForSeconds(bannerDelay);
        UI_PlanetBanner.GetComponent<Image>().CrossFadeAlpha(0.0f, fadeTime, true);

        yield return new WaitForSeconds(fadeTime);
        UI_PlanetBanner.SetActive(false);
    }
}
