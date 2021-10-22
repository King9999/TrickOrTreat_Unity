using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* A house can hold candy for players to pick up. Occasionally they may have more candy than the usual maximum capacity. A house can only be approached
 when its lights are on. */
public class House : MonoBehaviour
{
    public int candyAmount;             //random amount from 1 to 10, with a small chance to be 20.
    public float restockTime;           //time in seconds that must pass before house has candy. This value is randomized for each house.
    public float currentTime;           //gets timestamp of current time to enable cooldown for restocking candy
    public float candyPickupCurrentTime;
    public bool candyAmountIsHidden;    //if true, players will not know how much candy a house has until they approach
    public bool canHaveCandy;           //must be true in order to stock candy
    public bool candyBeingCollected;
    Costume player;                     //reference to the player collecting candy

    public Sprite houseLightsOff;
    public Sprite houseLightsOn;            //this sprite is used when a house has candy available
    public GameObject candyPickupPrefab;   //displays how much candy a player is taking from a house.
    [HideInInspector] public List<GameObject> candyPickupList;
    public GameObject trickOrTreatPrefab;   //sprite that appears when player approaches house.
    [HideInInspector] public List<GameObject> trickOrTreatList;
    public List<float> bubbleTimerList;               //controls when the trick or treat bubbles are destroyed
    bool isOnTrigger;                       //prevents trick or treat bubble from appearing more than once
    int candyTaken;                     //value varies depending on amount of candy remaining in house. This is really only for the princess.

    //consts
    public float MaxCandyChance { get; } = 0.04f;  //4% chance
    public float HiddenCandyChance { get; } = 0.1f; //10% chance. If sucessful, the amount of candy a house has is hidden until player approaches house
    public int MaxCandyAmount { get; } = 10;
    public int MinRestockTime { get; } = 5;
    public int MaxRestockTime { get; } = 10;
    public float CandyPickupRate { get; } = 0.5f;   //time in seconds that player acquires candy from a house.
    public float CandyZValue { get; } = -3;
    float bubbleActiveTime { get; } = 1f;           //time in seconds before the "trick or treat" bubble is removed


    private void Start()
    {
        restockTime = Random.Range(MinRestockTime, MaxRestockTime + 1);
        currentTime = 0;
        canHaveCandy = false;
        candyBeingCollected = false;
        candyPickupList = new List<GameObject>();
        trickOrTreatList = new List<GameObject>();
        bubbleTimerList = new List<float>();
        isOnTrigger = false;
    }

    //fill a house with candy.
    public void StockUp()
    {
        float randNum = Random.Range(0f, 1f);

        //check if house will have max candy
        if (randNum <= MaxCandyChance)
        {
            candyAmount = MaxCandyAmount * 2;
        }
        else
        {
            candyAmount = Random.Range(1, MaxCandyAmount + 1);
        }

        //is the candy amount hidden?
        randNum = Random.Range(0f, 1f);
        candyAmountIsHidden = randNum <= HiddenCandyChance;

        //turn the lights on
        GetComponent<SpriteRenderer>().sprite = houseLightsOn;

    }

    public bool HasCandy()
    {
        return candyAmount > 0;
    }

    private void Update()
    {
        if (candyBeingCollected && Time.time > candyPickupCurrentTime + CandyPickupRate)
        {
            
            if (candyAmount < player.candyTaken)           //need to do this check for the princess
            {
                candyTaken = candyAmount;
            }
            else
            {
                candyTaken = player.candyTaken;              
            }

            //remove candy from house
            player.candyAmount += candyTaken;
            candyAmount -= candyTaken;

            GameObject candyLabel = Instantiate(candyPickupPrefab, new Vector3(player.transform.position.x, player.transform.position.y, CandyZValue), Quaternion.identity);
            candyLabel.GetComponentInChildren<TextMeshProUGUI>().text = "+" + candyTaken;    //text mesh is a child component in the prefab
            candyPickupList.Add(candyLabel);

            candyPickupCurrentTime = Time.time;

            if (!HasCandy())
            {
                //time to restock
                currentTime = Time.time;
                restockTime = Random.Range(MinRestockTime * 2, MaxRestockTime * 2 + 1);     //restock takes longer during a game
                canHaveCandy = false;
                candyBeingCollected = false;

                //lights out!
                GetComponent<SpriteRenderer>().sprite = houseLightsOff;
                HouseManager.instance.housesWithCandy--;
            }
        }

        //coroutine management     
        StartCoroutine(PickupCandy());
        StartCoroutine(ManageTrickOrTreatBubbles());
    }

   
    private void OnTriggerStay2D(Collider2D collision)  //this method allows for continuous execution of the code every frame as long as there's a collision
    {
        if (collision.CompareTag("Player"))
        {
            if (HasCandy())
            {
                
                Debug.Log("Collecting Candy");
                //player starts collecting candy if the house has any available
                candyAmountIsHidden = false;
                candyBeingCollected = true;
                player = collision.GetComponent<Costume>();     //need reference to the player currently at the house

                //player yells "trick or treat"
                if (!isOnTrigger)
                {
                    isOnTrigger = true;
                    GameObject bubble = Instantiate(trickOrTreatPrefab, new Vector3(player.transform.position.x - 0.5f, player.transform.position.y + 0.5f, trickOrTreatPrefab.transform.position.z),
                        Quaternion.identity);
                    trickOrTreatList.Add(bubble);
                    bubbleTimerList.Add(Time.time);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            candyBeingCollected = false;
            isOnTrigger = false;
        }
    }

    #region Coroutines
    IEnumerator PickupCandy()
    {
        for (int i = 0; i < candyPickupList.Count; i++)
        {
            //player gets feedback when they collect candy from a house. The feedback scrolls and gradually disappears
            SpriteRenderer sr = candyPickupList[i].GetComponent<SpriteRenderer>();
            float scrollSpeed = 0.02f;
            while (sr != null && sr.color.a > 0)
            {
                candyPickupList[i].transform.position = new Vector3(candyPickupList[i].transform.position.x, candyPickupList[i].transform.position.y + scrollSpeed / 2 * Time.deltaTime,
                    candyPickupList[i].transform.position.z);
                candyPickupList[i].GetComponentInChildren<TextMeshProUGUI>().alpha -= scrollSpeed * Time.deltaTime;
                sr.color = new Color(1, 1, 1, sr.color.a - scrollSpeed * Time.deltaTime);
                candyPickupList[i].GetComponent<SpriteRenderer>().color = sr.color;

                yield return null;
            }
            
        }

        //cleanup
        for (int i = 0; i < candyPickupList.Count; i++)
        {
            Destroy(candyPickupList[i]);
        }
        if (candyPickupList.Capacity > 0)
        {
            candyPickupList.Clear();
            candyPickupList.Capacity = 0;
        }

    }

    IEnumerator ManageTrickOrTreatBubbles()
    {
        for (int i = 0; i < bubbleTimerList.Count; i++)
        {
            if (Time.time > bubbleTimerList[i] + bubbleActiveTime)
            {
                //remove corresponding bubble
                Destroy(trickOrTreatList[i]);
            }
            yield return null;
        }

        //clean up
        /*if (trickOrTreatList.Capacity > 0)
        {
            trickOrTreatList.Clear();
            trickOrTreatList.Capacity = 0;
        }*/
        /*if (bubbleTimerList.Capacity > 0)
        {
            bubbleTimerList.Clear();
            bubbleTimerList.Capacity = 0;
        }*/
    }
    #endregion
}
