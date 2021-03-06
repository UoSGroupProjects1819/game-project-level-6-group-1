﻿using UnityEngine;
using UnityEngine.EventSystems;

public class RotatePlanet : MonoBehaviour {

    private float baseAngle = 0.0f;

    private void OnMouseDown()
    {
        // If we are hovering over the UI, don't move the camera. Quick, easy and works.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!GameManager.instance.stopCameraMovement)
        {
            //GameManager.instance.onPlanet = true;

            Vector3 mousePos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos = Input.mousePosition - mousePos;

            baseAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        }
    }

    private void OnMouseDrag()
    {
        // If we are hovering over the UI, don't move the camera. Quick, easy and works.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!GameManager.instance.stopCameraMovement)
        {
            //GameManager.instance.onPlanet = true;

            Vector3 mousePos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos = Input.mousePosition - mousePos;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - baseAngle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnMouseUp()
    {
        //GameManager.instance.onPlanet = false;
    }
}
