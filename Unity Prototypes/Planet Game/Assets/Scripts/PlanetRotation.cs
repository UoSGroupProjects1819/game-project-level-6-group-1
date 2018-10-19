using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour {

    private bool isDragging = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.rotation.eulerAngles = new Vector3(0f, 0f, 0f);
	}

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseExit()
    {
        isDragging = false;
    }
}
