using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject sidebarUI;
    [SerializeField] private GameObject sortingUI;

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
        GameManager.instance.inMenu = !GameManager.instance.inMenu;
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        sidebarUI.SetActive(!inventoryUI.activeSelf);
    }
    
    /* CONTROLLING THE UI */

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
        sidebarUI.SetActive(true);
    }

}
