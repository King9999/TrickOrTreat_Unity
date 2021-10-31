using UnityEngine;
using TMPro;

//Used to display the winner. Also checks for input from a player to go to costume select screen.
public class Results : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI promptText;

    // Start is called before the first frame update
    void Start()
    {
        //screen is hidden by default and only appears when time is up.
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.instance.timer.TimeUp())
        {
            //gameObject.SetActive(true);
            
            //display winner and candy amounts for each player in order from highest to lowest.
        }
        
    }
}
