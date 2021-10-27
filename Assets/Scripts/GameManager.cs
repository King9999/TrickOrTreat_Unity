using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public Costume[] playerList = new Costume[MAX_PLAYERS];           //reference to active players. Four players maximum, 2 players minimum. Player 1 is index 0.
    //public int playerCount;                                         //number of active players
    public GameObject[] spawnPoints = new GameObject[MAX_PLAYERS];   //players begin at one of these points at random
    public List<GameObject> candyList = new List<GameObject>();     //contains dropped candy
    public GameObject candyPrefab;

    public enum CostumeType
    {
        Ghost, Knight, Princess, Witch
    }

    

    //consts
    public float ScreenBoundaryX { get; } = 10f;                 //calculated by dividing screen width by PPU (100).
    public float ScreenBoundaryY { get; } = 7f;                 //calculated by dividing screen height by PPU (100). The height is actually higher but a portion of it is for UI.
    public float yOffset { get; } = 0.68f;                             //used for offscreen checking when players move to bottom of screen, or magic goes offscreen.
    const int MAX_PLAYERS = 4;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);    //Only want one instance of game manager
            return;
        }

        instance = this;
    }

    void Start()
    {
        //get player info and have them start at a random spawn point
        //PlayerManager.instance.playerList[(int)PlayerManager.Player.One].transform.position = Vector3.zero;
        for (int i = 0; i < PlayerManager.instance.playerCount; i++)
        {
            switch(PlayerManager.instance.selectedCostumes[i])
            {
                case PlayerManager.CostumeType.Ghost:
                    PlayerManager.instance.playerList[i] = Instantiate(PlayerManager.instance.ghostPrefab);
                    //setup player icon here
                    break;

                case PlayerManager.CostumeType.Knight:
                    PlayerManager.instance.playerList[i] = Instantiate(PlayerManager.instance.knightPrefab);
                    break;

                case PlayerManager.CostumeType.Princess:
                    PlayerManager.instance.playerList[i] = Instantiate(PlayerManager.instance.princessPrefab);
                    break;

                case PlayerManager.CostumeType.Witch:
                    PlayerManager.instance.playerList[i] = Instantiate(PlayerManager.instance.witchPrefab);
                    break;
            }

            //set player's costume in UI
            //PlayerManager.instance.playerList[(int)PlayerManager.instance.currentPlayer]
            //for (int i = 0; i < PlayerManager.instance.playerCount; i++)
            //{
                //UI.instance.playerIcons[i].sprite = PlayerManager.instance.playerList[i].GetComponent<SpriteRenderer>().sprite;
            //}
            //set candy counter

            //assign the correct control setup to player 2.
            if (i == (int)PlayerManager.Player.Two)
            {
                PlayerManager.instance.playerList[i].GetComponent<PlayerInput>().SwitchCurrentActionMap("Player 2 Controls");
            }
        }
    }

    void Update()
    {
        /*****OFFSCREEN CHECK****/
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        for (int i = 0; i < PlayerManager.instance.playerCount; i++)
        {
            GameObject player = PlayerManager.instance.playerList[i];

            //left edge
            if (player.transform.position.x < screenPos.x * -ScreenBoundaryX)
            {
                player.transform.position = new Vector3(screenPos.x * -ScreenBoundaryX, player.transform.position.y, player.transform.position.z);
                Debug.Log(player.name + " Hit the left boundary");
            }

            //right edge
            if (player.transform.position.x > screenPos.x * ScreenBoundaryX)
            {
                player.transform.position = new Vector3(screenPos.x * ScreenBoundaryX, player.transform.position.y, player.transform.position.z);
                Debug.Log(player.name + "Hit the right boundary");
            }

            //top edge. Player can't move into UI space
            if (player.transform.position.y > screenPos.y * ScreenBoundaryY)
            {
                player.transform.position = new Vector3(player.transform.position.x, screenPos.y * ScreenBoundaryY, player.transform.position.z);
                Debug.Log(player.name + "Hit the top boundary");
            }

            //bottom edge
            if (player.transform.position.y < screenPos.y * -ScreenBoundaryY - yOffset)
            {
                player.transform.position = new Vector3(player.transform.position.x, screenPos.y * -ScreenBoundaryY - yOffset, player.transform.position.z);
                Debug.Log(player.name + "Hit the bottom boundary");
            }
        }
    }
}
