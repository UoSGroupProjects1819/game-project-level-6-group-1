using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject sidebarUI;
    [SerializeField] private GameObject sortingUI;
    [SerializeField] private GameObject miniSidebar;

    [SerializeField] private Canvas gameCanvas;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    /* FUNCTIONS FOR THE BUTTONS */

    public void _StartGame()
    {
        GameManager.instance.LoadGame();
    }

    public void ToggleSortingUI()
    {
        sortingUI.SetActive(!sortingUI.activeSelf);
        sidebarUI.SetActive(!sortingUI.activeSelf);
    }

    public void ToggleInventoryUI()
    {
        GameManager.instance.enableCameraMovement = !GameManager.instance.enableCameraMovement;
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        sidebarUI.SetActive(!inventoryUI.activeSelf);
    }

    public void ToggleSidebarUI()
    {
        sidebarUI.SetActive(!sidebarUI.activeSelf);
        miniSidebar.SetActive(!miniSidebar.activeSelf);
        //StartCoroutine(ToggleSidebarCR(2.0f));
    }

    /* CONTROLLING THE UI */

    //public IEnumerator ToggleSidebarCR(float delay)
    //{
    //    if (!sidebarUI.activeSelf || inventoryUI.activeSelf || sortingUI.activeSelf)
    //        yield break;

    //    yield return new WaitForSeconds(delay);

    //    ToggleSidebarUI();
    //}

    public void ShowCreationMenu()
    {
        GameObject[] _MainMenu = GameObject.FindGameObjectsWithTag("MainMenu");
        foreach (GameObject _go in _MainMenu)
        {
            _go.SetActive(false);
        }
    }

    public void GameUI()
    {
        miniSidebar.SetActive(true);
    }

}
