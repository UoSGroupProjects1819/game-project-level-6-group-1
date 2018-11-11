using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

    [SerializeField] private TMPro.TMP_Text numberText;
    [SerializeField] private Image fillImage;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float countTime;

    private float startEnergy;
    private float currentEnergy;
    private float desiredEnergy;

    private void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Set the number to the 'value', this will not have the 'count up' effect.
    /// </summary>
    public void SetNumber(float value)
    {
        startEnergy = currentEnergy;
        desiredEnergy = value;
    }

    /// <summary>
    /// Add the 'value' to our currentNumber, this will give us the 'count up' effect.
    /// </summary>
    public void AddToNumber(float value)
    {
        startEnergy = currentEnergy;
        desiredEnergy += value;
    }

    private void Update()
    {
        if (currentEnergy != desiredEnergy)
        {
            if (startEnergy < desiredEnergy)
            {
                currentEnergy += (countTime * Time.deltaTime) * (desiredEnergy - startEnergy);
                if (currentEnergy >= desiredEnergy)
                    currentEnergy = desiredEnergy;
            }
            else
            {
                currentEnergy -= (countTime * Time.deltaTime) * (startEnergy - desiredEnergy);
                if (currentEnergy <= desiredEnergy)
                    currentEnergy = desiredEnergy;
            }

            numberText.text = currentEnergy.ToString("0");
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        float uiFill = currentEnergy / maxEnergy;
        fillImage.fillAmount = uiFill;
    }
}
