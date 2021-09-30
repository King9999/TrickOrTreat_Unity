using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* The knight moves slowly, but their trick has the shortest cooldown, and they don't drop as much candy as the other costumes. */
public class Knight : Costume
{
    public GameObject swordPrefab;
    GameObject sword;
    float swordOffset = 0.5f;                   //used to position the sword away from player.

    // Start is called before the first frame update
    void Start()
    {
        name = "Knight";
        initCooldown = 1;
        dropAmount = 2;
        candyTaken = 1;
        moveSpeed = 1f;
        actionTimer = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        //player can't move while trick is active.
        if (isTrickActive)
        {
            vx = 0;
            vy = 0;
        }

        //check action timer
        if (isTrickActive && Time.time > currentTime + actionTimer)
        {
            isTrickActive = false;
            Destroy(sword);
        }
    }

    //Perform a sword attack in the direction the knight is facing.
    public override void UseTrick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (!isTrickActive && Time.time > currentTime + cooldown)
            {
                isTrickActive = true;
                cooldown = initCooldown;
                currentTime = Time.time;

                //sword attack is generated at different locations depending on direction.
                switch(direction)
                {
                    case Direction.Left:
                        sword = Instantiate(swordPrefab, new Vector3(transform.position.x - swordOffset, transform.position.y, PlayerZValue), Quaternion.identity);
                        break;

                    case Direction.Right:
                        sword = Instantiate(swordPrefab, new Vector3(transform.position.x + swordOffset, transform.position.y, PlayerZValue), Quaternion.identity);
                        sword.transform.localScale = new Vector3(sword.transform.localScale.x * -1, sword.transform.localScale.y, sword.transform.localScale.z);    //flips sprite in opposite direction
                        break;

                    case Direction.Up:
                        sword = Instantiate(swordPrefab, new Vector3(transform.position.x, transform.position.y + swordOffset, PlayerZValue), Quaternion.identity);
                        //sword.transform.Rotate(sword.transform.localRotation.x, sword.transform.localRotation.y, sword.transform.localRotation.z * -90);
                        sword.transform.Rotate(Vector3.forward * -90);
                        break;

                    case Direction.Down:
                        sword = Instantiate(swordPrefab, new Vector3(transform.position.x, transform.position.y - swordOffset, PlayerZValue), Quaternion.identity);
                        sword.transform.Rotate(Vector3.forward * 90);
                        break;

                     default:
                        break;
                }
            }
            //else
            //play a sound or show a graphic to indicate trick can't be used yet.

        }
    }
}

