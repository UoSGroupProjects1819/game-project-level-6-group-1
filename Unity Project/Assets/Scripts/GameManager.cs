using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private InputField nameField;

    private string planetName;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("playerPlanetName"))
        {
            planetName = PlayerPrefs.GetString("playerPlanetName");
            Debug.Log("Planet Loaded!");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.SetString("playerPlanetName", planetName);
            PlayerPrefs.Save();

            Debug.Log("Planet saved!");
        }
	}

    public void OnSubmit()
    {
        planetName = nameField.text;

        Debug.Log("Planet Name : " + planetName);
    }
}
