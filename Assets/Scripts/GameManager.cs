using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //consts
    public float ScreenBoundaryX { get; } = 10.24f;                 //calculated by dividing screen width by PPU (100).
    public float ScreenBoundaryY { get; } = 7.68f;                 //calculated by dividing screen height by PPU (100). The height is actually higher but a portion of it is for UI.

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);    //Only want one instance of game manager
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
