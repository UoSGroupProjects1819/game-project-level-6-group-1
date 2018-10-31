using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager GMInstance = null;

    //[SerializeField] private InputField nameField;

    //private string planetName;

    private void Awake()
    {
        if (GMInstance == null)
            GMInstance = this;
        else if (GMInstance != null)
            Destroy(gameObject);
    }

    private void Start ()
    {
        //if (PlayerPrefs.HasKey("playerPlanetName"))
        //{
        //    planetName = PlayerPrefs.GetString("playerPlanetName");
        //    Debug.Log("Planet Loaded!");
        //}
	}
	
	private void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    PlayerPrefs.SetString("playerPlanetName", planetName);
        //    PlayerPrefs.Save();

        //    Debug.Log("Planet saved!");
        //}
	}

    public void OnSubmit()
    {
        //planetName = nameField.text;

        //Debug.Log("Planet Name : " + planetName);
    }

    public void StartGame()
    {
        // Start the game.
    }
}
