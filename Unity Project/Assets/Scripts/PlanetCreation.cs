using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCreation : MonoBehaviour
{

    public TMPro.TMP_InputField planetNameInput;

    private GameObject planetCreationMenu;
    private bool isActive = false;
    private string _tempPlanetName;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("CreationMenu"))
            planetCreationMenu = GameObject.FindGameObjectWithTag("CreationMenu");
        else { /* Debug.LogError("Cannot find planet craetion menu!"); */ }
    }

    public void ShowCreationMenu()
    {
        UIManager.UIInstance.ShowCreationMenu();
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                _tempPlanetName = planetNameInput.text;

                if (_tempPlanetName != "" || _tempPlanetName != " ")
                {
                    GameManager.GMInstance.planetName = _tempPlanetName;
                    GameManager.GMInstance.StartGame();
                    StartCoroutine(MenuDisappearDelay());
                    isActive = false;
                }
                else { Debug.LogError("Invalid planet name!"); }
            }
        }
    }

    private IEnumerator MenuDisappearDelay()
    {
        yield return new WaitForSeconds(1f);
        planetCreationMenu.SetActive(false);
        StopCoroutine(MenuDisappearDelay());
    }
}
