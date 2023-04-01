using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1f;

    public KeyCode pickupKey; // 단축키 설정
    public float pickupRadius = 1f; // 픽업 반경 설정
    private Inventory inventory;

    [SerializeField] private Text pickupText;
    private bool isPickUp = false;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    private void Update()
    {
        // 플레이어 이동
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical);
        transform.Translate(direction * speed * Time.deltaTime);

        // 근처 아이템이 있을 경우
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
        bool hasFieldItem = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("FieldItem"))
            {
                hasFieldItem = true;
                isPickUp = true;
                pickupText.gameObject.SetActive(true);
                pickupText.text = "Press " + pickupKey.ToString() + " to pick up";
                break;
            }
        }

        // 아이템 획득 단축키를 누르면 근처의 FieldItem 오브젝트를 획득
        if (Input.GetKeyDown(pickupKey) && isPickUp)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("FieldItem"))
                {
                    FieldItem fieldItem = collider.GetComponent<FieldItem>();
                    if (fieldItem != null && inventory.AddItem(fieldItem.GetItem()))
                    {
                        fieldItem.DestroyItem();
                        isPickUp = false;
                        pickupText.gameObject.SetActive(false);
                    }
                }
            }
        }

        // 근처 아이템이 없을 경우
        if (!hasFieldItem)
        {
            isPickUp = false;
            pickupText.gameObject.SetActive(false);
        }
    }
}