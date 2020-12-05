using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellen : MonoBehaviour
{
    public float speed = 2;
    public Animator animator;
    public GameObject cam;
    public GameObject gunCam;
    public bool status = false;
    public float cd = 0.3f;
    public GameObject bullet;

    public float stoptime = 0;//累積停止移動時間
    public bool isMove = false;//是否為移動
    //血量功能
    public float HP = 100;
    public float MAXHP = 100;
    public GameObject HPUi;

    //瞄準攝影機位置
    //public Transform gunCameraTransform;
    //public Transform normalCameraTransform;

    public Transform bulletTransform;
    public GameObject thirdperson;

    //飛劍的功能列
    public GameObject[] sword;// 名稱
    public int swordMaxCount = 6; //武器最大數量
    public int swordCount = 6;    //武器當前數量
    public float swordCd = 0;     //武器冷卻時間
    public float swordGetTime = 4; //武器補充時間


    
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    // Start is called before the first frame update
    void Start()
    {
        //animator.SetFloat("speed", 1);
    }

    // Update is called once per frame
    void Update()
    {


        if (status == true)
        {
            this.transform.position += this.transform.TransformDirection(Vector3.back * 0.05f);
            cd -= Time.deltaTime;
            if (cd <0)
            {
                status = false;
                cd = 0.3f;
            }
        }

        HPUi.transform.localPosition = new Vector3(-399 + 399 * (HP / MAXHP), 0.0f, 0.0f);


        SwordEvent();

        //武器冷卻的事件
        if (swordCd > 0)
        {
            swordCd -= Time.deltaTime;
        }
    }
 /// <summary>
 /// 角色移動功能
 /// </summary>
    void PlayerMove()
    {
        AnimatorStateInfo anim = animator.GetCurrentAnimatorStateInfo(0);
        if (anim.IsName("stand") && anim.normalizedTime >= 0.1f ||
           anim.IsName("move") && anim.normalizedTime >= 0.1f)
                    
        { 
            // Unity取得按鈕值
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 vector3 = new Vector3(h, 0, v);

            Debug.Log("vector3.magnitude:" + vector3.magnitude);

            if (vector3.magnitude > 0.1f) // if (v != 0 || h != 0)
            {
                float moveSpeed = 1; //原本跑步速度
                if (Input.GetButton("Fire3"))// 按下SHIFT
                {
                    moveSpeed = 0.5f;
                }




                Debug.Log(" Mathf.Atan:" + Mathf.Atan2(vector3.x, vector3.z));
                //角色旋轉
                float targetAngle =
                    Mathf.Atan2(vector3.x, vector3.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float angle =
                    Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                //角色往前
                Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                
                transform.Translate(move * moveSpeed* speed * Time.deltaTime, Space.World);

                animator.SetFloat("Speed", moveSpeed* speed);

                isMove = true;
                stoptime = 0;

            }
            else
            {
                isMove = false;
                animator.SetFloat("Speed", 0.1f);
            }
        
        }
        if (!isMove)
        {
            stoptime += Time.deltaTime;
            if (stoptime >= 0.2f)
            {
                animator.SetFloat("Speed", 0);
            }
        }




        /*
        if (status == false)
        {

            if (v != 0 || h != 0)
            {
                Vector3 vector3 = new Vector3(h, 0, v);
                //float Y = cam.transform.rotation.eulerAngles.y;

                //vector3 = Quaternion.Euler(0, Y, 0) * vector3;

                if (Input.GetButton("Fire2"))
                {
                    transform.Translate(vector3 * Time.deltaTime * (speed / 2), Space.World);

                    //Debug.Log(Time.deltaTime * speed * 500);
                    animator.SetFloat("Speed", speed);
                }
                else
                {
                    transform.Translate(vector3 * Time.deltaTime * speed, Space.World);

                    //Debug.Log(Time.deltaTime * speed * 500);
                    animator.SetFloat("Speed", Time.deltaTime * speed * 500);
                }

            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
        }
        */
    }

/// <summary>
/// 武器補充事件
/// </summary>
    void SwordEvent()
    {
        if (swordMaxCount > swordCount)
        {
            swordGetTime -= Time.deltaTime;
            if (swordGetTime < 0)
            {
                swordCount += 1;
                swordGetTime = 4;

                sword[swordCount-1].SetActive(true);
            }

        }
    }

    private void FixedUpdate()
    {
        PlayerMove();

        if (Input.GetButtonDown("Fire1"))
        {
        }

        if (Input.GetButton("Fire2"))
        {
            Debug.Log("Fire2");
            thirdperson.SetActive(false);
            cam.SetActive(false);
            gunCam.SetActive(true);
        
            //cam.transform.localPosition = gunCameraTransform.localPosition;
            //cam.transform.localRotation = gunCameraTransform.localRotation;

            // 發射子彈的功能
            if (Input.GetButtonDown("Fire1"))
            {
                if (swordCount > 0 && swordCd <= 0)
                {
                    Debug.Log("Fire1");
                    animator.SetBool("Magic", true);

                    Invoke("CreateBullet", 0.5f);

                    swordCount -= 1;

                    swordCd = 2;

                    sword[swordCount].SetActive(false);
                }
                else
                {
                    //子彈不足時的事件

                }
               
            }
            else
            {
                animator.SetBool("Magic", false);
            }
        }
        else
        {
           // cam.transform.localPosition = normalCameraTransform.localPosition;
            thirdperson.SetActive(true);
            cam.SetActive(true);
            gunCam.SetActive(false);

            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fire1");
                animator.SetTrigger("Attack");
            }

    }
}

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.CompareTag("monster") && status == false)
        {
            Debug.Log("碰撞怪物");
            hp -= 10;
            animator.SetBool("monsterHit", true);
            status = true;
        
        }
        //animator.SetBool("monsterHit", false);
    }
    */
    /// <summary>
    /// 創建子彈
    /// </summary>
        void CreateBullet()
    {
        Instantiate(bullet, bulletTransform.position, gunCam.transform.rotation);
    }
    /// <summary>
    /// 實體碰撞事件
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("monster") && status == false)
        {
            Debug.Log("碰撞怪物");
            HP -= 5;

            animator.SetTrigger("monsterHit");
            status = true;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TP"))
        {
            transform.position = new Vector3(46, 5.2f, 20f);
        }



        if (other.gameObject.CompareTag("TPback"))
        {
            transform.position = new Vector3(35.4f, 4.3f, 45);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("monsterWeapon") &&status == false)
        {
            Debug.Log("被怪物砍");
            HP -= 15;
            animator.SetTrigger("monsterHit");
            status = true;
        }
    }
}
