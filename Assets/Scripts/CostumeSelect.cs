using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Player selects their costume here. A description of the costume and their trick can be found here also.
public class CostumeSelect : MonoBehaviour
{
    public TextMeshProUGUI costumeDescription;
    public TextMeshProUGUI trickData;
    string[] costumeDescriptions = new string[4];
    string[] trickDescriptions = new string[4];

    // Start is called before the first frame update
    void Start()
    {
        costumeDescriptions[0] = "The Ghost can scare any opponents in range, causing lots of candy to drop!";
        costumeDescriptions[1] = "The slow-moving Knight won't get knocked back much by tricks, and their candy is a bit more secure.";
        costumeDescriptions[2] = "The Ghost can scare any opponents in range, causing lots of candy to drop!";
        costumeDescriptions[3] = "The Ghost can scare any opponents in range, causing lots of candy to drop!";
        costumeDescription.text = costumeDescriptions[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
