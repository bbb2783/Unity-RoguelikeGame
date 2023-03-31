using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1f;


    public KeyCode pickupKey; // 단축키 설정
    public float pickupRadius = 1f; // 픽업 반경 설정
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    private void Update()
    {
        // 픽업 단축키를 누르면 근처의 FieldItem 오브젝트를 획득
        if (Input.GetKeyDown(pickupKey))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("FieldItem"))
                {
                    FieldItem fieldItem = collider.GetComponent<FieldItem>();
                    if (fieldItem != null)
                    {
                        inventory.AddItem(fieldItem.GetItem());
                        fieldItem.DestroyItem();
                    }
                }
            }
        }
    }
}
