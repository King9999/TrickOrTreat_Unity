using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The ghost can target multiple players at once, making any player in range drop their candy. Also, the ghost makes players drop
 * twice as much candy. The ghost's trick has the highest cooldown. */
public class Ghost : Costume
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Ghost";
        cooldown = 10;
        actionTimer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
