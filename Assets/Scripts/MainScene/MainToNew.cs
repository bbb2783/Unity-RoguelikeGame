using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainToNew : MonoBehaviour
{
    public Image Panel;
    public GameObject particle;
    float time = 0f;
    float F_time = 1f;
    
    public void ChangeSceneBtn()//페이드 효과 진행(3초) 후 씬 전환 실행
    {
        StartCoroutine(FadeFlow());
        particle.gameObject.SetActive(false);
        Invoke("SelectScene", 3f);
    }


    IEnumerator FadeFlow()//페이드효과
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
    
    public void SelectScene()//버튼에 따라 다른 씬 전환
    {
        switch (this.gameObject.name)
        {
            case "NewGame":
                SceneManager.LoadScene("TutorialDialogue");
                break;
            case "Nolimit":
                SceneManager.LoadScene("Nolimit_Tutorial"); 
                break;
            case "ending record":
                SceneManager.LoadScene("Ending Record"); 
                break;
        }
        
    }
}
