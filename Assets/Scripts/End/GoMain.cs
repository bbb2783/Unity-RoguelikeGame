using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoMain : MonoBehaviour
{
    public Image Panel;//씬 전환용 설정

    float time = 0f;
    float F_time = 1f;
    
    public void back_button()//메인화면으로 돌아가기 버튼
    {
        StartCoroutine(FadeFlow());
        Invoke("Back_MainScene", 1f);
    }
    
    public void Back_MainScene()
    {
        SceneManager.LoadScene("Main");
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
}
