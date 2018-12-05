using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public TMPro.TMP_InputField planetInput;

    private Animator anim;
    private string planetNameTemp;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void _PlayGame()
    {
        // Give option to start new game/load game?

        planetNameTemp = planetInput.text;
        bool correctName = CheckPlanetName(planetNameTemp);

        if (!correctName)
        {
            Debug.Log("Error starting game.");
            return;
        }

        PlayerPrefs.SetString("PlanetName", planetNameTemp);
        anim.Play("LoadingScreen");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Launching game.");
    }

    public void _NamePlanet()
    {
        anim.Play("CreationScreen");
        Debug.Log("Planet creation.");
    }

    public void _ShowMainMenu()
    {
        anim.Play("MainMenu");
        Debug.Log("Show menu.");
    }

    public void _ShowCredits()
    {
        anim.Play("CreditsScreen");
        Debug.Log("Show credits");
    }

    public void _ShowSettings()
    {
        anim.Play("SettingsScreen");
        Debug.Log("Show settings");
    }

    /* Put this somewhere else later */
    private bool CheckPlanetName(string planetName)
    {
        // Probably use Ragex to remove punctuation here, when releasing the game.
        // And maybe have a list of banned words? Yeah that'd be nice.
        if (planetName == "" || planetName == " ")
        {
            Debug.Log("Planet name can't be empty.");
            return false;
        }

        return true;
    }
}
