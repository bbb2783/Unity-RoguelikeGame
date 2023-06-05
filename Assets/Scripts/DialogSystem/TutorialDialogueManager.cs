using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDialogueManager : MonoBehaviour
{
    public Text TextBox;
    public Text NameBox;
    
    public Image Panel;//씬 전환용 설정
    float time = 0f;
    float F_time = 1f;

    string[] DialogueSet = //대화세트
        {
            "안녕하세요", "저는 태오입니다", "만나서 반가워요", "저희는 식물을 구해요"
        };
    string Dialogue;
    int setNum = 0; //DialogueSet 인덱스 관리

    void Start()
    {
        NameBox.text = "태오";
        
        Dialogue = DialogueSet[setNum];
        StartCoroutine(Typing(Dialogue));
    }

    void Update()
    {   
        if(setNum>=DialogueSet.Length) return;//배열 길이 이상으로 클릭 할 수 없게

        if (Input.GetMouseButtonDown(0))
        {
            setNum += 1;
            if(setNum<DialogueSet.Length)
            {
                Dialogue = DialogueSet[setNum];
                StartCoroutine(Typing(Dialogue));
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
        TextBox.text = null;
        for(int i = 0; i < Talk.Length; i++)
        {
            TextBox.text += Talk[i];

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
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

    public void TD_SceneChange()
    {
        SceneManager.LoadScene("MainLab");
    }
}
