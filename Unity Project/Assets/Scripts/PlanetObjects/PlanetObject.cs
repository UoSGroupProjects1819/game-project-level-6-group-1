using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetObject : ScriptableObject {

    [Header("Object Properties")]
    public Sprite[] objectSprites = new Sprite[2];
    public string objectName = "New Object Name";
}
