using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScene : MonoBehaviour
{
    public Image Panel;

    float time = 0f;
    float F_time = 2f;
    
    void Awake()
    {
        StartCoroutine(FadeOut());
        Invoke("NextScene", 2f);
    }
    
    void NextScene()
    {
        StartCoroutine(FadeFlow());
        Invoke("SceneChange", 2f);
    }

    IEnumerator FadeOut()
    {
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(2,0,time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
        Panel.gameObject.SetActive(false);
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

    void SceneChange()
    {
        SceneManager.LoadScene("Main"); return;
    }
}
