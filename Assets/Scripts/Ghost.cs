using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* The ghost can target multiple players at once, making any player in range drop their candy. Also, the ghost makes players drop
 * twice as much candy. The ghost's trick has the highest cooldown. */
public class Ghost : Costume
{
    public GameObject booPrefab;
    GameObject boo;
    //float InitCooldown { get; } = 10;
    // Start is called before the first frame update
    void Start()
    {
        name = "Ghost";
        //cooldown = 0;
        initCooldown = 10;
        actionTimer = 0.5f;
        moveSpeed = 1.3f;
    }

    // Update is called once per frame
    void Update()
    {
        //player can't move while trick is active.
        if (isTrickActive)
        {
            vx = 0;
            vy = 0;
        }

        //check action timer
        if (isTrickActive && Time.time > currentTime + actionTimer)
        {
            isTrickActive = false;
            Destroy(boo);

            //restore hitbox
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    //Ghost creates a "boo" attack at their current position. Ghost cannot move while this action is performed.
    public override void UseTrick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (!isTrickActive && Time.time > currentTime + cooldown)
            {
                isTrickActive = true;
                cooldown = initCooldown;
                currentTime = Time.time;

                //temporarily disable hitbox so they aren't hit by their own attack.
                GetComponent<BoxCollider2D>().enabled = false;

                boo = Instantiate(booPrefab, transform.position, Quaternion.identity);
            }
            //else
            //play a sound or show a graphic to indicate trick can't be used yet.

        }
    }
}
