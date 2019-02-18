using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace SaveSystem
{
    [Serializable]
    public struct PlanetObjectData
    {
        public string objectId;
        public string objectName;
        public string posX, posY;
        public int remainingTime;
    }
    
    [Serializable]
    public struct InventoryItemData
    {
        public string objectId;
        public string objectName;
    }

    public class SaveController : MonoBehaviour
    {
        private string planetObjects;
        private string inventoryObjects;
        
        private bool SavePlanetItems(List<GameObject> planetItems)
        {
            var i = 0;

            // Save objects placed on the planet.
            PlanetObjectData[] objectData = new PlanetObjectData[planetItems.Count];

            foreach (GameObject planetObject in planetItems)
            {
                PlanetObject tempObject = planetObject.GetComponent<PlanetObject>();
                Vector3 tempPos = tempObject.transform.position;
                
                objectData[i].objectId = tempObject.scrObject.objectID;
                objectData[i].objectName = tempObject.scrObject.name;
                objectData[i].posX = tempPos.x.ToString("0.00");
                objectData[i].posY = tempPos.y.ToString("0.00");
                objectData[i].remainingTime = (int) tempObject.RemainingTime;

                planetObjects = JsonHelper.ToJsonPlanetItems(objectData, true);
                i++;
            }
            
            return true;
        }

        private bool SaveInventoryItems(List<InventoryItem> inventoryItems)
        {
            var i = 0;
            
            // Save inventory items.
            InventoryItemData[] itemData = new InventoryItemData[inventoryItems.Count];

            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                itemData[i].objectId = inventoryItem.objectID;
                itemData[i].objectName = inventoryItem.name;

                inventoryObjects = JsonHelper.ToJsonInventory(itemData, true);
                i++;
            }
            
            return true;
        }

        public bool SaveGame(List<GameObject> planetItems, List<InventoryItem> items, string savePath)
        {
            if (!SavePlanetItems(planetItems) || planetObjects == "")
            {
                Debug.LogError("Could not save planet objects! Check planet items!");
                return false;
            }

            if (!SaveInventoryItems(items) || inventoryObjects == "")
            {
                Debug.LogError("Could not save inventory items! Check inventory items!");
                return false;
            }

            var saveContents = planetObjects + inventoryObjects;

            Debug.Log(saveContents);
            
            //File.WriteAllText(savePath, saveContents);
            return true;
        }
    }
}
