using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* This script controls all of the houses. It will monitor which houses have candy, as well as how many. */
public class HouseManager : MonoBehaviour
{
    public House[] houses;
    public TextMeshProUGUI[] candyUI;       //displays candy stock
    public int housesWithCandy;                    //used to check when it's time for houses to stock up

    //consts
    public int TotalHousesWithCandy { get; } = 5;       //the maximum amount of houses that can have candy.
    public int TotalHouses { get; } = 10;

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

    // Start is called before the first frame update
    void Start()
    {
        //houses = new House[TotalHouses];
        //decide which houses have candy

        //house setup
        /*housesWithCandy = 0;
        for (int i = 0; i < TotalHouses; i++)
        {
            //candy counter setup
            //Vector3 worldPos = Camera.main.ScreenToWorldPoint(houses[i].transform.position);
            //candyUI[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(worldPos.x, worldPos.y + 20);

            //does this house have candy?
            if (housesWithCandy < TotalHousesWithCandy)
            {
                float randNum = Random.Range(0f, 1f);
                if (randNum <= 0.5f)
                {
                    houses[i].canHaveCandy = true;
                    housesWithCandy++;
                }
                else
                    houses[i].canHaveCandy = false;

            }
            else
            {
                houses[i].canHaveCandy = false;
            }
        }*/
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
                
                houses[i].StockUp(); 
                housesWithCandy++;              
            }
       // }
            //housesWithCandy = 0;
        //for (int i = 0; i < TotalHouses; i++)
        //{
            if (houses[i].HasCandy())
            {
                //housesWithCandy++;
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

        //check if any new houses can have candy and turn on lights.
        /*while (housesWithCandy < TotalHousesWithCandy)
        {
            for (int i = 0; i < TotalHouses; i++)
            {
                if (houses[i].canHaveCandy)
                {
                    StockUp();
                }
            }

        }*/

    }
}
