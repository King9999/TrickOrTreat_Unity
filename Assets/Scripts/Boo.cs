using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the Ghost's trick. It hits any player in range, and will cause them to drop twice the amount of candy. */

public class Boo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //NOTE: May need to temporarily disable the hitbox on the player using the trick so they don't get hit by their own attack.
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit");
        }
    }
}
