using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private InputField nameField;

    private string planetName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSubmit()
    {
        planetName = nameField.text;

        Debug.Log("Planet Name : " + planetName);
    }
}
