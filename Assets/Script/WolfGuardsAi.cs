using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfGuardsAi : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private GameObject Target;
    private float ToEnemyDis;//主角跟敵人的距離
    private NavMeshAgent Nav;
    private Animator Anim;
    private List<GameObject> EnemyTarget = new List<GameObject>();
    //private List<EnemyHealth> enemyHealth = new List<EnemyHealth>();
    private Vector3 LookRotateToEnemy;
    public CapsuleCollider weaponCollider;
    private AudioSource audioSource;
    public AudioClip attack;
    float m_TurnAmount;//轉向值
    float timer = 0;
    bool NoEnemy = true;

    void Awake()
    {
        Target = GameObject.FindWithTag("Player");
        Nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Nav.enabled = false;
        Destroy(gameObject, 60);
        //StartCoroutine("DistoryTime");
    }

    private void Start()
    {
        Nav.enabled = true;
    }
    private void Update()
    {
        if (NoEnemy && (!Target || Target.tag != "Player"))
        {
            Target = GameObject.FindWithTag("Player");
            Nav.stoppingDistance = 3f;
        }
        if(Target.tag == "Player")
        {
            Nav.SetDestination(Target.transform.position);
        }

        //TargetInRange();
        if (Nav.remainingDistance < Nav.stoppingDistance) //如果移動位置小於停止位置，不跑步
        {
            Nav.isStopped = true;
            Anim.SetFloat("Speed", Nav.desiredVelocity.sqrMagnitude, 0.1f, Time.deltaTime);
            m_TurnAmount = Mathf.Atan2(Target.transform.position.x, Target.transform.position.z);
            Anim.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        }
        else
        {
            Nav.isStopped = false;
            Nav.SetDestination(Target.transform.position);
            Anim.SetFloat("Speed", Nav.desiredVelocity.sqrMagnitude, 0.1f, Time.deltaTime);
            m_TurnAmount = Mathf.Atan2(Target.transform.position.x, Target.transform.position.z);
            Anim.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        }
    }

    int AttackRender()
    {
        int AttackCount = Random.Range(0, 2);
        return AttackCount;
    }

    //private void TargetInRange()
    //{
    //    if (EnemyTarget.Count == 0)//如果沒有敵人，目標是主角
    //    {
    //        Nav.SetDestination(Target.transform.position);
    //    }
    //    else if (EnemyTarget.Count > 0)
    //    {
    //        float ShortestRange;//敵人跟目標最短距離
    //        if (EnemyTarget.Count > 1)
    //        {
    //            for (int i = 0; i < EnemyTarget.Count; i++)
    //            {
    //                if ((Vector3.Distance(transform.position, EnemyTarget[i].transform.position) < Vector3.Distance(transform.position, EnemyTarget[i + 1].transform.position)))
    //                {//第一個目標與第二個目標判斷最小距離，當一小於二
    //                    ShortestRange = Vector3.Distance(transform.position, EnemyTarget[i].transform.position);
    //                    Target = EnemyTarget[i];//將敵人攻擊目標轉成一
    //                    EnemyTarget[i + 1] = EnemyTarget[i];//將目標二變成目標一，之後在跟目標三比
    //                    ToEnemyDis = ShortestRange;
    //                }
    //                else
    //                {
    //                    ShortestRange = Vector3.Distance(transform.position, EnemyTarget[i + 1].transform.position);
    //                    Target = EnemyTarget[i + 1];
    //                    ToEnemyDis = ShortestRange;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            Target = EnemyTarget[0];
    //            ToEnemyDis = Vector3.Distance(transform.position, EnemyTarget[0].transform.position);
    //        }

    //    }
    //    if (ToEnemyDis <= 5)//當召喚狼與敵人距離小於5
    //    {//轉向敵人，跑向敵人
    //    LookRotateToEnemy = new Vector3((Target.transform.position.x - transform.position.x), 0, (Target.transform.position.z - transform.position.z));//Turn to Target
    //    transform.rotation = Quaternion.LookRotation(LookRotateToEnemy);
    //    Nav.SetDestination(Target.transform.position);
    //    for (int i = 0; i < EnemyTarget.Count; i++)
    //    {
    //        if (Target == EnemyTarget[i] && enemyHealth[i].currentHealth > 0)
    //        {//當Enemy的還有血量時
    //            if (Time.time - timer > 1f)//時間大於一秒就攻擊
    //            {
    //                Anim.SetTrigger("Attack");//之後要改誠動畫
    //                Anim.SetInteger("Render", AttackRender());
    //                timer = Time.time;
    //            }
    //        }
    //    }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        EnemyTarget.Add(other.transform.gameObject);
    //        enemyHealth.Add(other.GetComponent<EnemyHealth>());
    //        Nav.stoppingDistance = 0.5f;
    //    }
    //    else if (other.tag == "Player")
    //    {
    //        Target = GameObject.FindWithTag("Player");
    //        Nav.stoppingDistance = 3f;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        EnemyTarget.Clear();
    //        enemyHealth.Clear();
    //        Target = GameObject.FindWithTag("Player");
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            NoEnemy = false;
            Target = other.gameObject;
            Nav.stoppingDistance = 0.5f;
            enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth.currentHealth > 0)
            {//當Enemy的還有血量時
                transform.LookAt(other.transform);
                Nav.SetDestination(other.transform.position);
                if (Time.time - timer > 1f)//時間大於一秒就攻擊
                {
                    Anim.SetTrigger("Attack");
                    Anim.SetInteger("Render", AttackRender());
                    timer = Time.time;
                }
            }
        }
        else
        {
            NoEnemy = true;
        }
    }

    IEnumerator DistoryTime()//兩分鐘後消失
    {
        yield return new WaitForSeconds(120);
        Destroy(gameObject);
    }
    void WeaponSound()
    {
        audioSource.PlayOneShot(attack);
    }
    void WeaponColliderOpen()
    {
        weaponCollider.enabled = true;
    }
    void WeaponColliderClose()
    {
        weaponCollider.enabled = false;
    }
}
