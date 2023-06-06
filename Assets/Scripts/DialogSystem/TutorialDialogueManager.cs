using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDialogueManager : MonoBehaviour
{
    public Text TextBox;
    public Text NameBox;
    
    public Image NarPanel;//나레이션용 패널
    public Image Panel;//씬 전환용 설정

    public Image Tao;//채도 조절용 패널
    public Image Rea;

    float time = 0f;
    float F_time = 1f;
    int checkNum = 1;

    string[] NameSet = //이름세트
        {
            "TV 속 앵커", "태오", "연구원" 
        };
    
    string[] DialogueSet = //대화세트
        {
            "푸른 지구별이라는 말이 무색하게 지구는 서서히...", "그리고 빠르게 병들어 갔습니다.", "전쟁으로 마지막 종자보관소까지 파괴되면서...",//0-2
            "...",//3
            "뭐해? 이제 시간 다 됐어. 시작하자.",//4-8
            "자, 출발하기 전에 마지막으로 설명할게.", "너는 이 시간 역행 장치에 탑승하면 2053년으로 전송될거야.","거기서 식물을 채집해서 샘플을 로봇을 통해 전송하면 돼.",
            "한 구역에서 전송 할 수 있는 최대 개수는 5개야. 명심해.",
            "알았어. 다른건?",//9
            "다른건… 변이체에 대한거야.","바이러스 때문에 생겨난 변이체들이 주변을 초토화 하던 시기야. 전송이 되자마자 몰려들거야.",//10-14
            "미리 방어선을 쳐 둘 수 있으면 좋겠지만…", "네가 전송될 포인트를 완벽히 알 방법이 없어.", "적어도 5분정도는 방어선을 칠 때까지 전투를 해야해.",
            "오케이~ 그동안 훈련을 얼마나 열심히 받았는데.","걱정하지마. 샘플 그냥 아주 팍팍 보낼게!",//15-16
            "5개밖에 못보낸다니까. 까불지말고. ...조심히 갔다와.",//17
            "어차피 네가 열심히 서포트해줄거잖아? 둘다 힘내보자고."//18

        };
    string Dialogue;
    int setNum = 0; //DialogueSet 인덱스 관리

    void Start()
    {
        NameBox.text = NameSet[0];
        
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
                    case 3:
                        NameBox.text = NameSet[1]; break;
                    case 4:
                        StartCoroutine(FadeOut()); NameBox.text = NameSet[2]; break;
                    case 9:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 10:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[2]; break;
                    case 15:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                    case 17:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[2]; break;
                    case 18:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[1]; break;
                }
            }
            else
            {
                StartCoroutine(FadeFlow());
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
        //NarPanel.gameObject.SetActive(true);
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
        SceneManager.LoadScene("MainLab");
    }
}
