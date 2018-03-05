using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttack : MonoBehaviour
{
    //call other class
    private EnemyHealth enemyHealth;
    private TypeValue typeValue;

    //Animator
    private Animator animator;
    public int HumanAtk = 10;//攻擊力
    public Collider weaponCollider;
    public Collider myselfCollider;

    //Audio
    private AudioSource audioSource;
    public AudioClip attack;

    private float timer = 0;

    void Awake()
    {
        //set Animator
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //set WeaponCollider
        weaponCollider.enabled = false;
       // Physics.IgnoreCollision(myselfCollider, weaponCollider);//讓兩個物體不會產生碰撞
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("SquareAttack") && weaponCollider.enabled == false)//Attack
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("Render", AttackRender());
            //StartCoroutine(AttackColliderClose());
        }
    }

    int AttackRender()
    {
        int AttackCount = Random.Range(0, 2);
        return AttackCount;
    }

     IEnumerator AttackColliderClose()
    {
        yield return new WaitForSeconds(1f);
        weaponCollider.enabled = false;
        StopCoroutine(AttackColliderClose());
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
