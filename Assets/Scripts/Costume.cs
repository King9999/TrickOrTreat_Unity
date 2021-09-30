using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* This is the base class for all of the costumes in the game */

public class Costume : MonoBehaviour
{
    public new string name;
    public int candyAmount = 0;
    public float cooldown;                  //time in seconds before trick is recharged
    public int dropAmount = 5;              //how much candy the player drops when hit. 5 is the default.
    public int candyTaken = 1;              //how much candy the player gets from a house per tick
    public float vx, vy = 0;
    public float moveSpeed = 1.3f;          //scales vx and vy. Lower value = slower speed
    public float actionTimer;                //how long a trick is active for.
    public bool isTrickActive = false;
    public float currentTime = 0;           //used to track when trick can be used again.

    //consts  
    public int MaxCandy { get; } = 999;
    public int InitDropAmount { get; } = 5;
    float PlayerZValue { get; } = -1;       //used so that the player always remains visible on screen.

    public void AddCandy(short amount)
    {
        if (candyAmount + amount <= MaxCandy)
            candyAmount += amount;
    }

    public void DropCandy(short amount)
    {
        if (candyAmount >= amount)
            candyAmount -= amount;
    }

    public void SetCandyAmount(short amount)
    {
        candyAmount = amount;
    }

    /*public virtual void UseTrick()  //to be overridden by other classes.
    {
        if (Time.time > currentTime + cooldown)
        {
            isTrickActive = true;
            currentTime = Time.time;
        }
        //else
            //play a sound or show a graphic to indicate trick can't be used yet.
    }*/

    #region Controls
    //Movement controls TODO: Is the vertical axis still bugged?
    public void MoveUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //move player up
            vy = moveSpeed;
        }
        else
        {
            vy = 0;
        }
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //move player down
            vy = -moveSpeed;
        }
        else
        {
            vy = 0;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //move player left
            vx = -moveSpeed;
        }
        else
        {
            vx = 0;
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //move player right
            vx = moveSpeed;
        }
        else
        {
            vx = 0;
        }
    }

    public virtual void UseTrick(InputAction.CallbackContext context)
    {
        /*if (context.phase == InputActionPhase.Performed && Time.time > currentTime + cooldown)
        {           
            isTrickActive = true;
            currentTime = Time.time;
        }*/
        //else
            //play a sound or show a graphic to indicate trick can't be used yet.
    }
    #endregion

    private void FixedUpdate()
    {
        //update player movement
        transform.position = new Vector3(transform.position.x + (vx * Time.deltaTime),
            transform.position.y + (vy * Time.deltaTime), PlayerZValue);
    }

    //check for collision with candy triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Trigger"))
        {
            Debug.Log("Collecting Candy");
        }
    }

}
