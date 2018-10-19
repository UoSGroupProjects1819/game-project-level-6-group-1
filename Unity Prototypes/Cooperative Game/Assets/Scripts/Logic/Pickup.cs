using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private Vector3 startPos;

	void Start () {
        startPos = transform.position;
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "ForceField")
        {
            transform.position = startPos;
        }
    }
}
