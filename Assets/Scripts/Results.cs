using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//Used to display the winner. Also checks for input from a player to go to costume select screen.
public class Results : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI promptText;

    // Start is called before the first frame update
    void Start()
    {
        //screen is hidden by default and only appears when time is up.
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.instance.timer.TimeUp())
        {
            //gameObject.SetActive(true);

            //display winner and candy amounts for each player in order from highest to lowest.
            int mostCandy = 0;
            int winningPlayer = 0;
            for (int i = 0; i < PlayerManager.instance.playerCount; i++)
            {
                //find the highest candy amount
                Costume player = PlayerManager.instance.playerList[i].GetComponent<Costume>();
                if (player.candyAmount > mostCandy)
                {
                    mostCandy = player.candyAmount;
                    winningPlayer = i;
                }
            }

            if (mostCandy <= 0)
                winnerText.text = "Draw...";
            else
                winnerText.text = "Player " + (winningPlayer + 1) + " wins!";

            promptText.text = "Press any key";

            if (Keyboard.current.anyKey.isPressed) //TODO: how to get input from any gamepad button?
            {
                //PlayerManager.instance.onGameScreen = false;
                //PlayerManager.instance.p1Picked = false;
               // PlayerManager.instance.p2Picked = false;
               // PlayerManager.instance.allPlayersSelectedCostumes = false;            //used to move to game screen when true.
                Destroy(PlayerManager.instance.gameObject);             //want to force a new instance of player manager
                SceneManager.LoadScene("CostumeSelect");
            }
        }

    }
}
