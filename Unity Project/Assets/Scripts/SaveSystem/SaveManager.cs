using System;
using System.Collections.Generic;
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

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;
        private static string SaveFolder = "";
        private static string SaveName = "SaveGame.json";
        private static string SavePath = Path.Combine(SaveFolder, SaveName);
        
        #region Singleton
        private void Awake()
        {
            SaveFolder = Application.persistentDataPath;
            
            if (instance == null)
                instance = this;
            else if (instance != null)
                Destroy(gameObject);
        }
        #endregion
        
        public SaveController saveController;
        
        /// <summary>
        /// Save current game progress.
        /// </summary>
        /// <param name="planetObjects">List of planet objects.</param>
        /// <param name="inventoryItems">List of inventory items.</param>
        public void SaveGame(List<GameObject> planetObjects, List<InventoryItem> inventoryItems)
        {
            if (!saveController.SaveGame(planetObjects, inventoryItems, SavePath))
            {
                Debug.LogError("Could not save game. Check console for error!");
            }
        }

//        public bool LoadGame()
//        {
//            // Check if the save file exists, if it does = load it into the game. Otherwise, return a warning.
//            // Due to how the game is designed, there is no need to return error to the player as it should start a new game.
//            if (File.Exists(Application.persistentDataPath + objectSavePath))
//            {
//                loadString = File.ReadAllText(Application.persistentDataPath + objectSavePath);
//            }
//            else
//            {
//                Debug.LogWarning("Savefile not found.");
//                return false;
//            }
//
//            if (loadString == "")
//            {
//                Debug.LogError("Savefile empty/corrupted.");
//                return false;
//            }
//
//            // At this point the savefile exists, and it has save data in it. Load the game..
//            PlanetObjectData[] planetObjects = JsonHelper.FromJson<PlanetObjectData>(loadString);
//            InventoryItem _scriptableObject = null;
//
//            foreach (PlanetObjectData _obj in planetObjects)
//            {
//                // Find the scriptable object that has been used in the object before, by searching for the ID.
//                // This approach can become problematic if the IDs changed, or are removed. In this case savegames won't
//                //  be compatible with the previous versions of the game.
//                foreach (InventoryItem _inv in sortingManager.fruitRewards)
//                {
//                    if (_obj.objectId == _inv.objectID)
//                        _scriptableObject = _inv;
//                }
//
//                foreach (InventoryItem _inv in sortingManager.vegetableRewards)
//                {
//                    if (_obj.objectId == _inv.objectID)
//                        _scriptableObject = _inv;
//                }
//
//                if (_scriptableObject == null)
//                {
//                    Debug.LogError(
//                        "Scriptable object could not be found, while loading the game object. Have the IDs changed?");
//                    return false;
//                }
//
//                // At this point the scriptable object has been found, and the object can be loaded back into the game.
//                Vector3 _tempObjPos = new Vector3(_obj.posX, _obj.posY, 0.0f);
//                GameObject _tempGameObject = gameManager.SpawnPlanetItem(_scriptableObject, _tempObjPos);
//                PlanetObject _tempPlanetObject = _tempGameObject.GetComponent<PlanetObject>();
//
//                _tempPlanetObject.RemainingTime = _obj.remainingTime;
//                _tempGameObject.name = _obj.objectName;
//
//                // Object has been created successfully, and now exists back in the game.
//                // Continue with the next object from the save file.
//            }
//
//            // Game loaded successfully.
//            return true;
//        }

        public void ClearSaveData()
        {
            throw new NotImplementedException();
        }
    }
}