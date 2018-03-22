using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfGuardsAi : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private GameObject Target;
    private NavMeshAgent Nav;
    private Animator Anim;
    public CapsuleCollider weaponCollider;
    private AudioSource audioSource;
    public AudioClip attack;
    float m_TurnAmount;//轉向值

    void Awake()
    {
        Target = GameObject.FindWithTag("Player");
        Nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        
        Destroy(gameObject,60);
        //StartCoroutine("DistoryTime");
    }

    private void Update()
    {
        //if (Target != null)
        //{
         Nav.SetDestination(Target.transform.position);
        //}
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Nav.stoppingDistance = 0.5f;
            //Debug.Log("Find Enemy");
            enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth.currentHealth > 0)
            {//當Enemy的還有血量時
                transform.LookAt(other.transform);
                Nav.SetDestination(other.transform.position);
                Anim.SetTrigger("Attack");//之後要改誠動畫
                Anim.SetInteger("Render", AttackRender());
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Nav.SetDestination(Target.transform.position);
            Nav.stoppingDistance = 1.5f;
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
