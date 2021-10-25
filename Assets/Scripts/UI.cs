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
    public TextMeshProUGUI[] trickText = new TextMeshProUGUI[MAX_PLAYERS];              //"Trick OK" text
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
        //set player icon
        for (int i = 0; i < PlayerManager.instance.playerCount; i++)
        {
            playerIcons[i].sprite = PlayerManager.instance.playerList[i].GetComponent<SpriteRenderer>().sprite;
            fillBars[i].fillAmount = 0;
        }

        //fill.fillAmount = 0.5f;
    }

   
    void Update()
    {
        //Update candy scores
        for (int i = 0; i < PlayerManager.instance.playerCount; i++)
        {
            playerCandyCounters[i].text = "x" + PlayerManager.instance.playerList[i].GetComponent<Costume>().candyAmount;
        }
    }
}
