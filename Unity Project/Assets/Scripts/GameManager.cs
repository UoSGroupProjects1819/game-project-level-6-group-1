using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class GameManager : MonoBehaviour
{
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
    
    #region Script References
    SaveManager saveManager;
    UIManager uiManager;
    private Inventory inventory;
    #endregion

    [Header("DEBUG")]
    [Tooltip("Current version of the game.")]
    [SerializeField] private string buildVersion;
    [SerializeField] private string planetName;
    public List<GameObject> planetObjects;
    public GameObject treePrefab;
    public bool stopCameraMovement = false;


    // ----

    
    DateTime startDate;
    DateTime sessionTime;

    [HideInInspector] public InventoryItem itemHolding;
    [HideInInspector] public GameObject planetRef;
    [HideInInspector] public enum GameState { Menu, Gameplay, Sorting, PlaceItem };
    [HideInInspector] public GameState currentState;

    private void Start ()
    {
        saveManager = SaveManager.instance;
        uiManager   = UIManager.instance;
        planetRef   = GameObject.FindGameObjectWithTag("Player");
        inventory   = Inventory.instance;
	}

    private void Update()
    {
        #region Temporary Saving/Spawning
//        if (Input.GetKeyDown(KeyCode.L))
//        {
//            bool gameLoaded = saveManager.LoadGame();
//            if (!gameLoaded)
//                Debug.Log("Load failed.");
//            else
//                Debug.Log("Game loaded.");
//        }

        if (Input.GetKeyDown(KeyCode.S))
            saveManager.SaveGame(planetObjects, inventory.inventoryItems);
        #endregion

        if (currentState == GameState.PlaceItem)
        {
            stopCameraMovement = true;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 placePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                placePos.z = 0;

                SpawnPlanetItem(itemHolding, placePos);

                // Cleanup after spawning the item
                currentState = GameState.Gameplay;

                Inventory.instance.Remove(itemHolding);

                itemHolding = null;
                treePrefab = null;
                stopCameraMovement = false;
            }
        }
    }

    /// <summary>
    /// Returns a PlanetObject that is instantiated into the planet.
    /// </summary>
    /// <param name="_scriptableObject">Scriptable object that will be used, to get the data, by the planet object.</param>
    /// <param name="_objectPosition">Vector3 position at which the object should be instantiated.</param>
    /// <returns></returns>
    public GameObject SpawnPlanetItem(InventoryItem _scriptableObject, Vector3 _objectPosition)
    {
        GameObject _tempObject = Instantiate(treePrefab, _objectPosition, Quaternion.identity);

        _tempObject.GetComponentInChildren<PlanetObject>().scrObject = _scriptableObject;
        _tempObject.name = _scriptableObject.objectID;
        _tempObject.transform.parent = planetRef.transform;

        planetObjects.Add(_tempObject);

        return _tempObject;
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
    }
}
