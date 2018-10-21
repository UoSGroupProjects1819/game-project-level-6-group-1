using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private GameObject planetTemp;
    [SerializeField] private float cameraMinSize     = 4.0f;
    [SerializeField] private float cameraMaxSize     = 14.0f;

    private bool notPlanet = true;

    private Vector3 touchStart;
	
	void Update () {
        if (Input.GetMouseButtonDown(1) && notPlanet)
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne  = Input.GetTouch(1);

            Vector2 touchZeroPrevPos    = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos     = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currMagnitude = (touchZero.position - touchOne.position).magnitude;

            float touchDifference = currMagnitude - prevMagnitude;

            HandleZoom(touchDifference * 0.01f);
        }

        if (Input.GetMouseButton(1) && notPlanet)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        HandleZoom(Input.GetAxis("Mouse ScrollWheel"));
	}

    private void HandleZoom (float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, cameraMinSize, cameraMaxSize);
    }
}
