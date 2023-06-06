using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BadEnd1 : MonoBehaviour
{
    public Text TextBox;
    public Text NameBox;
    public Text EndingBox;
    
    public Image NarPanel;//나레이션용 패널
    public Image Panel;//씬 전환용 설정

    float time = 0f;
    float F_time = 1f;
    int checkNum = 0;

    string Dialogue;
    int setNum = 0; //DialogueSet 인덱스 관리

    void Start()
    {
        NameBox.text = "연구원";
        Dialogue = "태오... 안돼, 태오!!";
        StartCoroutine(Typing(Dialogue));
    }

    void Update()
    {   
        if(checkNum == 1)
        {
            StartCoroutine(FadeFlow());
            Invoke("EndingPrint", 2f);
        }
    }

    IEnumerator Typing(string Talk)
    {
        checkNum = 0;
        TextBox.text = null;
        for(int i = 0; i < Talk.Length; i++)
        {
            TextBox.text += Talk[i];

            yield return new WaitForSeconds(0.05f);
        }
        checkNum = 1;
    }

    IEnumerator EndTyping(string Talk)
    {
        checkNum = 0;
        EndingBox.text = null;
        for(int i = 0; i < Talk.Length; i++)
        {
            EndingBox.text += Talk[i];

            yield return new WaitForSeconds(0.05f);
        }
        checkNum = 1;
    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0,1,time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
    }

    public void EndingPrint()
    {
        Dialogue = "생체반응 없음... \n 탐사자 사망. 임무실패";
        StartCoroutine(EndTyping(Dialogue));

        if(checkNum == 2)
        {
            Dialogue = "BAD END - 희생자";
            StartCoroutine(EndTyping(Dialogue));
            Invoke("Back_SceneChange", 3f);
        }
        
    }

    public void Back_SceneChange()
    {
        SceneManager.LoadScene("Main");
    }
}
