using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager GMInstance = null;

    // This is how much time has passed between each play sessions, this can be used to compare
    // if objects have finished growing. The times are saved to the PlayerPrefs.
    [HideInInspector] public double secondsPassed;

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

    private void GetTimeDifference()
    {
        DateTime oldDate;           // The old time, that has been saved to the PlayerPrefs.
        DateTime currentDate;       // Current date that has been loaded when the game was launched.

        currentDate = System.DateTime.Now;

        if (PlayerPrefs.HasKey("sysTime"))
        {
            // If the game was run previously, get the time difference.
            // Convert the old time from binary to DataTime variable
            long tempTime = Convert.ToInt64(PlayerPrefs.GetString("sysTime"));
            oldDate = DateTime.FromBinary(tempTime);
        }
        else
        {
            // Else if the game was not run before, set the previous time to currnent time.
            // This will avoid null exception.
            oldDate = System.DateTime.Now;
        }

        // Calculate the difference, and convert it into seconds.
        TimeSpan difference = currentDate.Subtract(oldDate);
        secondsPassed = difference.Seconds;
    }

    private void OnApplicationQuit()
    {
        // When quitting the game, save the time to PlayerPrefs.
        PlayerPrefs.SetString("sysTime", System.DateTime.Now.ToBinary().ToString());
    }
}
