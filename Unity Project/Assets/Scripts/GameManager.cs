using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    [Tooltip("Set this to true, in order to skip the menu and go straight into the gameplay.")]
    public bool skipMenu= false;
    public bool enableSorting;

    [HideInInspector] public bool inMenu = true;
    [HideInInspector] public bool onPlanet = false;

    [HideInInspector] public double secondsPassed;
    [HideInInspector] public string planetName;
    [HideInInspector] public GameObject itemToPlace;
    [HideInInspector] public Item itemHolding;

    private GameObject planet;
    private TimeController timeController;
    private PlanetCreation planetCreation;

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

    private void Start ()
    {
        // DELETE ALL PLAYERPREF KEYS. || DEBUG ONLY, DELETE LATER.
        PlayerPrefs.DeleteAll();

        planet = GameObject.FindGameObjectWithTag("Player");
        planetCreation = gameObject.GetComponent<PlanetCreation>();
        timeController = gameObject.GetComponent<TimeController>();

        if (skipMenu)
            StartGame();
	}

    private void Update()
    {
        // THIS SHOULD BE IN ITS OWN SCRIPT
        if (currentState == GameState.PlaceItem)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 placePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                placePos.z = 0;

                GameObject tempObject = Instantiate(itemToPlace, placePos, Quaternion.identity);
                tempObject.transform.parent = planet.transform;
                tempObject.name = itemHolding.name;

                // Cleanup
                currentState = GameState.Gameplay;

                Inventory.instance.Remove(itemHolding);

                tempObject = null;
                itemHolding = null;
                itemToPlace = null;
            }
        }
    }

    private void OnApplicationQuit()
    {
        // When quitting the game, save the time to PlayerPrefs.
        timeController.SaveTime();
    }

    #region FUNCTIONS USED TO CONTROL THE GAME

    public void PlaceItem()
    {

    }

    public void LoadGame()
    {
        Debug.Log("Load game");

        // Before starting the planet creation menu, see if a existing game save exists.
        if (PlayerPrefs.HasKey("PlanetName"))
        {
            // There is a planet name saved in the PlayerPrefs, we assume that a save exists.
            planetName = PlayerPrefs.GetString("PlanetName");
            Debug.Log("Game found, loading game!");
            StartGame();
        }
        else
        {
            // No planet has been created, show the creation menu.
            Debug.Log("No previous game found, create new planet.");
            PlanetCreationMenu();
        }

    }

    public void StartGame()
    {
        Debug.Log("Start game!");
        UIManager.instance.GameUI();
        inMenu = false;
    }

    private void PlanetCreationMenu()
    {
        planetCreation.ShowCreationMenu();
        
    }

    /* TEMP FUNCTIONS */

    private void SaveGame()
    {
        PlayerPrefs.SetString("PlanetName", planetName);
        timeController.SaveTime();
    }

    #endregion
}
