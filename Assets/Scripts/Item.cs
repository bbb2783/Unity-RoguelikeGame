using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    [SerializeField] Text pickupText;
    bool isPickUp;

    void Start()
    {
        pickupText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPickUp && Input.GetKeyDown(KeyCode.Space))
        {
            Pickup();
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Player"))
        {
            pickupText.gameObject.SetActive(true);
            isPickUp = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Player"))
        {
            pickupText.gameObject.SetActive(false);
            isPickUp = false;
        }
    }
    
    void Pickup()
    {
        Destroy(gameObject);
    }
}
