using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* The witch can shoot magic, allowing her to hit other players from a distance. The magic attack will go through other players
 * until it hits a house or goes off screen. Has a high cooldown, plus the cooldown doesn't activate while the beam is on screen. */
public class Witch : Costume
{
    public GameObject magicPrefab;
    GameObject magic;
    Magic magicState;
    public float magicOffset = 0.4f;
    public float magicVx, magicVy = 0;             //used to move the magic shot
    public float magicSpeed;

    public static Witch instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        name = "Witch";
        candyAmount = 0;
        initCooldown = 8;
        //dropAmount = 5;
        //candyTaken = 1;
        moveSpeed = 1.3f;
        actionTimer = 1;        //lasts until beam disappears
        magicSpeed = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //the magic lasts until it hits a house or goes off screen. So there's no action timer check.
        //However, cooldown does not take effect until magic disappears.
        /*if (magicState.isActive && isTrickActive)
        {
            magic.transform.position = new Vector3(magic.transform.position.x + magicVx * Time.deltaTime, magic.transform.position.y + magicVy * Time.deltaTime, magic.transform.position.z);

            //check if offscreen.
            Vector3 screenPos = Camera.main.WorldToViewportPoint(GameManager.instance.transform.position);
            if (magic.transform.position.x > screenPos.x * GameManager.instance.ScreenBoundaryX || magic.transform.position.x < screenPos.x * -GameManager.instance.ScreenBoundaryX
                || magic.transform.position.y > screenPos.y * GameManager.instance.ScreenBoundaryY || magic.transform.position.y < screenPos.y * -GameManager.instance.ScreenBoundaryY)
            {
                Debug.Log("Magic offscreen at " + magic.transform.position);
                Destroy(magic);
                isTrickActive = false;
                magicVx = 0;
                magicVy = 0;

                //cooldown activates here
                
            }
        }*/


    }

    public override void UseTrick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && UI.instance.gameStarted)
        {
            if (!isTrickActive && Time.time > currentTime + cooldown)
            {
                isTrickActive = true;
                cooldown = initCooldown;
                currentTime = Time.time;

                //magic attack is generated at different locations depending on direction.
                switch (direction)
                {
                    case Direction.Left:
                        magic = Instantiate(magicPrefab, new Vector3(transform.position.x - magicOffset, transform.position.y, PlayerZValue - 1), Quaternion.identity);
                        magicVx = -magicSpeed;
                        break;

                    case Direction.Right:
                        magic = Instantiate(magicPrefab, new Vector3(transform.position.x + magicOffset, transform.position.y, PlayerZValue - 1), Quaternion.identity);
                        magicVx = magicSpeed;
                        break;

                    case Direction.Up:
                        magic = Instantiate(magicPrefab, new Vector3(transform.position.x, transform.position.y + magicOffset, PlayerZValue - 1), Quaternion.identity);
                        magicVy = magicSpeed;
                        break;

                    case Direction.Down:
                        magic = Instantiate(magicPrefab, new Vector3(transform.position.x, transform.position.y - magicOffset, PlayerZValue - 1), Quaternion.identity);
                        magicVy = -magicSpeed;
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
