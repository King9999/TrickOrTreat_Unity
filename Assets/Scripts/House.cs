using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A house can hold candy for players to pick up. Occasionally they may have more candy than the usual maximum capacity. A house can only be approached
 when its lights are on. */
public class House : MonoBehaviour
{
    public int candyAmount;         //random amount from 1 to 10, with a small chance to be 20.
    public float restockTime;       //time in seconds that must pass before house has candy
    public float currentTime;

    public Sprite houseLightsOff;
    public Sprite houseLightsOn;

    //consts
    public float MaxCandyChance { get; } = 0.04f;  //4% chance
    public float HiddenCandyChance { get; } = 0.1f; //10% chance. If sucessful, the amount of candy a house has is hidden until player approaches house
    public int MaxCandyAmount { get; } = 10;

    //fill a house with candy.
    public void StockUp()
    {
        float randNum = Random.Range(0, 1);

        //check if house will have max candy
        if (randNum <= MaxCandyChance)
        {
            candyAmount = MaxCandyAmount * 2;
        }
        else
        {
            candyAmount = Random.Range(1, MaxCandyAmount + 1);
        }
    }

    public bool HasCandy()
    {
        return candyAmount > 0;
    }

    private void Start()
    {
        restockTime = 2;
        currentTime = 0;
    }
    private void Update()
    {
        if (!HasCandy() && Time.time > currentTime + restockTime)
        {
            StockUp();
            currentTime = Time.time;
        }

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
    }


}
