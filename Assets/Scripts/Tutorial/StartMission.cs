using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMission : MonoBehaviour
{
    public Image Panel;
    public AudioSource warp;

    float time = 0f;
    float F_time = 1f;
    
    public void TTM_SceneChange()
    {
        StartCoroutine(FadeFlow());
        warp.Play();
        Invoke("ML_SceneChange", 3f);
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

    public void ML_SceneChange()
    {
        SceneManager.LoadScene("D_forest_start");
    }
}
