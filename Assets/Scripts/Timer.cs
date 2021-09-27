using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is used to track each game. Default time is 2 minutes. */

public class Timer : MonoBehaviour
{
    public int minutes;
    public int seconds;
    public float milliseconds;   //counts down from 99. Not actually milliseconds
    public bool timerRunning;

    // Start is called before the first frame update
    void Start()
    {
        minutes = 2;
        seconds = 0;
        milliseconds = 0;
        timerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //timer is displayed as a string so that extra zeroes can be added when necessary to display correct time
        if (timerRunning)
        {
            //millisecond countdown
            milliseconds -= 1 * Time.deltaTime;           
            if (milliseconds < 0)
            {
                milliseconds = 99;
                seconds--;
            }

            //second countdown
            if (seconds < 0)
            {
                seconds = 59;
                minutes--;
            }

            //minute countdown
            if (minutes < 0)
            {
                //stop timer
                timerRunning = false;
                minutes = 0;
                seconds = 0;
                milliseconds = 0;
            }

        }
    }

    public void SetTimer(int minutes, int seconds)
    {
        //timer will not exceed five minutes, and seconds cannot exceed 60      
        this.minutes = (minutes > 5) ? 5 : minutes;
        this.seconds = (seconds > 59) ? 59 : seconds;
    }

    public string DisplayTimer()
    {
        string seconds, milliseconds;
        //append zeroes when necessary
        seconds = (this.seconds < 10) ? "0" + this.seconds : this.seconds.ToString();
        milliseconds = (this.seconds < 10) ? "0" + this.milliseconds : this.milliseconds.ToString();

        return minutes + ":" + seconds + ":" + milliseconds;
    }

}
