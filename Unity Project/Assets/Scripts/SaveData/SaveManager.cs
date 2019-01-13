using System;
using UnityEngine;
using System.IO;

/// <summary>
/// With this version of Save/Load system, SimpleJSON is no longer used. The system now utilizes Unity's built-in JSON
///     serializer. With a second script 'JsonHelper', there is no need to use the additional library. At a later date,
///     the library should be removed to avoid having it being built into the game.
///     
/// ToDo:
///     - Remove SimpleJSON once certain its no longer being used/no longer needed to save/load.
/// </summary>

[Serializable]
public struct PlanetData
{
    public string planetName;
    public string startDate;
}

[Serializable]
public struct PlanetObjectData
{
    public string objectId;
    public string objectName;
    public float posX, posY;
    public int remainingTime;
}

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
    #endregion
    
    #region Script References
    Sorting sortingManager;
    GameManager gameManager;
    Inventory inventory;
    Journal journal;
    #endregion

    private string planetDataPath       = "/PlanetSettings.json";   // Temp path for the planet settings file.
    private string objectSavePath       = "/PlanetObjects.json";    // Temp path for the planet's objects save.
    private string objectsData          = "";                       // String used for the save data.
    private string loadString           = "";                       // String used for the load data.

    private void Start()
    {
        sortingManager  = Sorting.instance;
        gameManager     = GameManager.instance;
        inventory       = Inventory.instance;
        journal         = Journal.instance;
    }

    // ---- Simple loading and saving of the objects, as more data is saved the system will need to be exapanded.
    //          A possiblity of having a separate save/load scripts to keep this one clean, in the future.
    //          For now this is sufficient, as long as kept tidy and easy to read.

    public bool SaveGame()
    {
        // Save the planet data
        PlanetData planetInstance = new PlanetData();

        planetInstance.planetName   = gameManager.PlanetName;
        planetInstance.startDate    = gameManager.GetStartDate;

        string planetData = JsonUtility.ToJson(planetInstance, true);


        // Save planet objects
        PlanetObjectData[] planetObjectInstance = new PlanetObjectData[gameManager.planetObjects.Count];

        // ----> consider simplyfying this, and maybe use a foreach instead?
        for (int i = 0; i < gameManager.planetObjects.Count; i++)
        {
            GameObject tempGO = gameManager.planetObjects[i];

            planetObjectInstance[i] = new PlanetObjectData();

            planetObjectInstance[i].objectId        = tempGO.GetComponent<PlanetObject>().scrObject.objectID;
            planetObjectInstance[i].objectName      = tempGO.GetComponent<PlanetObject>().scrObject.name;
            planetObjectInstance[i].posX            = tempGO.transform.position.x;
            planetObjectInstance[i].posY            = tempGO.transform.position.y;
            planetObjectInstance[i].remainingTime   = (int)tempGO.GetComponent<PlanetObject>().RemainingTime;

            objectsData = JsonHelper.ToJson(planetObjectInstance, true);
        }

        if (objectsData == "" || objectsData == " ")
        {
            Debug.LogError("Save string empty! Couldn't save!");
            return false;
        }

        string path1 = Application.persistentDataPath + planetDataPath;
        string path2 = Application.persistentDataPath + objectSavePath;
        File.WriteAllText(path1, planetData);
        File.WriteAllText(path2, objectsData);

        return true;
    }

    public bool LoadGame()
    {
        // Check if the save file exists, if it does = load it into the game. Otherwise, return a warning.
        // Due to how the game is designed, there is no need to return error to the player as it should start a new game.
        if (File.Exists(Application.persistentDataPath + objectSavePath))
        {
            loadString = File.ReadAllText(Application.persistentDataPath + objectSavePath);
        }
        else
        {
            Debug.LogWarning("Savefile not found.");
            return false;
        }

        if (loadString == "")
        {
            Debug.LogError("Savefile empty/corrupted.");
            return false;
        }

        // At this point the savefile exists, and it has save data in it. Load the game..
        PlanetObjectData[] planetObjects = JsonHelper.FromJson<PlanetObjectData>(loadString);
        InventoryItem _scriptableObject = null;

        foreach (PlanetObjectData _obj in planetObjects)
        {
            // Find the scriptable object that has been used in the object before, by searching for the ID.
            // This approach can become problematic if the IDs changed, or are removed. In this case savegames won't
            //  be compatible with the previous versions of the game.
            foreach (InventoryItem _inv in sortingManager.fruitRewards)
            {
                if (_obj.objectId == _inv.objectID)
                    _scriptableObject = _inv;
            }
            foreach (InventoryItem _inv in sortingManager.vegetableRewards)
            {
                if (_obj.objectId == _inv.objectID)
                    _scriptableObject = _inv;
            }
            if (_scriptableObject == null)
            {
                Debug.LogError("Scriptable object could not be found, while loading the game object. Have the IDs changed?");
                return false;
            }

            // At this point the scriptable object has been found, and the object can be loaded back into the game.
            Vector3 _tempObjPos             = new Vector3(_obj.posX, _obj.posY, 0.0f);
            GameObject _tempGameObject      = gameManager.SpawnPlanetItem(_scriptableObject, _tempObjPos);
            PlanetObject _tempPlanetObject  = _tempGameObject.GetComponent<PlanetObject>();

            _tempPlanetObject.RemainingTime = _obj.remainingTime;
            _tempGameObject.name            = _obj.objectName;

            // Object has been created successfully, and now exists back in the game.
            // Continue with the next object from the save file.
        }

        // Game loaded successfully.
        return true;
    }

    public void ClearSaveData()
    {
        throw new NotImplementedException();
    }
}
