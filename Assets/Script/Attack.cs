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
    public Collider weaponCollider;
    public Collider myselfCollider;

    //Audio
    private AudioSource audioSource;
    public AudioClip attack;
    public AudioClip summon;//召喚

    private float timer = 0;
    private bool CanAttack = true;
    [SerializeField] private Rigidbody WolfGuards;//召喚狼
    [SerializeField] private Transform SummonPoint1;
    [SerializeField] private Transform SummonPoint2;

    Rigidbody m_Rigidbody;
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        //set Animator
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //set WeaponCollider
        weaponCollider.enabled = false;
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
                        CanAttack = false;
                        animator.SetTrigger("Attack");
                        animator.SetInteger("Render", AttackRender());
                        Invoke("ResetAttackFlag", 1.5f);
                    }
                    if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && Input.GetButtonDown("SquareAttack"))
                    {
                        animator.SetTrigger("WalkAttack");
                    }
                }
                break;
            case "Wolf":
                if (CanAttack)
                {
                    if (Input.GetButtonDown("SquareAttack"))
                    {
                        CanAttack = false;
                        animator.SetTrigger("Attack");
                        animator.SetInteger("Render", AttackRender());
                        Invoke("ResetAttackFlag", 1.2f);
                    }
                    if (Input.GetMouseButtonDown(1))//特殊技
                    {
                        if (PossessedSystem.WolfCount >= 1 && PossessedSystem.OnPossessed == true && PossessedSystem.PossessedCol.enabled == false)
                        {
                            animator.SetTrigger("Surgery");
                            audioSource.PlayOneShot(summon);
                            Instantiate(WolfGuards, SummonPoint1.position, Quaternion.identity);
                            Instantiate(WolfGuards, SummonPoint2.position, Quaternion.identity);
                            PossessedSystem.WolfCount = 0;
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
    }
    void WeaponColliderClose()
    {
        weaponCollider.enabled = false;
    }
    void ResetAttackFlag()
    {
        //animator.SetBool("Attack", false);
        CanAttack = true;
    }
}
