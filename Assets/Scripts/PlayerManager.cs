using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //consts
    const int MAX_PLAYERS = 4;

    enum CostumeType
    {
        Ghost, Knight, Princess, Witch
    }

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
        DontDestroyOnLoad(instance);    //need this object to survive so game manager can use it
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
