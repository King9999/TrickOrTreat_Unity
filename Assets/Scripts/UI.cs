using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//This script handles the player UI details such as icons, candy amount, and whether tricks can be used.

public class UI : MonoBehaviour
{
    [Header("UI Info")]
    public TextMeshProUGUI[] playerCandyCounters = new TextMeshProUGUI[MAX_PLAYERS];    //score
    public Image[] playerIcons = new Image[MAX_PLAYERS];
    public float[] cooldownTimers = new float[MAX_PLAYERS];
    public Image[] fillBars = new Image[MAX_PLAYERS];                                   //visual cooldown when trick is used
    public GameObject[] playerData = new GameObject[MAX_PLAYERS];
    public TextMeshProUGUI[] trickText = new TextMeshProUGUI[MAX_PLAYERS];              //"Trick OK" text
    public TextMeshProUGUI countdownText;                       //appears at the start of a match

    [Header("Timers")]
    public bool gameStarted;                                    //used to prevent game from running while countdown is in effect
    public Timer timer;
    public Countdown countdown;
    //public GameObject[] playerHUD;

    //cooldown bar 
    //public float minValue, maxValue;
    //public Image fill;

    public static UI instance;

    const int MAX_PLAYERS = 4;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    
    void Start()
    {
        //set player icons. Disable UI for inactive players.
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            if (i < PlayerManager.instance.playerCount)
            {
                playerIcons[i].sprite = PlayerManager.instance.playerList[i].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                playerData[i].SetActive(false);
            }
        }

        //timer.timerRunning = true;
        countdown.SetTimer(4);          //set it to 4 due to how quickly the countdown starts. Wait time should be 3 seconds in game.
        countdown.timerRunning = true;
    }


    void Update()
    {

        //Update candy scores
        if (gameStarted)
        {
            timer.timerRunning = true;

            for (int i = 0; i < PlayerManager.instance.playerCount; i++)
            {
                playerCandyCounters[i].text = "x" + PlayerManager.instance.playerList[i].GetComponent<Costume>().candyAmount;
            }

            /*******Update cooldown timers*****/
            for (int i = 0; i < PlayerManager.instance.playerCount; i++)
            {
                Costume player = PlayerManager.instance.playerList[i].GetComponent<Costume>();

                //did a player use a trick recently?
                if (player.isTrickActive)
                {
                    cooldownTimers[i] = player.currentTime + player.cooldown;
                }

                if (!player.isTrickActive && player.TrickIsCharging())
                {
                    //show cooldown bar and update it. Must subtract player.currentTime (which acts as the minimum value) on both sides for accurate reading of bar.
                    fillBars[i].enabled = true;
                    trickText[i].alpha = 0.3f;
                    fillBars[i].fillAmount = (Time.time - player.currentTime) / (cooldownTimers[i] - player.currentTime);
                }
                else
                {
                    fillBars[i].enabled = false;
                    trickText[i].alpha = 1;
                }
            }
        }
    }
}
