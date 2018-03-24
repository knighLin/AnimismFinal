using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //call other class
    private EnemyHealth enemyHealth;
    private TypeValue typeValue;
    private PossessedSystem possessedSystem;
    
    //Animator
    private Animator animator;
    public CapsuleCollider weaponCollider;
    //public BoxCollider weaponCollider2;

    //Audio
    private AudioSource audioSource;
    public AudioClip attack;
    public AudioClip summon;//召喚
    
    private bool CanAttack = true;
    [SerializeField] private Rigidbody WolfGuards;//召喚狼
    [SerializeField] private Transform SummonPoint1;
    [SerializeField] private Transform SummonPoint2;
    Rigidbody rigidbody;
    
    void Awake()
    {
        //set Animator
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //set WeaponCollider
        weaponCollider.enabled = false;
        //weaponCollider2.enabled = false;
        // Physics.IgnoreCollision(myselfCollider, weaponCollider);//讓兩個物體不會產生碰撞
    }

    void Start()
    {
        possessedSystem = GetComponent<PossessedSystem>();
    }

    private void Update()
    {
        switch (possessedSystem.Possessor.tag)
        {//判斷是不是可以附身的物件
            case "Player":
                if (CanAttack)
                {
                    if (Input.GetButtonDown("SquareAttack"))//Attack
                    {
                        //this.GetComponent<Rigidbody>().isKinematic = true;
                   
                        CanAttack = false;
                        animator.SetTrigger("Attack");
                        animator.SetInteger("Render", AttackRender());
                        Invoke("ResetAttackFlag", 1.5f);
                    }
                }
                break;
            case "Wolf":
                if (CanAttack)
                {
                    rigidbody = GetComponent<Rigidbody>();
                    if (Input.GetButtonDown("SquareAttack"))
                    {
                        CanAttack = false;
                        animator.SetTrigger("Attack");
                        animator.SetInteger("Render", AttackRender());
                        Invoke("ResetAttackFlag", 1.2f);
                    }
                    if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("TriangleAbility"))//特殊技
                    {
                        if (PossessedSystem.OnPossessed == true && PossessedSystem.PossessedCol.enabled == false)
                        {
                            animator.SetTrigger("Surgery");
                            audioSource.PlayOneShot(summon);
                            Instantiate(WolfGuards, SummonPoint1.position, Quaternion.identity).name = "WolfGuard1";
                            Instantiate(WolfGuards, SummonPoint2.position, Quaternion.identity).name = "WolfGuard2";
                        }
                    }
                }

                break;
            case "Bear":
            case "Deer":
                break;
            default:
                return;
        }
    }

    int AttackRender()
    {
        int AttackCount = Random.Range(0, 2);
        return AttackCount;
    }
    void WeaponSound()
    {
        audioSource.PlayOneShot(attack);
    }
    void WeaponColliderOpen()
    {
        weaponCollider.enabled = true;
        //weaponCollider2.enabled = true;
    }
    void WeaponColliderClose()
    {
        weaponCollider.enabled = false;
        //weaponCollider2.enabled = false;
       // Time.timeScale = 1f;
    }
    void ResetAttackFlag()
    {
        //animator.SetBool("Attack", false);
        // this.gameObject.GetComponent<PlayerMovement>().enabled = false;
       // this.GetComponent<Rigidbody>().isKinematic = false;
        CanAttack = true;
    }

    //void AttackEffectOpen()
    //{
    //    gameObject.GetComponentInChildren<MeleeWeaponTrail>().enabled = true;
    //}
    //void AttackEffectClose()
    //{
    //    gameObject.GetComponentInChildren<MeleeWeaponTrail>().enabled = false;
    //}

    void WolfAddForce()
    {
        //rigidbody.AddForce(Vector3.forward * 100);
    }
}
