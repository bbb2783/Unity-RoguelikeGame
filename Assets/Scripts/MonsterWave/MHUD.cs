using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MHUD : MonoBehaviour
{
    public enum InfoType {Time, PlayerHealth}
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type) 
        {
            case InfoType.Time:

                break;

            case InfoType.PlayerHealth:

                break;
        }
    }
}
