using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

[Serializable]
public struct PlanetObjectData
{
    public string objectId;
    public string objectName;
    public float posX, posY;
    public float remainTime;
}

public class SaveManager : MonoBehaviour
{
    private Journal journal;
    private Inventory inventory;
    private GameManager gameManager;

    private string planetDataPath = "/PlanetSettings.json";
    private string objectSavePath = "/PlanetObjects.json";
    private string saveString = "";
    private string loadString = "";
    private JSONObject objectsToSave = new JSONObject();

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

    private void Start()
    {
        gameManager = GameManager.instance;
        inventory   = Inventory.instance;
        journal     = Journal.instance;
    }

    public void SaveGame()
    {
        PlanetObjectData[] planetObjectInstance = new PlanetObjectData[gameManager.planetObjects.Count];

        for (int i = 0; i < gameManager.planetObjects.Count; i++)
        {
            GameObject tempGO = gameManager.planetObjects[i];

            planetObjectInstance[i] = new PlanetObjectData();

            planetObjectInstance[i].objectId = tempGO.GetComponent<PlanetObject>().scrObject.objectID;
            planetObjectInstance[i].posX = tempGO.transform.position.x;
            planetObjectInstance[i].posY = tempGO.transform.position.y;
            planetObjectInstance[i].remainTime = tempGO.GetComponent<PlanetObject>().RemainingTime;

            saveString = JsonHelper.ToJson(planetObjectInstance, true);
        }

        Debug.Log(saveString);

        string path = Application.persistentDataPath + objectSavePath;
        File.WriteAllText(path, saveString);
    }
    
    // IMPROVE AT SOME POINT?
    public void LoadGame()
    {
        string tempString = File.ReadAllText(Application.persistentDataPath + objectSavePath);

        if (tempString == "")
            return;

        PlanetObjectData[] planetObjects = JsonHelper.FromJson<PlanetObjectData>(tempString);

        foreach (PlanetObjectData _obj in planetObjects)
        {
            Vector3 _objPos = new Vector3(_obj.posX, _obj.posY, 0);

            GameObject _tempObject = Instantiate(gameManager.treePrefab, _objPos, Quaternion.identity);
            PlanetObject _tempPlanetObj = _tempObject.GetComponent<PlanetObject>();

            foreach (InventoryItem _inv in Sorting.instance.vegetableRewards)
            {
                if (_obj.objectId == _inv.objectID)
                {
                    _tempPlanetObj.scrObject = _inv;
                }
            }

            foreach (InventoryItem _inv in Sorting.instance.fruitRewards)
            {
                if (_obj.objectId == _inv.objectID)
                {
                    _tempPlanetObj.scrObject = _inv;
                }
            }

            _tempPlanetObj.RemainingTime = _obj.remainTime;
            _tempObject.name = _obj.objectId;

            _tempObject.transform.parent = gameManager.planetRef.transform;
            gameManager.planetObjects.Add(_tempObject);
        }
    }








    public void SavePlanet(string planetName, string gameVersion,
                            string startDate, string sessionTime,
                            List<GameObject> planetObjects)
    {

    }

    public void SavePlanetName(string planetName)
    {
        JSONObject planetJSON = new JSONObject();
        planetJSON.Add("PlanetName", planetName);

        string path = Application.persistentDataPath + "/SaveGame.json";
        File.WriteAllText(path, planetJSON.ToString());

        Debug.Log(planetJSON.ToString());
    }

    public void SaveGameOld()
    {
        JSONObject planetJSON = new JSONObject();
        planetJSON.Add("PlanetName", gameManager.PlanetName);
        planetJSON.Add("GameVersion", gameManager.GetBuildVersion);
        //planetJSON.Add("StartDate", UIManager.instance.startDate.ToShortDateString());

        string path = Application.persistentDataPath + "/SaveGame.json";
        File.WriteAllText(path, planetJSON.ToString());
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/SaveGame.json";
        string jsonString = File.ReadAllText(path);

        JSONObject planetJSON = (JSONObject)JSON.Parse(jsonString);

        // Set values:
        GameManager.instance.PlanetName = planetJSON["PlanetName"];
    }

    public void ClearSaveFile()
    {
        string path = Application.persistentDataPath + "/SaveGame.json";

        JSONObject emptySave = new JSONObject();
        File.WriteAllText(path, emptySave.ToString());
    }
}
