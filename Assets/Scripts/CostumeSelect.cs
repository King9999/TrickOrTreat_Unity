using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Player selects their costume here. A description of the costume and their trick can be found here also.
public class CostumeSelect : MonoBehaviour
{
    //public TextMeshProUGUI costumeDescription;
    //public TextMeshProUGUI trickData;
    string[] costumeDescriptions = new string[4];
    string[] trickDescriptions = new string[4];

    enum CostumeType
    {
        Ghost, Knight, Princess, Witch
    }

    CostumeType currentCostume;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void OnPressLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (currentCostume - 1 >= CostumeType.Ghost)
                currentCostume--;
            else
                currentCostume = CostumeType.Witch;

            //move cursor to next costume
            //cursor.transform.position = menus[currentMenu];
            
            Debug.Log("Pressed Left");
        }
    }*/

    /*public void OnPressRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (currentCostume + 1 <= CostumeType.Witch)
                currentCostume++;
            else
                currentCostume = CostumeType.Ghost;

            //move cursor to next costume
            //cursor.transform.position = menus[currentMenu];

            Debug.Log("Pressed Right");
        }
    }*/

    /*public void OnSelectedCostume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //check what player 1 and 2 selected and carry that info over to game screen.
            if (currentMenu == START)
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
            }
            //Debug.Log("Selected Menu Option");
        }
    }*/
}
