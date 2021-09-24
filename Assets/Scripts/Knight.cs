using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The knight moves slowly, but their trick has the shortest cooldown, and they don't drop as much candy as the other costumes. */
public class Knight : Costume
{

    // Start is called before the first frame update
    void Start()
    {
        name = "Knight";
        candyAmount = 0;
        cooldown = 2;
        dropAmount = 2;
        candyTaken = 1;
        moveSpeed = 0.6f;
        actionTimer = 1;
        isTrickActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

