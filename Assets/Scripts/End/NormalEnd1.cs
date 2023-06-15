using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalEnd1 : MonoBehaviour
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
            "...임무 완료! 다녀왔어!",
            "그는 훌륭히 임무를 마치고 제자리로 돌아갔습니다",
            "그의 탐사 결과를 토대로 더 많은 탐사대원들이 식물 복원을 위해",
            "과거로 파견되기 시작하였습니다.",
            "NORMAL END - 시간을 넘나든 탐사대원",
            "메인 화면으로 돌아가시겠습니까?",
        };

    string Dialogue;
    int setNum = 0; //DialogueSet 인덱스 관리

    void Start()
    {
        NameBox.text = "TO-01";
        Dialogue = "탐사대 태오. 임무완료. 2123년 4월 8일 6시 12분으로 전송합니다.";
        StartCoroutine(Typing(Dialogue));
    }

    void Update()
    {   
        if(setNum>=DialogueSet.Length) return;//배열 길이 이상으로 클릭 할 수 없게
        for(int i = 0; i<6; i++)
        {
            if(Input.GetMouseButtonDown(0) && checkNum==1)
            {
                NameBox.text = "";
                Dialogue = DialogueSet[setNum];
                StartCoroutine(Typing(Dialogue));
                setNum += 1;
            }
        }
        if(setNum == 6) Invoke("Back_SceneChange", 1f);
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
