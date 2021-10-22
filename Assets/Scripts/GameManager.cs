using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Costume[] playerList = new Costume[MAX_PLAYERS];           //reference to active players. Four players maximum, 2 players minimum. Player 1 is index 0.
    public int playerCount;                                         //number of active players
    public GameObject[] spawnPoints = new GameObject[MAX_PLAYERS];   //players begin at one of these points at random

    public enum CostumeType
    {
        Ghost, Knight, Princess, Witch
    }

    

    //consts
    public float ScreenBoundaryX { get; } = 10.24f;                 //calculated by dividing screen width by PPU (100).
    public float ScreenBoundaryY { get; } = 7.68f;                 //calculated by dividing screen height by PPU (100). The height is actually higher but a portion of it is for UI.
    const int MAX_PLAYERS = 4;
    int MinPlayers { get; } = 2;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);    //Only want one instance of game manager
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCount = MinPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
