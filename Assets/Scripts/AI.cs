using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script 
public class AI : Costume
{
    Vector3 targetPos;
    bool aiFoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aiFoundPlayer)
        {
            //moving to target position
            if (targetPos.x > transform.position.x)
            {
                vx = moveSpeed;
            }

            if (targetPos.x < transform.position.x)
            {
                vx = -moveSpeed;
            }

            if (targetPos.y > transform.position.y)
            {
                vy = moveSpeed;
            }

            if (targetPos.y < transform.position.y)
            {
                vy = -moveSpeed;
            }
        }
        else
        {
            vx = 0;
            vy = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //approach player
            targetPos = collision.transform.position;
            aiFoundPlayer = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            aiFoundPlayer = false;
        }
    }
}
