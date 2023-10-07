using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class but : MonoBehaviour
{
    public GameObject Panel;

    public void ShowPanal()
    {
        Panel.SetActive(true);
    }

    public void DisPanal()
    {
        Panel.SetActive(false);
    }

    
}
