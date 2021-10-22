using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script manages the number of players in the game, as well as which costumes they picked.
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int playerCount;                                         //number of active players
    public GameObject[] playerList = new GameObject[MAX_PLAYERS];   //reference to active players. Four players maximum, 2 players minimum. Player 1 is index 0, etc.

    public GameObject ghostPrefab;
    public GameObject knightPrefab;
    public GameObject princessPrefab;
    public GameObject witchPrefab;

    //bools
    public bool p1Picked;
    public bool p2Picked;
    public bool allPlayersSelectedCostumes;            //used to move to game screen when true.
    bool onGameScreen;

    //consts
    const int MAX_PLAYERS = 4;
    int MinPlayers { get; } = 2;

    public enum CostumeType
    {
        Ghost, Knight, Princess, Witch
    }

    public CostumeType[] selectedCostumes = new CostumeType[MAX_PLAYERS];      

    public enum Player
    {
        One, Two, Three, Four
    }

    [HideInInspector] public Player currentPlayer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);    //need this object to survive so game manager can use it
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCount = MinPlayers;
        allPlayersSelectedCostumes = false;
        onGameScreen = false;
        p1Picked = false;
        p2Picked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (p1Picked && p2Picked)
            allPlayersSelectedCostumes = true;

        if (!onGameScreen && allPlayersSelectedCostumes)
        {
            onGameScreen = true;
            SceneManager.LoadScene("Game");
        }
    }
}
