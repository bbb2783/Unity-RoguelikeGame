using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFade : MonoBehaviour
{
    public Image Panel;//씬 전환용 설정

    float time = 0f;
    float F_time = 1f;
    
    void Awake()
    {
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeOut()
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
}
