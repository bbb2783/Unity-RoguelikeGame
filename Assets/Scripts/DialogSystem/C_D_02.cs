using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C_D_02 : MonoBehaviour
{
    public Text TextBox;
    public Text NameBox;
    
    public Image NarPanel;//연출용 패널
    public Image Panel;//씬 전환용 설정

    public Image Tao;//채도 조절용 패널
    public Image Rea;
    public Image ReaM;//연구원 기본 일러스트
    public Image TO01;

    float time = 0f;
    float F_time = 1f;
    int checkNum = 0;

    string[] NameSet = //이름세트
        {
            "TO-01", "태오", "연구원" 
        };
    
    string[] DialogueSet = //대화세트
        {
            "헉...헉... 쟤들 왜저렇게 빨라!",//0
            "마지막까지 상상을 초월하네. 다른 곳과는 변이가 다른 방식으로 일어났나봐.",
            "연구해볼 가치가 있겠어.",//1-2
            "내 걱정도 해줘! 나 아파~!",//3
            "생체 체크. 혈압 정상. 오른쪽 어깨 타박상. 그 외 정상. 체력 보존을 위해 10분간 휴식을 권장합니다.",//4
            "그… 그래…",//5
            "...", 
            "…너도 수고 많았어. 얼른 샘플 채집하고 돌아와.",//6-7
            "...!!",
            "그래! 1호! 빨리 가자!"//8-9
        };
    string Dialogue;
    int setNum = 0; //DialogueSet 인덱스 관리

    void Awake()
    {
        Panel.gameObject.SetActive(true);
        StartCoroutine(All_FadeIn());
        Invoke("D_Start", 1f);
    }
    
    void D_Start()
    {
        checkNum = 1;
        NameBox.text = NameSet[1];
        
        Dialogue = DialogueSet[setNum];
        StartCoroutine(Typing(Dialogue));
    }

    void Update()
    {   
        if(setNum>=DialogueSet.Length) return;//배열 길이 이상으로 클릭 할 수 없게

        if (Input.GetMouseButtonDown(0) && checkNum==1)
        {
            setNum += 1;
            if(setNum<DialogueSet.Length)
            {
                Dialogue = DialogueSet[setNum];
                StartCoroutine(Typing(Dialogue));
                
                switch(setNum)
                {
                    case 1://연구원
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[2]; break;
                    case 3://태오
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 4://1호
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); ReaM.gameObject.SetActive(false); NameBox.text = NameSet[0]; break;
                    case 5://태오
                        Tao.gameObject.SetActive(false); TO01.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 6://연구원
                        Tao.gameObject.SetActive(true); ReaM.gameObject.SetActive(true); NameBox.text = NameSet[2]; break;
                    case 8://태오
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    
                }
            }
            else
            {
                StartCoroutine(All_FadeOut());//화면 암전 후 씬 전환
                Invoke("SceneChange", 1f);
            }
            
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

    IEnumerator All_FadeOut()//화면전체 페이드 인
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
    IEnumerator All_FadeIn()//화면전체 페이드 아웃
    {
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1,0,time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
        Panel.gameObject.SetActive(false);
    }

    IEnumerator Sub_FadeIn()//화면 일부 페이드아웃
    {
        time = 0f;
        Color alpha = NarPanel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1,0,time);
            NarPanel.color = alpha;
            yield return null;
        }
        yield return null;
        NarPanel.gameObject.SetActive(false);
    }
    IEnumerator Sub_FadeOut()//화면 일부 페이드인
    {
        NarPanel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = NarPanel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0,1,time);
            NarPanel.color = alpha;
            yield return null;
        }
        yield return null;
    }

    void SceneChange()
    {
        SceneManager.LoadScene("City01_2");
    }
}
