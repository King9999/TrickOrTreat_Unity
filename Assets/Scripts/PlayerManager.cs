using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    //cursor
    public GameObject[] cursors = new GameObject[2];

    //bools
    public bool p1Picked;
    public bool p2Picked;
    public bool allPlayersSelectedCostumes;            //used to move to game screen when true.
    public bool ghostPicked;
    public bool knightPicked;
    public bool princessPicked;
    public bool witchPicked;
    [HideInInspector]public bool onGameScreen;

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
        //cursor management
        if (!onGameScreen && cursors[(int)Player.Two].transform.position == cursors[(int)Player.One].transform.position)
        {
            //move p2 cursor above p1's
            Image cursorImg = cursors[(int)Player.Two].GetComponent<Image>();
            cursors[(int)Player.Two].transform.position = new Vector3(cursors[(int)Player.Two].transform.position.x, cursors[(int)Player.Two].transform.position.y + cursorImg.sprite.rect.height + 10,
                cursors[(int)Player.Two].transform.position.z);
        }

        if (p1Picked && p2Picked)
            allPlayersSelectedCostumes = true;

        if (!onGameScreen && allPlayersSelectedCostumes)
        {
            onGameScreen = true;
            SceneManager.LoadScene("Game");
        }
    }
}
