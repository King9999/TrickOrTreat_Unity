using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The princess does not have a trick, but gets more candy from houses, and has higher move speed */
public class Princess : Costume
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Princess";
        candyAmount = 0;
        cooldown = 1;
        dropAmount = 5;
        candyTaken = 2;
        moveSpeed = 1.5f;
        actionTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
