using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI descriptionTxt;
    public TextMeshProUGUI atkTxt;
    public TextMeshProUGUI atkValueTxt;
    public Image itemImage;

    public void SetupTooltip(string name, string des, int atk, Sprite image)
    {
        nameTxt.text = name;
        descriptionTxt.text = des;

        if (atk == 0)
        {
            atkTxt.gameObject.SetActive(false);
            atkValueTxt.gameObject.SetActive(false);
        }
        else
        {
            atkTxt.gameObject.SetActive(true);
            atkValueTxt.gameObject.SetActive(true);
            atkValueTxt.text = atk.ToString();
        }

        // 이미지 설정
        if (image != null)
        {
            itemImage.sprite = image;
            itemImage.gameObject.SetActive(true);
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
    }
}
