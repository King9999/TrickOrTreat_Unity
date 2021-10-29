using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* This is the base class for all of the costumes in the game */

public class Costume : MonoBehaviour
{
    public new string name;
    public int candyAmount;
    public float cooldown;                  //time in seconds before trick is recharged. Starts at zero so that trick can be used immediately at game start
    public float initCooldown;              //cooldown of each trick.
    public int dropAmount = 5;              //how much candy the player drops when hit. 5 is the default.
    public int candyTaken = 1;              //how much candy the player gets from a house per tick
    public float vx, vy;
    public float moveSpeed = 1.3f;           //scales vx and vy. Lower value = slower speed
    public float actionTimer;                //how long a trick is active for.
    public bool isTrickActive = false;
    //public bool isTrickCharging = false;    //if true, trick is not active but isn't ready to be used again
    public bool isCollectingCandy = false;

    [Header("Timers")]
    public float currentTime;                   //used to track when trick can be used again.
    public float currentInvulTime;              //timestamp to get current time
    public float invulDuration = 1.5f;         //time in seconds. Determines how long player is invincible
    public bool isInvincible = false;
    public float currentStunTime;
    public float stunDuration = 1f;          //time in seconds. Player can't move during this time
    public bool isStunned = false;
    
    [Header("AI")]
    public bool isAI;                       //if true, AI controls all other costumes not controlled by players.
    public bool locationSet;                //when true, AI's location does not change until they reach their destination or something causes the location to no longer be valid.
    //House houseWithMostCandy;
    //House houseWithShortestDistance;


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

    #region AI Actions 

    public void MoveToLocation(Vector3 destination)
    {
        //AI player moves to given location. NOTE: Will likely have to do some raycasting to avoid obstacles
        //float posDiffX = transform.position.x - destination.x;
        if (destination.x > transform.position.x)
        {
            vx = moveSpeed;
        }
        else if (destination.x < transform.position.x)
        {
            vx = -moveSpeed;
        }

        if (destination.y > transform.position.y)
        {
            vy = moveSpeed;
        }

        if (destination.y < transform.position.y)
        {
            vy = -moveSpeed;
        }
    
        /*else
        {
            vx = 0;
            vy = 0;
        }*/
    }

    public void SeekPlayer()
    {
        //approach player if they're within the detector's range
    }

    public void SearchHouses()
    {
        /*check all houses with candy and:
         * 1. find one with the most candy, or
         * 2. find the nearest house
         * AI will choose randomly between the two (70/30?)
         * if AI finds a house with an unknown amount, 20% chance it approaches the house, otherwise it'll be treated as having no candy.*/

        float shortestDistance = 0;
        float mostCandy = 0;
        House houseWithMostCandy = HouseManager.instance.houses[6];
        House houseWithShortestDistance = HouseManager.instance.houses[6];
        /*for (int i = 0; i < HouseManager.instance.houses.Length; i++)
        {
            //get the distance of each house, and get the nearest house and the one with the most candy.
            if (HouseManager.instance.houses[i].HasCandy())
            {
                float distanceToHouse = Vector3.Distance(transform.position, HouseManager.instance.houses[i].transform.position);
                if (distanceToHouse < shortestDistance)
                {
                    shortestDistance = distanceToHouse;
                    houseWithShortestDistance = HouseManager.instance.houses[i];
                }

                if (HouseManager.instance.houses[i].candyAmount > mostCandy)
                {
                    houseWithMostCandy = HouseManager.instance.houses[i];
                    mostCandy = HouseManager.instance.houses[i].candyAmount;
                }
            }
        }*/

        Debug.Log("House with most candy is " + houseWithMostCandy.name);
        Debug.Log("Closest house is " + houseWithShortestDistance.name);

        //roll to decide which house AI goes for
        if (!locationSet)
        {
            float randNum = Random.Range(0f, 1f);

            if (randNum <= 0.3f)
            {
                //go for house with shortest distance
                MoveToLocation(houseWithShortestDistance.transform.position /*+ houseWithShortestDistance.triggerLocation*/);
                Debug.Log("Closest house pos is " + houseWithShortestDistance.transform.position);
            }
            else
            {
                //go for house with most candy
                MoveToLocation(houseWithMostCandy.transform.position /*+ houseWithMostCandy.triggerLocation*/);
                Debug.Log("House with most candy's pos is " + houseWithMostCandy.transform.position);
            }

            locationSet = true;
        }
    }


    #endregion

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
