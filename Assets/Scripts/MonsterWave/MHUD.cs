using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MHUD : MonoBehaviour
{
    public enum InfoType {Time, PlayerHealth, PlayerMana}
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
                float remainTime = MGameManager.instance.maxGameTime - MGameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}",min,sec);
                break;

            case InfoType.PlayerHealth://플레이어 체력관리
                float curHealth = MGameManager.instance.playerHealth;
                float maxHealth = MGameManager.instance.playerMaxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
            
            case InfoType.PlayerMana://플레이어 기력관리
                float curMana = MGameManager.instance.playerMana;
                float maxMana = MGameManager.instance.playerMaxMana;
                mySlider.value = curMana / maxMana;
                break;
        }
    }
}
