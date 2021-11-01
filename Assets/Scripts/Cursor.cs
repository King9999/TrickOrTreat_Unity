using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Cursor : MonoBehaviour
{
    public Image[] costumes = new Image[MAX_COSTUMES];  //contain references to the costumes on select screen. Needed for cursor placement.

    public bool p1Cursor;
    public bool p2Cursor;       //used to determine who is player 1 or 2
    public bool p1Picked;
    public bool p2Picked;       //once all players choose, move to game screen.
    public TextMeshProUGUI okText;

    enum CostumeType
    {
        Ghost, Knight, Princess, Witch
    }

    CostumeType currentCostume;

    //consts
    const int MAX_COSTUMES = 4;
    //const int GHOST = 0;
    //const int KNIGHT = 1;           //index locations in array
    //const int PRINCESS = 2;
    //const int WITCH = 3;
    const float yOffset = 75;
    const float xOffset = -10;

    // Start is called before the first frame update
    void Start()
    {
        //set initial cursor position. They can occupy same space but players must select different costumes.
        transform.position = new Vector3(costumes[(int)CostumeType.Ghost].transform.position.x + xOffset, costumes[(int)CostumeType.Ghost].transform.position.y + yOffset, transform.position.z);
        p1Picked = false;
        p2Picked = false;
        okText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //move to next screen
        //if (PlayerManager.instance.p1Picked == true && p2Picked == true)
            //PlayerManager.instance.allPlayersSelectedCostumes = true;

        //update cursor's location if necessary
        transform.position = new Vector3(costumes[(int)currentCostume].transform.position.x + xOffset, costumes[(int)currentCostume].transform.position.y + yOffset, transform.position.z);
    }

    public void OnPressLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !okText.enabled)
        {
            if (currentCostume - 1 >= CostumeType.Ghost)
                currentCostume--;
            else
                currentCostume = CostumeType.Witch;

            //move cursor to next costume
            //cursor.transform.position = menus[currentMenu];

            Debug.Log("Pressed Left");
        }
    }

    public void OnPressRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !okText.enabled)
        {
            if (currentCostume + 1 <= CostumeType.Witch)
                currentCostume++;
            else
                currentCostume = CostumeType.Ghost;

            //move cursor to next costume
            //cursor.transform.position = menus[currentMenu];

            Debug.Log("Pressed Right");
        }
    }

    public void OnSelectedCostume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !okText.enabled)
        {
            //which player is this?
            if (p1Cursor == true)
            {
                PlayerManager.instance.currentPlayer = PlayerManager.Player.One;
                //get costume object and send it to game
                switch (currentCostume)
                {
                    case CostumeType.Ghost:
                        if (!PlayerManager.instance.ghostPicked)
                        {
                            PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Ghost;
                            PlayerManager.instance.ghostPicked = true;
                            PlayerManager.instance.p1Picked = true;
                            okText.enabled = true;
                        }
                        break;

                    case CostumeType.Knight:
                        if (!PlayerManager.instance.knightPicked)
                        {
                            PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Knight;
                            PlayerManager.instance.knightPicked = true;
                            PlayerManager.instance.p1Picked = true;
                            okText.enabled = true;
                        }
                        break;

                    case CostumeType.Princess:
                        if (!PlayerManager.instance.princessPicked)
                        {
                            PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Princess;
                            PlayerManager.instance.princessPicked = true;
                            PlayerManager.instance.p1Picked = true;
                            okText.enabled = true;
                        }
                        break;

                    case CostumeType.Witch:
                        if (!PlayerManager.instance.witchPicked)
                        {
                            PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Witch;
                            PlayerManager.instance.witchPicked = true;
                            PlayerManager.instance.p1Picked = true;
                            okText.enabled = true;
                        }
                        break;

                    default:
                        break;
                }
            }
            else if (p2Cursor == true)
            {
                PlayerManager.instance.currentPlayer = PlayerManager.Player.Two;
                //get costume object and send it to game
                switch (currentCostume)
                {
                    case CostumeType.Ghost:
                        if (!PlayerManager.instance.ghostPicked)
                        {
                            PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Ghost;
                            PlayerManager.instance.ghostPicked = true;
                            PlayerManager.instance.p2Picked = true;
                            okText.enabled = true;
                        }
                        break;

                    case CostumeType.Knight:
                        if (!PlayerManager.instance.knightPicked)
                        {
                            PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Knight;
                            PlayerManager.instance.knightPicked = true;
                            PlayerManager.instance.p2Picked = true;
                            okText.enabled = true;
                        }
                        break;

                    case CostumeType.Princess:
                        if (!PlayerManager.instance.princessPicked)
                        {
                            PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Princess;
                            PlayerManager.instance.princessPicked = true;
                            PlayerManager.instance.p2Picked = true;
                            okText.enabled = true;
                        }
                        break;

                    case CostumeType.Witch:
                        if (!PlayerManager.instance.witchPicked)
                        {
                            PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Witch;
                            PlayerManager.instance.witchPicked = true;
                            PlayerManager.instance.p2Picked = true;
                            okText.enabled = true;
                        }
                        break;

                    default:
                        break;
                }
            }

            //okText.enabled = true;

            //get costume object and send it to game
           /* switch (currentCostume)
            {
                case CostumeType.Ghost:
                    if (!PlayerManager.instance.ghostPicked)
                    {
                        PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Ghost;
                        PlayerManager.instance.ghostPicked = true;
                    }
                    break;

                case CostumeType.Knight:
                    PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Knight;
                    break;

                case CostumeType.Princess:
                    PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Princess;
                    break;

                case CostumeType.Witch:
                    PlayerManager.instance.selectedCostumes[(int)PlayerManager.instance.currentPlayer] = PlayerManager.CostumeType.Witch;
                    break;

                default:
                    break;
            }*/

            //move to next screen
            //if (p1Picked == true && p2Picked == true)
                //SceneManager.LoadScene("Game");

            /*if (currentMenu == START)
            {
                //load game
                StartCoroutine(OpenScene("Game"));
            }
            else if (currentMenu == HELP)
            {
                //load help
                StartCoroutine(OpenScene("Help"));
            }
            else
            {
                //toggle sound
                muted = !muted;
                soundToggleText.text = (muted == true) ? "Off" : "On";
                if (muted == false)
                    //play sound to alert player that sound is on.
                    soundSource.Play();
            }*/
            //Debug.Log("Selected Menu Option");
        }
    }

}
