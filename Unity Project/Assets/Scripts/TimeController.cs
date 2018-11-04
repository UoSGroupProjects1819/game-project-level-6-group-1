using System;
using UnityEngine;

public class TimeController : MonoBehaviour {

    private double secondsPassed;
    private string timeKey = "_gameTime";

    /// <summary>
    /// Returns amount of seconds, that have passed between player sessions.
    /// </summary>
    public double GetTimeDifference()
    {
        DateTime oldDate;           // The old time, that has been saved to the PlayerPrefs.
        DateTime currentDate;       // Current date that has been loaded when the game was launched.

        currentDate = System.DateTime.Now;

        if (PlayerPrefs.HasKey(timeKey))
        {
            // If the game was run previously, get the time difference.
            // Convert the old time from binary to DataTime variable
            long tempTime = Convert.ToInt64(PlayerPrefs.GetString(timeKey));
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

        // Return the seconds that have passed.
        return secondsPassed;
    }

    /// <summary>
    /// Save the current time to PlayerPrefs. Use this, rather than manually saving it.
    /// </summary>
    public void SaveTime()
    {
        PlayerPrefs.SetString(timeKey, DateTime.Now.ToBinary().ToString());
    }
}
