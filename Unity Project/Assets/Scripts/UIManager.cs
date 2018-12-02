using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    [SerializeField] private GameObject UI_Sidebar;
    [SerializeField] private GameObject UI_MSidebar;
    [SerializeField] private GameObject UI_Inventory;
    [SerializeField] private GameObject UI_Sorting;
    [SerializeField] private GameObject UI_Journal;
    [SerializeField] private GameObject UI_PlanetBanner;
    [SerializeField] private TMPro.TMP_Text[] planetTexts;

    [SerializeField] private Canvas gameCanvas;

    [Header("Debug")]
    [SerializeField] private string planetName = "Foobar";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    /* FUNCTIONS FOR THE BUTTONS */

    //public void _StartGame()
    //{
    //    GameManager.instance.LoadGame();
    //}

    private void Start()
    {
        foreach (TMPro.TMP_Text _text in planetTexts)
        {
            _text.text = planetName;
        }


        UI_Sidebar.SetActive(true);

        StartCoroutine(FadeOutBanner(2.0f, 0.7f));
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
        //StartCoroutine(ToggleSidebarCR(2.0f));
    }

    public void ToggleJournalUI()
    {
        UI_Sidebar.SetActive(!UI_Sidebar.activeSelf);
        UI_Journal.SetActive(!UI_Journal.activeSelf);
    }

    /* CONTROLLING THE UI */

    //public IEnumerator ToggleSidebarCR(float delay)
    //{
    //    if (!sidebarUI.activeSelf || inventoryUI.activeSelf || sortingUI.activeSelf)
    //        yield break;

    //    yield return new WaitForSeconds(delay);

    //    ToggleSidebarUI();
    //}

    //public void ShowCreationMenu()
    //{
    //    GameObject[] _MainMenu = GameObject.FindGameObjectsWithTag("MainMenu");
    //    foreach (GameObject _go in _MainMenu)
    //    {
    //        _go.SetActive(false);
    //    }
    //}

    public void GameUI()
    {
        UI_MSidebar.SetActive(true);
    }


    IEnumerator FadeOutBanner(float bannerDelay, float fadeTime)
    {
        yield return new WaitForSeconds(bannerDelay);
        UI_PlanetBanner.GetComponent<Image>().CrossFadeAlpha(0.0f, fadeTime, true);

        yield return new WaitForSeconds(fadeTime);
        UI_PlanetBanner.SetActive(false);
    }
}
