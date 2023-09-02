using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Nolimit_Tutiral_D : MonoBehaviour
{
    public Text TextBox;
    public Text NameBox;
    public AudioSource Beep;

    public Image Panel;//씬 전환용 설정

    public Image Tao;//채도 조절용 패널
    public Image Rea;
    public GameObject AlertSetting;//임시 알림창

    float time = 0f;
    float F_time = 1f;
    int checkNum = 1;

    string[] NameSet = //이름세트
        {
            "태오", "연구원" 
        };
    
    string[] DialogueSet = //대화세트
        {
            "  ",
            "테오, 새로운 임무가 배정되었어.", //0
            "그래? 이번엔 무슨 일이려나.", //1
            "이때까지 네가 하던것과 크게 차이는 없어.", "네가 그동안 처치해온 변이체들에 대해 좀 더 많은 정보를 수집하기 위한 전투임무야.",
            "서포트 로보 1호가 녹화한 영상은 물론이고 네 슈트에 묻은 변이체의 혈액등이 귀중한 분석자료로 쓰이고 있어.", "그러니 많은 전투를 통해 정보수집에 도움을 주기를 바라.",//2-5
            "알았어! 그러니까 다 때려잡고 오기만 하면 되는거지?",//6
            "너는 진짜...","솔직히 네 목숨걸고 하는 임무라 마음에 들지는 않지만... 그래도 최소한의 안전장치는 마련해뒀어.",
            "네 생체 신호가 일정수준 이상으로 떨어지면 강제 귀환 시킬거야.",
            "몸에 이상이 있는게 아니더라도 귀환하고 싶을때에는 언제든지 1호를 통해 귀환할 수 있어.",//7-10
            "알았어. 걱정하지마, 내가 누군데!",//11
            "...됐어. 조심해서 갔다오기나 해."//12

        };
    string Dialogue;
    int setNum = 0; //DialogueSet 인덱스 관리

    void Awake()
    {
        Panel.gameObject.SetActive(true);
        StartCoroutine(FadeOut());
        Invoke("Start", 1f);
    }

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
                    case 2:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[0]; break;
                    case 3:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[1]; break;
                    case 7:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[0]; break;
                    case 8:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[1]; break;
                    case 12:
                        Tao.gameObject.SetActive(false); Rea.gameObject.SetActive(true); NameBox.text = NameSet[0]; break;
                    case 13:
                        Tao.gameObject.SetActive(true); Rea.gameObject.SetActive(false); NameBox.text = NameSet[1]; break;
                }
            }
            else
            {
                StartCoroutine(FadeFlow());
                Invoke("Alert", 1f);
            }
            
        }
    }

    void Alert()
    {
        AlertSetting.gameObject.SetActive(true);
    }

    IEnumerator Typing(string Talk)
    {
        checkNum = 0;
        TextBox.text = null;
        for(int i = 0; i < Talk.Length; i++)
        {
            TextBox.text += Talk[i];
            if(i%2==0) Beep.Play();

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

    public void TD_SceneChange()
    {
        SceneManager.LoadScene("Main");//컨텐츠 완성될때까지 막아둠 
        //SceneManager.LoadScene("Infinite Monster");
    }
}
