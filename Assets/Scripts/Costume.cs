using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the base class for all of the costumes in the game */

public class Costume : MonoBehaviour
{
    public string name;
    public short candyAmount;
    public float cooldown;      //time in seconds before trick is recharged
    public short dropAmount;    //how much candy the player drops when hit
    public short candyTaken;    //how much candy the player gets from a house per tick
    public float vx, vy;
    public float moveSpeed;     //scales vx and vy. Lower value = slower speed
    public float actionTimer;   //how long a trick is active for.
}
