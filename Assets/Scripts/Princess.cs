using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* The princess does not have a trick, but gets more candy from houses, and has higher move speed */
public class Princess : Costume
{
    public GameObject questionMarkPrefab;
    GameObject questionMark;
    float trickPosOffset = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        name = "Princess";
        //candyAmount = 0;
        initCooldown = 1;
        //dropAmount = 5;
        candyTaken = 2;
        moveSpeed = 1.65f;
        actionTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if trick is active, it will follow the player wherever they move.
        if (isTrickActive)
        {
            questionMark.transform.position = new Vector3(transform.position.x, transform.position.y + trickPosOffset, PlayerZValue);
        }

        //check action timer
        if (isTrickActive && Time.time > currentTime + actionTimer)
        {
            isTrickActive = false;
            Destroy(questionMark);
        }
    }

    /* The Princess doesn't have a trick, but something amusing should happen when player tries to use it */
    public override void UseTrick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && UI.instance.gameStarted)
        {
            if (!isTrickActive && Time.time > currentTime + cooldown)
            {
                isTrickActive = true;
                cooldown = initCooldown;
                currentTime = Time.time;

                
                questionMark = Instantiate(questionMarkPrefab, new Vector3(transform.position.x, transform.position.y + trickPosOffset, PlayerZValue), Quaternion.identity);
            }
            //else
            //play a sound or show a graphic to indicate trick can't be used yet.

        }
    }
}
