using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour {

    [SerializeField]
    private GameObject objectToSpawn;

    public Transform WorldTransform;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0.0f;

            Vector3 diff = WorldTransform.position - spawnPosition;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            GameObject objectInstance = Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
        }
    }
}
