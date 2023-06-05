using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1f;
    private int collectedItemCount = 0; // 획득한 아이템 개수
    public KeyCode pickupKey; // 단축키 설정
    public float pickupRadius = 1f; // 픽업 반경 설정
    public float pickupDelay = 2f; // 먹기까지의 딜레이 시간
    private bool isPickupDelay = false; // 딜레이 중인지 여부
    private float pickupTime = 0f; // 먹기 시작한 시간
    private Inventory inventory;

    [SerializeField] private Text pickupText;
    [SerializeField] private Text pickupTextDelay;

    private bool isPickUp = false;

    private Animator anim; //애니메이션 제어
    private float scaleX;//좌우반전용 스케일 변수
    private float scaleY;
    private float scaleZ;
    Rigidbody2D rigid;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    void Awake()//애니메이션 제어
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    private void Update()
    {
        // 플레이어 이동
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical);
        transform.Translate(direction * speed * Time.deltaTime);

        if (horizontal > 0) //방향 전환
        { transform.localScale = new Vector3(-scaleX, scaleY, scaleZ); }
        else if (horizontal < 0)
        { transform.localScale = new Vector3(scaleX, scaleY, scaleZ); }
        if (horizontal != 0 || vertical != 0) //애니메이션 제어
        { anim.SetBool("isWalk", true); }
        else { anim.SetBool("isWalk", false); }

        // 근처 아이템이 있을 경우
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
        bool hasFieldItem = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("FieldItem"))
            {
                hasFieldItem = true;
                isPickUp = true;
                pickupText.gameObject.SetActive(!isPickupDelay); // 딜레이 중이지 않을 때만 활성화
                pickupText.text = "Press " + pickupKey.ToString() + " to pick up";
                pickupTextDelay.gameObject.SetActive(isPickupDelay); // 딜레이 텍스트 활성화
                pickupTextDelay.text = "Picking up...";
                break;
            }
        }

        // 아이템 획득 단축키를 누르면 근처의 FieldItem 오브젝트를 획득
        if (Input.GetKeyDown(pickupKey) && isPickUp && collectedItemCount < 5)
        {
            if (!isPickupDelay)
            {
                isPickupDelay = true;
                pickupTime = Time.time;
                pickupText.gameObject.SetActive(false); // 기존 텍스트 비활성화
                pickupTextDelay.gameObject.SetActive(true); // 딜레이 텍스트 활성화
                pickupTextDelay.text = "Picking up...";
            }
        }

        // 딜레이가 끝났을 경우 아이템을 획득
        if (isPickupDelay && Time.time >= pickupTime + pickupDelay)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("FieldItem"))
                {
                    FieldItem fieldItem = collider.GetComponent<FieldItem>();
                    if (fieldItem != null && inventory.AddItem(fieldItem.GetItem()))
                    {
                        fieldItem.DestroyItem();
                        collectedItemCount++;
                        isPickUp = false;
                        pickupText.gameObject.SetActive(!isPickupDelay); // 딜레이 중이지 않을 때만 활성화
                        pickupTextDelay.gameObject.SetActive(isPickupDelay); // 딜레이 텍스트 활성화
                        pickupTextDelay.text = "Picking up...";
                    }
                }
            }

            isPickupDelay = false;
        }

        // 근처 아이템이 없을 경우
        if (!hasFieldItem)
        {
            isPickUp = false;
            pickupText.gameObject.SetActive(false);
            pickupTextDelay.gameObject.SetActive(false); // 딜레이 텍스트 비활성화
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        collectedItemCount = 0; // 씬 이동 시 collectedItemCount를 0으로 초기화
    }
}
