using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* This script controls all of the houses. It will monitor which houses have candy, as well as how many. */
public class HouseManager : MonoBehaviour
{
    public House[] houses;
    public TextMeshProUGUI[] candyUI;     //displays candy stock

    //consts
    public int TotalHousesWithCandy { get; } = 5;       //the maximum amount of houses that can have candy.
    public int TotalHouses { get; } = 10;

    // Start is called before the first frame update
    void Start()
    {
        //houses = new House[TotalHouses];
        //decide which houses have candy

        //house setup
        int housesWithCandy = 0;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        //house update
        for(int i = 0; i < TotalHouses; i++)
        {
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
            else
            {
                candyUI[i].text = "";
            }

        }


    }
}
