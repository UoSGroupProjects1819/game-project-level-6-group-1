using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager GMInstance = null;

    private DateTime oldDate;
    private DateTime currentDate;
    private double secondsPassed;

    private void Awake()
    {
        if (GMInstance == null)
            GMInstance = this;
        else if (GMInstance != null)
            Destroy(gameObject);
    }

    private void Start ()
    {
        GetTimeDifference();
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

    private void GetTimeDifference()
    {
        // Getting the current time.
        currentDate = System.DateTime.Now;

        // Get the old time.
        if (PlayerPrefs.HasKey("sysTime"))
        {
            // If the game was run previously, get the time difference.
            long tempTime = Convert.ToInt64(PlayerPrefs.GetString("sysTime"));

            // Convert the old time from binary to DataTime variable
            oldDate = DateTime.FromBinary(tempTime);
            Debug.Log("Old Time: " + oldDate);
        }
        else
        {
            // Else if the game was not run before, set the previous time to currnent time.
            // This will avoid null exception.
            oldDate = System.DateTime.Now;
        }

        TimeSpan difference = currentDate.Subtract(oldDate);
        Debug.Log("Difference: " + difference);

        secondsPassed = difference.Seconds;
        Debug.Log("Seconds passed: " + secondsPassed);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("sysTime", System.DateTime.Now.ToBinary().ToString());
        Debug.Log("Saving time to prefs: " + System.DateTime.Now);
    }
}
