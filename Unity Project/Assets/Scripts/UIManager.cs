using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour {

    public static UIManager UIInstance = null;

    [SerializeField] private Animator planetNameAnim;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject sidebarUI;

    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas menuCanvas;

    private CameraMovement camMovement;

    private void Awake()
    {
        if (UIInstance == null)
            UIInstance = this;
        else if (UIInstance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        camMovement = Camera.main.GetComponent<CameraMovement>();
    }

    /* FUNCTIONS FOR THE BUTTONS */

    public void _StartGame()
    {
        GameManager.GMInstance.LoadGame();
    }

    public void _ShowInventory()
    {
        // Show the seeds bag.
        Debug.Log("Show inventory.");
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
