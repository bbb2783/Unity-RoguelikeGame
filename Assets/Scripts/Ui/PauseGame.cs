using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    bool IsPause;
    public Image PauseUI;
    
    void Start()
    {
        IsPause = false;
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPause == false) //일시정지 활성화
            {
                Time.timeScale = 0;
                IsPause = true;

                PauseUI.gameObject.SetActive(true); //설정창 활성화
                
                return;
            }

            if(IsPause == true) //일시정지 해제
            {
                Time.timeScale = 1;
                IsPause = false;

                PauseUI.gameObject.SetActive(false); //설정창 비활성화
                
                return;
            }
        }
    }
}
