using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The witch can shoot magic, allowing her to hit other players from a distance. The magic attack will go through other players
 * until it hits a house or goes off screen. Has a high cooldown, plus the cooldown doesn't activate while the beam is on screen. */
public class Witch : Costume
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Witch";
        candyAmount = 0;
        cooldown = 8;
        dropAmount = 5;
        actionTimer = 1;        //lasts until beam disappears
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
