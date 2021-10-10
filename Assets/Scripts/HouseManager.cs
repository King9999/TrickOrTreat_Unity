using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* This script controls all of the houses. It will monitor which houses have candy, as well as how many. */
public class HouseManager : MonoBehaviour
{
    public House[] houses;
    public TextMeshProUGUI[] candyUI;               //displays candy stock
    public int housesWithCandy;                    //used to check when it's time for houses to stock up

    //consts
    public int TotalHousesWithCandy { get; } = 5;       //the maximum amount of houses that can have candy.
    public int TotalHouses { get; } = 10;
    public float StockUpChance { get; } = 0.5f;

    public static HouseManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        //house update
        for (int i = 0; i < TotalHouses; i++)
        {
            if (Time.time > houses[i].currentTime + houses[i].restockTime)
                houses[i].canHaveCandy = true;

            if (houses[i].canHaveCandy && !houses[i].HasCandy() && housesWithCandy < TotalHousesWithCandy)
            {
                //Roll to see if this house gets candy
                float randNum = Random.Range(0f, 1f);
                if (randNum <= StockUpChance)
                {
                    houses[i].StockUp();
                    housesWithCandy++;
                }
            }
      
            if (houses[i].HasCandy())
            {
                if (houses[i].candyAmountIsHidden)
                {
                    candyUI[i].text = "??";
                }
                else
                {
                    candyUI[i].text = houses[i].candyAmount.ToString();
                }
            }
            else //no candy
            {
                candyUI[i].text = "";
            }

        }

    }
}
