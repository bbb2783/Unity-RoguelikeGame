using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandBG : MonoBehaviour
{
    public Image BG;
    public Sprite[] BGsprite;

    void Start()
    {
        int index = Random.Range(0,BGsprite.Length);
        Sprite select = BGsprite[index];
        BG.sprite = select;
    }
}
