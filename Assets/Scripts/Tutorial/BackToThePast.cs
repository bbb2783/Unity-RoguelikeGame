using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToThePast : MonoBehaviour
{
    public GameObject StartMission;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("FieldObject")) return; //머신과 충돌한게 아니면 리턴
        StartMission.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("FieldObject")) return;
        
        StartMission.SetActive(false);
    }
}
