using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager GMInstance = null;

    public bool skipMenu= false;
    [HideInInspector] public bool inMenu = true;
    [HideInInspector] public bool onPlanet = false;

    [HideInInspector] public double secondsPassed;
    [HideInInspector] public string planetName;

    private TimeController timeController;
    private PlanetCreation planetCreation;

    // Enum that is used to control the current state of the game.
    private enum GameState { MainMenu, PlanetCreation, Gameloop, Sorting };
    GameState currentState;

    private void Awake()
    {
        if (GMInstance == null)
            GMInstance = this;
        else if (GMInstance != null)
            Destroy(gameObject);
    }

    private void Start ()
    {
        // DELETE ALL PLAYERPREF KEYS. || ONLY FOR TESTING.
        PlayerPrefs.DeleteAll();

        planetCreation = gameObject.GetComponent<PlanetCreation>();
        timeController = gameObject.GetComponent<TimeController>();

        if (skipMenu)
            StartGame();
	}

    private void OnApplicationQuit()
    {
        // When quitting the game, save the time to PlayerPrefs.
        timeController.SaveTime();
    }

    /* FUNCTIONS USED TO CONTROL THE STATE OF THE GAME */

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
        UIManager.UIInstance.GameUI();
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
}
