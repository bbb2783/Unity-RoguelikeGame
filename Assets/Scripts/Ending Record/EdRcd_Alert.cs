using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EdRcd_Alert : MonoBehaviour
{
    public void TD_SceneChange()
    {
        SceneManager.LoadScene("Main");//컨텐츠 완성될때까지 막아둠 
        //SceneManager.LoadScene("Infinite Monster");
    }
}
