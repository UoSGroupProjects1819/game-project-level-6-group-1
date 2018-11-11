using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Text numberText;
    [SerializeField] private Image fillImage;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float countTime;

    private float startEnergy;
    private float currentEnergy;
    private float desiredEnergy;

    #region Singleton
    public static Energy instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        UpdateUI();
        SetNumber(maxEnergy);
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
    public void AddEnergy(float value)
    {
        startEnergy = currentEnergy;
        desiredEnergy += value;
    }

    public void RemoveEnergy(float value)
    {
        startEnergy = currentEnergy;
        desiredEnergy -= value;
    }

    private void Update()
    {
        if (currentEnergy == maxEnergy)
            return;

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

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        float uiFill = currentEnergy / maxEnergy;
        fillImage.fillAmount = uiFill;

        //numberText.text = currentEnergy.ToString("0");
    }
}
