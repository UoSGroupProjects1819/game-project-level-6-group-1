using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("DEBUG")]
    [Tooltip("Current version of the game.")]
    [SerializeField] private string buildVersion;
    [SerializeField] private string planetName;

    [HideInInspector] public InventoryItem itemHolding;
    [HideInInspector] public GameObject treePrefab;
    [HideInInspector] public GameObject planetRef;

    [HideInInspector] public bool allowCameraMovement = false;

    #region Singleton
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
    #endregion

    [HideInInspector] public enum GameState { Menu, Gameplay, Sorting, PlaceItem };
    [HideInInspector] public GameState currentState;

    UIManager UI;

    private void Start ()
    {
        UI = UIManager.instance;

        SaveManager.instance.LoadData();
        UI.UpdateUI();

        planetRef = GameObject.FindGameObjectWithTag("Player");
	}

    private void Update()
    {
        // THIS SHOULD BE IN ITS OWN THING
        if (currentState == GameState.PlaceItem)
        {
            allowCameraMovement = true;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 placePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                placePos.z = 0;

                GameObject tempObject = Instantiate(treePrefab, placePos, Quaternion.identity);
                tempObject.transform.parent = planetRef.transform;

                tempObject.GetComponentInChildren<PlanetObject>().scrObject = itemHolding;
                tempObject.name = itemHolding.name;

                // Cleanup
                currentState = GameState.Gameplay;

                Inventory.instance.Remove(itemHolding);

                tempObject = null;
                itemHolding = null;
                treePrefab = null;
                allowCameraMovement = false;
            }
        }
    }

    #region Getters & Setters
    public string PlanetName { get { return planetName; } set { planetName = value; } }
    public string GetBuildVersion { get { return buildVersion; } }
    #endregion

    private void OnApplicationQuit()
    {
        SaveManager.instance.SaveGame();
        
        // Delete player prefs when done.
        // Should probably make a note, to remove this when shipping.
        if (Debug.isDebugBuild)
        {
            PlayerPrefs.DeleteAll();
            //SaveManager.instance.ClearSaveFile();
        }
    }
}
