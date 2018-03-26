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
        if(!Target)
            NoEnemy = true;
        if ((!Target || Target.tag != "Player")&&NoEnemy)
        {
            Target = GameObject.FindWithTag("Player");
            Nav.stoppingDistance = 3f;
        }
        if (Target && Target.tag == "Player")
        {
            Nav.SetDestination(Target.transform.position);
        }
        

        //TargetInRange();
        if (Target && Nav.remainingDistance < Nav.stoppingDistance) //如果移動位置小於停止位置，不跑步
        {
            Nav.isStopped = true;
            Anim.SetFloat("Speed", Nav.desiredVelocity.sqrMagnitude, 0.1f, Time.deltaTime);
            m_TurnAmount = Mathf.Atan2(Target.transform.position.x, Target.transform.position.z);
            Anim.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        }
        else if (Target)
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

    private void OnTriggerStay(Collider other)
    {
        switch(other.tag)
        {
            case "Enemy":
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
                break;
            //default:
                
            //    return;
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
