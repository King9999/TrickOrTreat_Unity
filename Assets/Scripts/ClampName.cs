using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This is used to display candy counter over the house.
public class ClampName : MonoBehaviour
{
    public TextMeshProUGUI candyUI;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 namePos = Camera.main.ScreenToWorldPoint(transform.position);
        //candyUI.transform.localPosition = namePos;
        Vector3 namePos = Camera.main.WorldToScreenPoint(transform.position);
        //candyUI.GetComponent<RectTransform>().position = namePos;
        candyUI.transform.position = new Vector3(namePos.x, namePos.y + 20, namePos.z);

    }

}
