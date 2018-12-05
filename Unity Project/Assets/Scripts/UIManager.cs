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

    [SerializeField] private Canvas gameCanvas;

    [Header("Debug")]
    [SerializeField] private string planetName = "Foobar";

    private RewardNotification rewardNotif;

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
        planetName = GameManager.instance.planetName;

        foreach (TMPro.TMP_Text _text in planetTexts)
        {
            _text.text = planetName;
        }

        rewardNotif = UI_RewardNotification.GetComponent<RewardNotification>();

        StartCoroutine(BeginGameUI());
    }

    public void ToggleSortingUI()
    {
        UI_Sorting.SetActive(!UI_Sorting.activeSelf);
        UI_Sidebar.SetActive(!UI_Sorting.activeSelf);
    }

    public void ToggleInventoryUI()
    {
        GameManager.instance.enableCameraMovement = !GameManager.instance.enableCameraMovement;
        UI_Inventory.SetActive(!UI_Inventory.activeSelf);
        UI_Sidebar.SetActive(!UI_Inventory.activeSelf);
    }

    public void ToggleSidebarUI()
    {
        UI_Sidebar.SetActive(!UI_Sidebar.activeSelf);
        UI_MSidebar.SetActive(!UI_MSidebar.activeSelf);
    }

    public void ToggleJournalUI()
    {
        UI_Sidebar.SetActive(!UI_Sidebar.activeSelf);
        UI_Journal.SetActive(!UI_Journal.activeSelf);
    }

    public void RewardNotif(InventoryItem item)
    {
        rewardNotif.transform.gameObject.SetActive(true);
        rewardNotif.DisplayNotification(item);
    }

    #region Journal Functions
    public void DisplayItem(InventoryItem item)
    {
        itemName.text = item.name;
        itemIcon.sprite = item.itemIcon;
        growthTime.text = item.growthTime.ToString();
    }
    #endregion

    public void GameUI()
    {
        UI_MSidebar.SetActive(true);
    }

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
