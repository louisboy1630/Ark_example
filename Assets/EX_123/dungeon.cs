using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dungeon : MonoBehaviour
{//怪物基本數值
    public int hp = 100; //血量
    public float attackTime = 2;//攻擊速度
    public float hurtTime = 2;//受傷後無敵時間
    public bool attackStatus = false;//攻擊狀態
    public bool hurtStatus = false;//受傷狀態
    public bool dead = false;//死亡狀態

    //追逐功能
    public GameObject player;//宣告追逐目標
    private NavMeshAgent agent;//宣告導航功能
    private Animator animator;//宣告動畫
    private AnimatorStateInfo animatorStateInfo;//取得動畫狀態


     
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //程式會自動抓取工具不必手動拖曳
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //計算怪物跟目標的距離

        float dis = Vector3.Distance(transform.position, player.transform.position);

        if (dis < 10 && dis > 2.5f)
        {
            animator.SetBool("Speed", true);
        }
        else if (dis <= 2.5f && attackStatus == false) 
        {
           animator.SetTrigger("Attack");
            attackStatus = true;
        }
        else
        {
            animator.SetBool("Speed", false);
            agent.ResetPath();
        }

        TackTarget();
        AttackEvent();

    }
    /// <summary>
    /// 追逐目標功能
    /// </summary>
    void TackTarget()
    {
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.IsName("Run") && animatorStateInfo.normalizedTime >=0.0001f)
        {
            agent.SetDestination(player.transform.position);
        }
    }
    /// <summary>
    /// 攻擊事件
    /// </summary>
    void AttackEvent()
    {
        if(attackStatus == true)
        {
            attackTime -= Time.deltaTime;
            if (attackTime < 0)
            {
                attackTime = 2;
                attackStatus = false; 
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            //this.GetComponent<Rigidbody>().AddForce(direction * 200);

            hp -= 50;
            if (hp <= 0)
            {
                animator.SetBool("Death", true);

                dead = true;

                Destroy(this.gameObject, 2);
                
            }
            else
            {
                animator.SetTrigger("Damage");
            }
        }
    }
}
