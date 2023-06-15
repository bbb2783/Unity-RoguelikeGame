using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class F_D_01 : MonoBehaviour
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
    int checkNum = 1;

    string[] NameSet = //이름세트
        {
            "TO-01", "태오", "연구원" 
        };
    
    string[] DialogueSet = //대화세트
        {
            "휴~ 이거 만만치가 않은데.", "탐사대 태오, 도착 보고 합니다!", //0-1
            "확인했습니다. 거긴 어때?",//2
            "여기? ",
            "...와",
            "이게... 진짜 식물이라는거구나, 싶네...",
            "윽,그런데 여기 꼴이 장난아닌걸.",//3-6
            "지금 네가 있는 시대는 자원전쟁이 한창일때니까.", 
            "전쟁으로 개발이 중단되고 엉망이 된 상태라 장애물들을 조심해야해.",//7-8
            "알았어. 그럼 어디…",//9
            "비상. 비상. 미확인 개체 다수 접근중.",
            "전투모드로 전환합니다.",//10-11
            "뭐야? 벌써?",//12
            "이런… 준비해 태오! 변이체들이 몰려오고 있어!"//13

        };
    string Dialogue;
    int setNum = 0; //DialogueSet 인덱스 관리

    void Start()
    {
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
                    case 1:
                        StartCoroutine(FadeOut()); NameBox.text = NameSet[1]; break;
                    case 2:
                        Tao.gameObject.SetActive(true); NameBox.text = NameSet[2]; break;
                    case 3:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 7:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[2]; break;
                    case 9:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 10:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); ReaM.gameObject.SetActive(false); NameBox.text = NameSet[0]; break;
                    case 12:
                        Tao.gameObject.SetActive(false); TO01.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 13:
                        Tao.gameObject.SetActive(true); ReaM.gameObject.SetActive(true);NameBox.text = NameSet[1]; break;
                }
            }
            else
            {
                StartCoroutine(FadeFlow());//화면 암전 후 씬 전환
                Invoke("TD_SceneChange", 1f);
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

    IEnumerator FadeOut()
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

    public void TD_SceneChange()
    {
        SceneManager.LoadScene("MonsterScene");
    }
}
