using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Script References
    SaveManager saveManager;
    UIManager uiManager;
    #endregion

    [Header("DEBUG")]
    [Tooltip("Current version of the game.")]
    [SerializeField] private string buildVersion;
    [SerializeField] private string planetName;
    public List<GameObject> planetObjects;

    public GameObject treePrefab;
    [HideInInspector] public InventoryItem itemHolding;
    [HideInInspector] public GameObject planetRef;

    public bool stopCameraMovement = false;

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

    DateTime startDate;
    DateTime sessionTime;

    [HideInInspector] public enum GameState { Menu, Gameplay, Sorting, PlaceItem };
    [HideInInspector] public GameState currentState;

    private void Start ()
    {
        uiManager = UIManager.instance;
        saveManager = SaveManager.instance;

        planetName = PlayerPrefs.GetString("PlanetName");

        //bool gameLoaded = saveManager.LoadGame();
        //if (!gameLoaded)
        //    Debug.LogError("Couldn't load save data");

        uiManager.UpdateUI();

        if (startDate == null)
            startDate = DateTime.Now;

        planetRef = GameObject.FindGameObjectWithTag("Player");
	}

    public GameObject SpawnPlanetItem(InventoryItem _scriptableObject, Vector3 _objectPosition)
    {
        // Create a temporaray object, populate it with data, and return the gameobject.
        GameObject _tempObject = Instantiate(treePrefab, _objectPosition, Quaternion.identity);

        _tempObject.GetComponentInChildren<PlanetObject>().scrObject    = _scriptableObject;
        _tempObject.name                                                = _scriptableObject.objectID;
        _tempObject.transform.parent                                    = planetRef.transform;

        planetObjects.Add(_tempObject);

        return _tempObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            bool gameLoaded = saveManager.LoadGame();
            if (!gameLoaded)
                Debug.Log("Load failed.");
            else
                Debug.Log("Game loaded.");
        }

        if (Input.GetKeyDown(KeyCode.S))
            saveManager.SaveGame();

        // THIS SHOULD BE IN ITS OWN THING
        if (currentState == GameState.PlaceItem)
        {
            stopCameraMovement = true;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 placePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                placePos.z = 0;

                GameObject tempObject = Instantiate(treePrefab, placePos, Quaternion.identity);
                tempObject.transform.parent = planetRef.transform;

                tempObject.GetComponentInChildren<PlanetObject>().scrObject = itemHolding;
                tempObject.name = itemHolding.name;
                planetObjects.Add(tempObject);

                // Cleanup
                currentState = GameState.Gameplay;

                Inventory.instance.Remove(itemHolding);

                tempObject = null;
                itemHolding = null;
                treePrefab = null;
                stopCameraMovement = false;
            }
        }
    }

    #region Getters & Setters
    public string PlanetName { get { return planetName; } set { planetName = value; } }
    public string GetBuildVersion { get { return buildVersion; } }
    public string GetStartDate { get { return startDate.ToShortDateString(); } }
    public string GetSessionDate { get { return sessionTime.ToShortDateString(); } }
    #endregion

    private void OnApplicationQuit()
    {
        sessionTime = DateTime.Now;

        //SaveManager.instance.SaveGame();
        //saveManager.SaveGame();
        
        // Delete player prefs when done.
        // Should probably make a note, to remove this when shipping.
        //if (Debug.isDebugBuild)
        //{
        //    PlayerPrefs.DeleteAll();
        //    //SaveManager.instance.ClearSaveFile();
        //}
    }
}
