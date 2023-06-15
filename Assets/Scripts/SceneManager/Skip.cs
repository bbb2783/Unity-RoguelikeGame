using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skip : MonoBehaviour
{
    public int SceneNum;
    public Image Panel;

    float time = 0f;
    float F_time = 1f;
    
    public void TTM_SceneChange()
    {
        StartCoroutine(FadeFlow());
        Invoke("SceneChange", 2f);
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
        switch(SceneNum)
        {
            case 1:
                SceneManager.LoadScene("D_forest_mid"); return;
            case 2:
                SceneManager.LoadScene("D_river_mid"); return;
            case 3:
                SceneManager.LoadScene("D_city_mid"); return;
        }
    }
}
