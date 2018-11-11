using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    [SerializeField] private Animator planetNameAnim;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject sidebarUI;
    [SerializeField] private GameObject sortingUI;

    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas menuCanvas;

    private CameraMovement camMovement;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        camMovement = Camera.main.GetComponent<CameraMovement>();
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
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        sidebarUI.SetActive(!inventoryUI.activeSelf);
    }
    
    /* CONTROLLING THE UI */

    public void ShowCreationMenu()
    {
        planetNameAnim.Play("PlanetNameDropDown");

        GameObject[] _MainMenu = GameObject.FindGameObjectsWithTag("MainMenu");
        foreach (GameObject _go in _MainMenu)
        {
            _go.SetActive(false);
        }
    }

    public void GameUI()
    {
        sidebarUI.SetActive(true);
        planetNameAnim.Play("PlanetNameUp");
    }

}
