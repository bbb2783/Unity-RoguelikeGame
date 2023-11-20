using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MGun : MonoBehaviour
{
    public float turnSpd;
    
    private Rigidbody2D gunRB;//총 움직임
    private Vector2 mousePos;//마우스 값 받아옴
    private float angle;//마우스 각
    public static int modeSet = 0;
    
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    SpriteRenderer rend;
    Rigidbody2D rigid;

    float coolTime = 10.0f; //레일건 쿨타임
    float leftTime = 0.0f;
    float coolTimeSpeed = 1.0f;
    bool isUse = true;
    public Image coolimage;

    float EcoolTime = 20.0f; //전기장 쿨타임
    float EleftTime = 0.0f;
    bool isEUse = true;
    public Image Ecoolimage;
    float EcoolTimeSpeed = 1.0f;


    public Image Qspot;//키 스포트라이트
    public Image Espot;
    public Image Rspot;
    
    void start()
    {
        gunRB = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x); 
        

        if(angle < 1.5 && angle > -1.5)//플레이어 본이 마우스 좌표를 향해 회전하지 않음 -> 팔이 포함된 애니메이션이 원인!!!!!!!!! -> 해결
        {
            angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x); 
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), turnSpd * Time.deltaTime);
        }
        else 
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), turnSpd * Time.deltaTime);
        }
        
        if(leftTime > 0){ //레일건 쿨타임 돌리기
            leftTime -= Time.deltaTime*coolTimeSpeed;
            if(leftTime < 0){
                leftTime = 0;
                isUse = true;
            }
            float ratio = 1.0f - (leftTime / coolTime);
            if(coolimage) coolimage.fillAmount = ratio;
        }

        if(EleftTime > 0){ //전기장 쿨타임 돌리기
            EleftTime -= Time.deltaTime*EcoolTimeSpeed;
            if(EleftTime < 0){
                EleftTime = 0;
                isEUse = true;
            }
            float Eratio = 1.0f - (EleftTime / EcoolTime);
            if(Ecoolimage) Ecoolimage.fillAmount = Eratio;
        }
        
        
        
        if(Input.GetKeyDown(KeyCode.Q)) {modeSet = 1; Qspot.gameObject.SetActive(true); Rspot.gameObject.SetActive(false); Espot.gameObject.SetActive(false);}
        else if(Input.GetKeyDown(KeyCode.E)) {modeSet = 2; Qspot.gameObject.SetActive(false); Rspot.gameObject.SetActive(false); Espot.gameObject.SetActive(true);}
        else if(Input.GetKeyDown(KeyCode.R)) {modeSet = 3; Qspot.gameObject.SetActive(false); Rspot.gameObject.SetActive(true); Espot.gameObject.SetActive(false);}
        else if(Input.GetKeyDown(KeyCode.LeftShift)) {modeSet = 0; Qspot.gameObject.SetActive(false); Rspot.gameObject.SetActive(false); Espot.gameObject.SetActive(false);}

        if(modeSet == 0){//기본공격
            if(Input.GetMouseButtonDown(0))
            {
                GameObject Bullet = Instantiate(Resources.Load<GameObject>("Prefab/Bullet"), transform.position, transform.rotation);
                Bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Bullet.GetComponent<MBullet>().bulletSpd;
            }
        }
        else if(modeSet == 1){
            //가로베기
            if(Input.GetMouseButtonDown(0))
            {
                GameObject Ray = Instantiate(Resources.Load<GameObject>("Prefab/Knife"), transform.position, transform.rotation);
            }
        }
        else if(modeSet == 2){
            //전기장
            if(Input.GetMouseButtonDown(0) && isEUse == true)
            {
                GameObject Ray = Instantiate(Resources.Load<GameObject>("Prefab/Electric"), transform.position, Quaternion.identity);
                isEUse = false;
                EleftTime = 20.0f;
            }
        }
        else if(modeSet == 3){
            //레일건
            if(Input.GetMouseButtonDown(0) && isUse == true)
            {
                GameObject Ray = Instantiate(Resources.Load<GameObject>("Prefab/Ray"), transform.position, transform.rotation);
                isUse = false;
                leftTime = 10.0f;
            }
        }
        
    }
}
