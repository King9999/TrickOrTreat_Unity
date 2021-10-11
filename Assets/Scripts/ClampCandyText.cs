using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This script is used to display text next to candy icon when picking up candy from a house.
public class ClampCandyText : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    // Start is called before the first frame update
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(transform.position);
        textUI.transform.position = new Vector3(namePos.x + 15, namePos.y, namePos.z);

    }
}
