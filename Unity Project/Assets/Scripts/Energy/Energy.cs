using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ToDo:
 *  - Separate the UI into its own UI script.
 *  - Keep this to functionality only.
 */

public class Energy : MonoBehaviour
{
    [Header("Refill Settings")]
    [Tooltip("Amount of time (in seconds), before the player receives free energy.")]
    [SerializeField] private float refillTime;
    [Tooltip("Amount of energy the players will receive when the timer runs out.")]
    [SerializeField] private float refillAmnt;

    [Header("System settings")]
    [Tooltip("Image used to display the amount of energy. REMEMBER to set the image type to 'fill'.")]
    [SerializeField] private Image fillImage;
    [Tooltip("When ticked, the energy will not be drained. Use for testing.")]
    [SerializeField] private bool infiniteEnergy;
    [Tooltip("Maximum amount of energy players can have, this can be adjusted on the go.")]
    [SerializeField] private float maxEnergy;

    [HideInInspector] public bool hasEnergy;

    private float countTime = 5.0f;

    [SerializeField] private float timer = 0;
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
        SetEnergy(maxEnergy);
        //timer = refillTime;
    }

    /// <summary>
    /// Set the number to the 'value', this will not have the 'count up' effect.
    /// </summary>
    public void SetEnergy(float value)
    {
        startEnergy = currentEnergy;
        desiredEnergy = value;

        if (desiredEnergy > maxEnergy)
            desiredEnergy = maxEnergy;
    }

    /// <summary>
    /// Add the 'value' to our currentNumber, this will give us the 'count up' effect.
    /// </summary>
    public void AddEnergy(float value)
    {
        startEnergy = currentEnergy;
        desiredEnergy += value;

        if (desiredEnergy > maxEnergy)
            desiredEnergy = maxEnergy;
    }

    public void RemoveEnergy(float value)
    {
        startEnergy = currentEnergy;
        desiredEnergy -= value;
    }

    private void Update()
    {
        // For testing purpose, only works on debug builds.
        if (Debug.isDebugBuild && infiniteEnergy)
            desiredEnergy = maxEnergy;

        #region Simple Energy Refill
        if (timer <= refillTime)
        {
            timer += Time.deltaTime;
        }
        else if (timer >= refillTime)
        {
            Debug.Log("Added " + refillAmnt + " energy, after " + refillTime + " seconds.");
            AddEnergy(refillAmnt);
            timer = 0.0f;
        }
        #endregion

        #region Energy system
        if (currentEnergy > 0)
            hasEnergy = true;
        else
            hasEnergy = false;

        if (currentEnergy == maxEnergy && desiredEnergy == currentEnergy)
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
        #endregion
    }

    private void UpdateUI()
    {
        float uiFill = currentEnergy / maxEnergy;
        fillImage.fillAmount = uiFill;
    }
}
