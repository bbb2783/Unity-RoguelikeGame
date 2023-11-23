using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    public Image Hp_icon;
    public Image STR_icon;
    
    /*float coolTime = 10.0f; //레일건 쿨타임
    float leftTime = 0.0f;
    bool isOver = true;*/

    //public static int ItemSet = 0;

    void Start()
    {
        
    }

    void Update()
    {
        /*if(leftTime > 0){ //레일건 쿨타임 돌리기
            leftTime -= Time.deltaTime;
            if(leftTime < 0){
                leftTime = 0;
                isOver = true;
            }
        }*/

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            //ItemSet = 1; 
            Debug.Log("회복템 사용");
            if(MGameManager.instance.playerHealth <= 150)
            {
                MGameManager.instance.playerHealth += 50;
            }
            else
            {
                MGameManager.instance.playerHealth = 200;
            }

            Hp_icon.gameObject.SetActive(true);
            Invoke("HideHpIcon",2.0f);
        }
    }

    private void HideHpIcon(){
        Hp_icon.gameObject.SetActive(false);
    }
}
