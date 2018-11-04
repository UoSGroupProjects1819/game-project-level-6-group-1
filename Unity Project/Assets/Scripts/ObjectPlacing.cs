using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacing : MonoBehaviour {

    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform planetTransform;
	
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0.0f;

            Vector3 difference = planetTransform.position - spawnPosition;
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            GameObject objectInstance = Instantiate(
                objectToSpawn, spawnPosition, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle + 90.0f)));
            objectInstance.transform.parent = planetTransform;
        }
	}
}
