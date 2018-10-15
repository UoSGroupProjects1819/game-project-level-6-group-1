using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float smoothSpeed = 10f;
    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private float maxCamSize = 10f;
    [SerializeField]
    private float minCamSize = 5.6f;

    private GameObject PlayerOne;
    private GameObject PlayerTwo;
    private Camera MapCamera;

    private Vector2 desiredPosition;

    private float distanceBetweenPlayers;
    private float cameraZoom;


	void Start () {
        PlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        PlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");

        if (!PlayerOne || !PlayerTwo)
            Debug.LogError("Cannot find the players! Make sure the players are in the scene!");

        MapCamera = Camera.main;
	}
	
	void Update () {
        // Calculate the distance between players, and zoom in/out accordingly.
        distanceBetweenPlayers = Vector2.Distance(PlayerOne.transform.position, PlayerTwo.transform.position);
        cameraZoom = Mathf.Lerp(minCamSize, maxCamSize, distanceBetweenPlayers / 20f);

        MapCamera.orthographicSize = cameraZoom;

        // Calculate the midpoint. and smooth lerp the camera.
        desiredPosition.x = PlayerOne.transform.position.x + (PlayerTwo.transform.position.x - PlayerOne.transform.position.x) / 2;
        desiredPosition.y = PlayerOne.transform.position.y + (PlayerTwo.transform.position.y - PlayerOne.transform.position.y) / 2;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        smoothedPosition.z = -10f;
        transform.position = smoothedPosition;
	}
}
