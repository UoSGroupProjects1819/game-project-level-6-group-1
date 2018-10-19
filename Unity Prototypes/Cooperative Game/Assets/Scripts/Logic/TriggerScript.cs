using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    [SerializeField]
    private bool isTimerBased;
    [SerializeField]
    private float timerReset    = 3.0f;

    [HideInInspector]
    public bool isPressed       = false;

    private float timer         = 0.0f;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        isPressed = true;
        timer = timerReset;
    }

    private void Update()
    {
        if (isTimerBased)
        {
            if (timer < 0)
            {
                isPressed = false;
                timer = 0;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (!isTimerBased)
        {
            isPressed = false;
        }
    }
}
