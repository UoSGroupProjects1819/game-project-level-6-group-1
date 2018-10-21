using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour {

    private float baseAngle = 0.0f;

    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos = Input.mousePosition - mousePos;

        baseAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos = Input.mousePosition - mousePos;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - baseAngle;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
