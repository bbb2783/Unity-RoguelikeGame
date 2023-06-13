using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickingNPC : MonoBehaviour
{
    public GameObject MoveScene;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("npc")) return; //머신과 충돌한게 아니면 리턴
        MoveScene.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("npc")) return;
        
        MoveScene.SetActive(false);
    }
}
