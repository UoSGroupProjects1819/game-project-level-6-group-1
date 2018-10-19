using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float cameraMinSize = 4.0f;
    [SerializeField]
    private float cameraMaxSize = 14.0f;
    [SerializeField]
    private Text timeText;

    private Vector3 touchStart;

    private void Update()
    {
        timeText.text = System.DateTime.UtcNow.ToString("HH:mm");

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchZero.position - touchZero.deltaPosition;

            float prevMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMag = (touchZeroPrevPos - touchOne.position).magnitude;

            float difference = currentMag - prevMag;

            Zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, cameraMinSize, cameraMaxSize);
    }
}
