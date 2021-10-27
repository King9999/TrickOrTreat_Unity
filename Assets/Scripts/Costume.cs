using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* This is the base class for all of the costumes in the game */

public class Costume : MonoBehaviour
{
    public new string name;
    public int candyAmount;
    public float cooldown;              //time in seconds before trick is recharged. Starts at zero so that trick can be used immediately at game start
    public float initCooldown;          //cooldown of each trick.
    public int dropAmount;              //how much candy the player drops when hit. 5 is the default.
    public int candyTaken;              //how much candy the player gets from a house per tick
    public float vx, vy;
    public float moveSpeed;                  //scales vx and vy. Lower value = slower speed
    public float actionTimer;                //how long a trick is active for.
    public bool isTrickActive = false;
    //public bool isTrickCharging = false;    //if true, trick is not active but isn't ready to be used again
    public bool isCollectingCandy = false;

    [Header("Timers")]
    public float currentTime;           //used to track when trick can be used again.
    public float currentInvulTime;      //timestamp to get current time
    public float invulDuration = 1.5f;         //time in seconds. Determines how long player is invincible
    public bool isInvincible = false;
    public float currentStunTime;
    public float stunDuration = 1f;          //time in seconds. Player can't move during this time
    public bool isStunned = false;

    //player orientation. Used to determine where to generate an action
    protected enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    protected Direction direction = Direction.Down;      //default position when game starts

    //consts  
    int MaxCandy { get; } = 999;
    int InitDropAmount { get; } = 5;
    protected float PlayerZValue { get; } = -1;       //used so that the player always remains visible on screen.

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

    public bool TrickIsCharging()
    {
        return Time.time < currentTime + cooldown;
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
            direction = Direction.Up;
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
            direction = Direction.Down;
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
            direction = Direction.Left;
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
            direction = Direction.Right;
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
        //picking up dropped candy
        if (collision.CompareTag("Candy"))
        {
            candyAmount += 1;
            Destroy(collision.gameObject);
            //TODO: Clear the object references in the candy list, then trim the list.
            Debug.Log(name + " picked up Candy");
        }

        //getting hit by a trick
        if (collision.CompareTag("Trick") && !isInvincible && !isStunned)
        {
            int candyDropped;

            //if the trick was the ghost's, double the amount of candy dropped
            if (collision.name == "Trick_Boo(Clone)")
            {
                candyDropped = candyAmount < dropAmount * 2 ? candyAmount : dropAmount * 2;
            }
            else
            {
                candyDropped = candyAmount < dropAmount ? candyAmount : dropAmount;
            }


            candyAmount -= candyDropped;

            //candy spills out in random directions.
            for (int i = 0; i < candyDropped; i++)
            {
                float randX = Random.Range(0.1f, 1f);
                float randY = Random.Range(0.1f, 1f);
                float candyZValue = -2;
                GameObject candy = Instantiate(GameManager.instance.candyPrefab, new Vector3(transform.position.x + randX, transform.position.y + randY, candyZValue), Quaternion.identity);
                GameManager.instance.candyList.Add(candy);
            }

            //The player is stunned for a duration
            isStunned = true;
            StartCoroutine(Stun());

            Debug.Log(name + " hit");
        }
    }

    IEnumerator BeginInvincibility()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        currentInvulTime = Time.time;
        while (Time.time < currentInvulTime + invulDuration)
        {
            //player sprite visibility alternates
            if (sr.enabled)
                sr.enabled = false;
            else if (!sr.enabled)
                sr.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }

        GetComponent<SpriteRenderer>().enabled = true;
        isInvincible = false;
    }

    IEnumerator Stun()
    {
        currentStunTime = Time.time;
        Vector3 originalPos = transform.position;
        float yMod = 0.1f;
        while (Time.time < currentStunTime + stunDuration)
        {
            //player remains in place and shakes for a duration
            vx = 0;
            vy = 0;           
            transform.position = new Vector3(transform.position.x, transform.position.y + yMod, transform.position.z);
            yMod *= -1;
            yield return new WaitForSeconds(0.05f);
        }

        //place player back to their original spot
        transform.position = originalPos;

        //player can now move again and they're invincible for a duration
        isStunned = false;
        isInvincible = true;
        StartCoroutine(BeginInvincibility());

    }
}
