using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HoldButton : Button
{
    public UnityEvent OnHoldButton;
    private float holdDownTimer;

    private void Update()
    {
        if (IsPressed())
        {
            holdDownTimer += Time.deltaTime;

            if (holdDownTimer >= 2)
            {
                if (OnHoldButton != null)
                    OnHoldButton.Invoke();

                ResetButton();
            }
        }
    }

    private void ResetButton()
    {
        holdDownTimer = 0;
    }
}
