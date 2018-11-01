﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour {

    public static UIManager UIInstance = null;

    [SerializeField] private GameObject MAINMENU_UI;
    [SerializeField] private Animator planetNameAnim;

    private CameraMovement camMovement;

    private void Awake()
    {
        if (UIInstance == null)
            UIInstance = this;
        else if (UIInstance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        camMovement = Camera.main.GetComponent<CameraMovement>();
    }

    public void GOTOMAINMENU()
    {
        camMovement.moveTowards = new Vector3(0, 0, -10);
    }

    public void GOTOSETTINGS()
    {
        camMovement.moveTowards = new Vector3(18, 0, -10);
    }

    public void PLAYGAME()
    {
        MAINMENU_UI.SetActive(false);
        planetNameAnim.Play("PlanetNameDropDown");
    }
}
