using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//This script handles the player UI details such as icons, candy amount, and whether tricks can be used.

public class UI : MonoBehaviour
{
    public TextMeshProUGUI[] playerCandyCounters = new TextMeshProUGUI[MAX_PLAYERS];
    public Image[] playerIcons = new Image[MAX_PLAYERS];
    public float[] cooldownTimers = new float[MAX_PLAYERS];
    public TextMeshProUGUI trickText;
    //public GameObject[] playerHUD;

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
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerManager.instance.playerCount; i++)
        {
            playerIcons[i].sprite = PlayerManager.instance.playerList[i].GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UI is updated frequently.
        for (int i = 0; i < PlayerManager.instance.playerCount; i++)
        {
            playerCandyCounters[i].text = "x" + PlayerManager.instance.playerList[i].GetComponent<Costume>().candyAmount;
        }
    }
}
