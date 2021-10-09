using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* A house can hold candy for players to pick up. Occasionally they may have more candy than the usual maximum capacity. A house can only be approached
 when its lights are on. */
public class House : MonoBehaviour
{
    public int candyAmount;             //random amount from 1 to 10, with a small chance to be 20.
    public float restockTime;           //time in seconds that must pass before house has candy. This value is randomized for each house.
    public float currentTime;           //gets timestamp of current time to enable cooldown for restocking candy
    public float candyPickupCurrentTime;
    public bool candyAmountIsHidden;    //if true, players will not know how much candy a house has until they approach
    public bool canHaveCandy;           //must be true in order to stock candy
    public bool candyBeingCollected;
    Costume player;                     //reference to the player collecting candy

    public Sprite houseLightsOff;
    public Sprite houseLightsOn;        //this sprite is used when a house has candy available
    //public TextMeshProUGUI candyUI;     //displays candy stock

    //consts
    public float MaxCandyChance { get; } = 0.04f;  //4% chance
    public float HiddenCandyChance { get; } = 0.1f; //10% chance. If sucessful, the amount of candy a house has is hidden until player approaches house
    public int MaxCandyAmount { get; } = 10;
    public int MinRestockTime { get; } = 5;
    public int MaxRestockTime { get; } = 10;
    public float CandyPickupRate { get; } = 0.5f;   //controls how quickly player acquires candy from a house.


    private void Start()
    {
        restockTime = Random.Range(MinRestockTime, MaxRestockTime + 1);
        currentTime = 0;
        canHaveCandy = false;
        candyBeingCollected = false;
    }

    //fill a house with candy.
    public void StockUp()
    {
        float randNum = Random.Range(0f, 1f);

        //check if house will have max candy
        if (randNum <= MaxCandyChance)
        {
            candyAmount = MaxCandyAmount * 2;
        }
        else
        {
            candyAmount = Random.Range(1, MaxCandyAmount + 1);
        }

        //is the candy amount hidden?
        randNum = Random.Range(0f, 1f);
        candyAmountIsHidden = randNum <= HiddenCandyChance;

        //turn the lights on
        GetComponent<SpriteRenderer>().sprite = houseLightsOn;

        /*if (candyAmountIsHidden)
            candyUI.text = "??";
        else
            candyUI.text = candyAmount.ToString();*/
    }

    public bool HasCandy()
    {
        return candyAmount > 0;
    }

    private void Update()
    {
        /*if (Time.time > currentTime + restockTime)
            canHaveCandy = true;

        if (canHaveCandy && HouseManager.instance.housesWithCandy < HouseManager.instance.TotalHousesWithCandy)
        {
            //if (!HasCandy() /*&& Time.time > currentTime + restockTime*///)
            //{
                //StockUp();
                //currentTime = Time.time;
                //HouseManager.instance.housesWithCandy++;
            //}
        //}

        //TODO: Figure out how to disable the box collider in the child component without disabling the parent.
        //GetComponentInChildren<BoxCollider2D>().enabled = false;
        

        /*if (HasCandy())
        {
            GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponentInChildren<BoxCollider2D>().enabled = false;
        }*/

        if (candyBeingCollected && Time.time > candyPickupCurrentTime + CandyPickupRate)
        {
            player.candyAmount += player.candyTaken;
            candyAmount -= player.candyTaken;               //removes candy from house

            candyPickupCurrentTime = Time.time;

            if (!HasCandy())
            {
                //time to restock
                currentTime = Time.time;
                restockTime = Random.Range(MinRestockTime * 2, MaxRestockTime * 2 + 1);     //restock takes longer during a game
                canHaveCandy = false;
                candyBeingCollected = false;

                //lights out!
                GetComponent<SpriteRenderer>().sprite = houseLightsOff;
                HouseManager.instance.housesWithCandy--;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)  //this method allows for continuous execution of the code every frame as long as there's a collision
    {
        if (collision.CompareTag("Player"))
        {
            if (HasCandy() /*&& Time.time > candyPickupCurrentTime + CandyPickupRate*/)
            {
                
                Debug.Log("Collecting Candy");
                //player starts collecting candy if the house has any available
                candyAmountIsHidden = false;
                candyBeingCollected = true;
                player = collision.GetComponent<Costume>();     //need reference to the player currently at the house
                /*player.candyAmount += player.candyTaken;
                candyAmount -= player.candyTaken;               //removes candy from house

                //candyPickupCurrentTime = Time.time;

                if (!HasCandy())
                {
                    //time to restock
                    currentTime = Time.time;
                    restockTime = Random.Range(MinRestockTime * 2, MaxRestockTime * 2 + 1);     //restock takes longer during a game
                    canHaveCandy = false;
                    candyBeingCollected = false;

                    //lights out!
                    GetComponent<SpriteRenderer>().sprite = houseLightsOff;
                    HouseManager.instance.housesWithCandy--;
                }*/
                
            }
        }
        else
        {
            candyBeingCollected = false;
        }
    }


}
