using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public bool isActive;
    //public float magicVx, magicVy = 0;

    private void Start()
    {
        isActive = true;
    }

    private void Update()
    {
        //the magic lasts until it hits a house or goes off screen. So there's no action timer check.
        //However, cooldown does not take effect until magic disappears.
       
        transform.position = new Vector3(transform.position.x + Witch.instance.magicVx * Time.deltaTime, transform.position.y + Witch.instance.magicVy * Time.deltaTime, transform.position.z);

        //check if offscreen.
        Vector3 screenPos = Camera.main.WorldToViewportPoint(GameManager.instance.transform.position);
        if (transform.position.x > screenPos.x * GameManager.instance.ScreenBoundaryX || transform.position.x < screenPos.x * -GameManager.instance.ScreenBoundaryX
            || transform.position.y > screenPos.y * GameManager.instance.ScreenBoundaryY || transform.position.y < screenPos.y * -GameManager.instance.ScreenBoundaryY)
        {
            Debug.Log("Magic offscreen at " + transform.position);
            Destroy(gameObject);
            Witch.instance.magicVx = 0;
            Witch.instance.magicVy = 0;

            //cooldown activation
            Witch.instance.currentTime = Time.time;
            Witch.instance.isTrickActive = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            //Debug.Log("House hit");
            Destroy(gameObject);
            Witch.instance.magicVx = 0;
            Witch.instance.magicVy = 0;

            //cooldown activation
            Witch.instance.currentTime = Time.time;
            Witch.instance.isTrickActive = false;
        }
    }
}
