using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMission : MonoBehaviour
{
    public void TTM_SceneChange()
    {
        SceneManager.LoadScene("MonsterScene");
    }
}
