using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class SaveManager : MonoBehaviour {

    private Journal journal;
    private Inventory inventory;
    private GameManager GM;

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
        GM          = GameManager.instance;
        inventory   = Inventory.instance;
        journal     = Journal.instance;
    }

    public void SavePlanetName(string planetName)
    {
        JSONObject planetJSON = new JSONObject();
        planetJSON.Add("PlanetName", planetName);

        string path = Application.persistentDataPath + "/SaveGame.json";
        File.WriteAllText(path, planetJSON.ToString());

        Debug.Log(planetJSON.ToString());
    }

    public void SaveGame()
    {
        JSONObject planetJSON = new JSONObject();
        planetJSON.Add("PlanetName", GM.PlanetName);
        planetJSON.Add("GameVersion", GM.GetBuildVersion);
        planetJSON.Add("StartDate", UIManager.instance.startDate.ToShortDateString());

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
