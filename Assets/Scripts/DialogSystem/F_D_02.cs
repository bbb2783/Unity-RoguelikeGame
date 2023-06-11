using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class F_D_02 : MonoBehaviour
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
            "로봇", "태오", "연구원" 
        };
    
    string[] DialogueSet = //대화세트
        {
            "헉...헉...", "이게 대체 무슨...", //0-1
            "...",
            "우리가 가지고 있는 정보랑 너무 달라.",
            "이 당시로부터 전해진 정보가 적어서 조금 차이가 있을거라고 생각은 했지만 이건 예상범위 밖인걸.",
            "일단 방어선을 구축했으니 식물 전송을 진행해줘. 나는 네 레이저건을 강화할 준비를 할게.",//2-5
            "알았어. 가자, 1호!", //6
            "명령 이행. 채집 모드로 전환합니다.",//7
        };
    string Dialogue;
    int setNum = 0; //DialogueSet 인덱스 관리

    void Awake()
    {
        Panel.gameObject.SetActive(true);
        StartCoroutine(All_FadeFlow2());
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
                    case 2:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[2]; break;
                    case 6:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[2]; break;
                    case 7:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); ReaM.gameObject.SetActive(false); NameBox.text = NameSet[0]; break;
                }
            }
            else
            {
                StartCoroutine(FadeFlow());//화면 암전 후 씬 전환
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

    IEnumerator FadeFlow()//화면전체 페이드 인
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
    IEnumerator All_FadeFlow2()//화면전체 페이드 아웃
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

    IEnumerator FadeOut()//화면 일부 페이드아웃
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
    IEnumerator Sub_FadeIn()//화면 일부 페이드인
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
        SceneManager.LoadScene("SampleScene");
    }
}
