using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the base class for all of the costumes in the game */

public class Costume : MonoBehaviour
{
    public new string name;
    public short candyAmount = 0;
    public float cooldown;          //time in seconds before trick is recharged
    public short dropAmount = 5;    //how much candy the player drops when hit. 5 is the default.
    public short candyTaken = 1;        //how much candy the player gets from a house per tick
    public float vx, vy = 0;
    public float moveSpeed = 1;     //scales vx and vy. Lower value = slower speed
    public float actionTimer;       //how long a trick is active for.
    public bool isTrickActive = false;
    public float currentTime = 0;   //used to track when trick can be used again.

    //consts
    public const short MAX_CANDY = 999;
    public const short INIT_DROP_AMOUNT = 5;

    public void AddCandy(short amount)
    {
        if (candyAmount + amount <= MAX_CANDY)
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

    public virtual void UseTrick()  //to be overridden by other classes.
    {
        if (Time.time > currentTime + cooldown)
        {
            isTrickActive = true;
            currentTime = Time.time;
        }
        //else
            //play a sound or show a graphic to indicate trick can't be used yet.
    }
}
