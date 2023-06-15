using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BadEnd1 : MonoBehaviour
{
    public Text TextBox;
    public Text NameBox;
    public GameObject BackMain;
    
    public Image NarPanel;//나레이션용 패널
    public Image Panel;//씬 전환용 설정

    float time = 0f;
    float F_time = 1f;
    int checkNum = 0;
    
    string[] DialogueSet = //대화세트
        {
            "생체반응 없음... 탐사자 사망. 임무실패",
            "BAD END - 희생된 첫번째 탐사대원",
            "메인 화면으로 돌아가시겠습니까?",
        };

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
        if(setNum>=DialogueSet.Length) return;//배열 길이 이상으로 클릭 할 수 없게
        for(int i = 0; i<3; i++)
        {
            if(Input.GetMouseButtonDown(0) && checkNum==1)
            {
                NameBox.text = "";
                Dialogue = DialogueSet[setNum];
                StartCoroutine(Typing(Dialogue));
                setNum += 1;
            }
        }
        if(setNum == 3) Invoke("Back_SceneChange", 1f);
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

    public void Back_SceneChange()
    {
        BackMain.SetActive(true);
    }
}
