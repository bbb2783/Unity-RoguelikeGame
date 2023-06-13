using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class R_D_01 : MonoBehaviour
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
            "전송된 샘플은 이상 없어.", "식물이라는 거... 생각보다 특이한 질감이네.", //0-1
            "감상이 그거야? 너답네 정말...",
            "아무튼 제대로 전송됐다니 한시름 놨는걸.",
            "안심하고 임무를 수행 할 수 있겠어.",//2-4
            "그래. 마침 네 레이저건을 강화할 준비도 끝났어. 1호, 수신모드.",//5
            "명령 이행. 수신모드로 전환합니다.", //6
            "태오, 레이저건을 1호에게 가져다 대.",//7
            "오케이, 준비됐어",//8
            "에너지를 주입합니다.", "...", "...완료.", "프로세스를 수정합니다.", "...", "...완료.", "발생한 오류는 없습니다.", "수신모드를 종료합니다.",//9-16
            "이제 건의 공격력이 두배정도 좋아졌을거야.", "...타이밍 좋게 몰려오는것 같네.", "준비해, 건을 강화했다곤 해도 전투는 쉽지 않을거야."//17-19
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
        NameBox.text = NameSet[2];
        
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
                    case 2://태오
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 5://연구원
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[2]; break;
                    case 6://1호
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); ReaM.gameObject.SetActive(false); NameBox.text = NameSet[0]; break;
                    case 7://연구원
                        Tao.gameObject.SetActive(true); ReaM.gameObject.SetActive(true); NameBox.text = NameSet[2]; break;
                    case 8://태오
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 9://1호
                        StartCoroutine(Sub_FadeOut()); Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); ReaM.gameObject.SetActive(false); NameBox.text = NameSet[0]; break;
                    case 17://연구원
                        StartCoroutine(Sub_FadeIn()); Tao.gameObject.SetActive(true); ReaM.gameObject.SetActive(true); NameBox.text = NameSet[2]; break;
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
        SceneManager.LoadScene("River01_1");
    }
}
