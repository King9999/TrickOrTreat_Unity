using UnityEngine;
using TMPro;

//this script is used to start the game and let players get ready. No actions can be performed during countdown.

public class Countdown : MonoBehaviour
{
    public float time;           //time in seconds. Will use this value to get the minutes, seconds and milliseconds
    public bool timerRunning;
    float displayTime = 1;
    float currentTimestamp;

    //UI
    public TextMeshProUGUI countdownUI;

    // Start is called before the first frame update
    void Start()
    {
        //timerRunning = false;
        //time = 3;             
    }

    // Update is called once per frame
    void Update()
    {
        //timer is displayed as a string so that extra zeroes can be added when necessary to display correct time
        if (timerRunning)
        {
            time -= Time.deltaTime;

            if (time < 1)
            {
                //display a "Start" message, then disappear. Players can move during this time.
                //time = 0;
                countdownUI.text = "GO!";
                timerRunning = false;
                currentTimestamp = Time.time;
                UI.instance.gameStarted = true;
            }
            else
                countdownUI.text = DisplayTimer();

        }
        else
        {
            if (Time.time > currentTimestamp + displayTime)
            {
                //countdownUI.text = "GO!";
                gameObject.SetActive(false);
            }

            //hide the timer
            //enabled = false;
        }


    }

    public void SetTimer(float seconds)
    {
        time = seconds;
    }

    public string DisplayTimer()
    {
        //float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        //float milliseconds = time % 1 * 99;    //I only want the first two digits displayed so I'm not using 1000. Format wouldn't remove the extra digits.

        string timeText = string.Format("{0:0}", seconds);

        return timeText;
    }

    public bool TimeUp()
    {
        return time <= 0;
    }
}
