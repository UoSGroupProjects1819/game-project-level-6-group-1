using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [Tooltip("This is the camera's minimum size, players won't be able to zoom in more than this value.")]
    [SerializeField] private float cameraMinSize    = 4.0f;

    [Tooltip("This is the camera's maximum size, players won't be able to zoom out more than this value.")]
    [SerializeField] private float cameraMaxSize    = 14.0f;

    [Tooltip("This is the value used to control the camera movement speed in the menu, increasing this value will make the camera move from one menu to another faster while decreasing it will make it move slower.")]
    [SerializeField] private float cameraMenuMoveSpeed  = 70.0f;
    [SerializeField] private GameObject planetTemp;

    [HideInInspector] public bool menuMode  = true;
    [HideInInspector] public bool notPlanet = true;
    [HideInInspector] public Vector3 moveTowards;

    private Vector3 touchStart;

    private void Awake()
    {
        // Set the moveTowards point to Camera's position at the start, so it doesn't run away.
        moveTowards = transform.position;
    }

    private void Update()
    {
        if (menuMode)
        {
            // The camera is in the menu mode, which means players are not able to control the camera directly.
            transform.position = Vector3.MoveTowards(
                transform.position, moveTowards, cameraMenuMoveSpeed * Time.deltaTime);
        }
        else if (!menuMode)
        {
            // Camera is NOT in menuMode, but in 'playMode'; players can control the camera.
            // Grab the postition of when the users first pressed down (or mouse clicked).
            if (Input.GetMouseButtonDown(1))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            // Players have made two touches, they are trying to zoom in/zoom out.
            // Calculate the difference and apply zoom.This is used on mobile devices.
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currMagnitude = (touchZero.position - touchOne.position).magnitude;

                float touchDifference = currMagnitude - prevMagnitude;

                HandleZoom(touchDifference * 0.01f);
            }

            // Players are holding the touch (or holding the mouse button), they are trying to move camera.
            // Calculate the difference between the first touch, and current position and move camera accordingly.
            if (Input.GetMouseButton(1))
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position += direction;
            }

            // Handle the camera zoom, if players are using a mouse.
            HandleZoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private void HandleZoom (float increment)
    {
        // Handle the camera zoom, clamp it between min/max values.
        Camera.main.orthographicSize = Mathf.Clamp(
            Camera.main.orthographicSize - increment, cameraMinSize, cameraMaxSize);
    }
}
