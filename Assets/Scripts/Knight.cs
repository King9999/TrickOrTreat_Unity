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
        cooldown = 1;
        dropAmount = 2;
        candyTaken = 1;
        moveSpeed = 1f;
        actionTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            Debug.Log("Collecting Candy");
        }

        if (collision.CompareTag("Trick"))
        {
            Debug.Log("Player hit");
        }
    }*/
}

