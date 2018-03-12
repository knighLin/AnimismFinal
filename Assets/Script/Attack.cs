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
    public int HumanAtk = 10;//攻擊力
    public Collider weaponCollider;
    public Collider myselfCollider;

    //Audio
    private AudioSource audioSource;
    public AudioClip attack;
    public AudioClip summon;//召喚

    private float timer = 0;
    private bool CanAttack = true;
    [SerializeField] private Rigidbody WolfGuards;//召喚狼
    void Awake()
    {
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
                    if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("SquareAttack"))//Attack
                    {
                        CanAttack = false;
                        animator.SetBool("Attack", true);
                        animator.SetInteger("Render", AttackRender());
                        Invoke("ResetAttackFlag", 2f);
                    }
                }
                break;
            case "Wolf":
                if (Input.GetButtonDown("SquareAttack") && weaponCollider.enabled == false)
                {
                    animator.SetTrigger("Attack");
                    animator.SetInteger("Render", AttackRender());
                }
                if (Input.GetMouseButtonDown(1))//特殊技
                {
                    if (PossessedSystem.WolfCount >= 1 && PossessedSystem.OnPossessed == true && PossessedSystem.PossessedCol.enabled == false)
                    {
                        animator.SetTrigger("Surgery");
                        audioSource.PlayOneShot(summon);
                        Vector3 MovePoint = new Vector3(Random.Range(this.gameObject.transform.position.x - 2, this.gameObject.transform.position.x + 2), this.gameObject.transform.position.y + 2, Random.Range(this.transform.position.z - 2, this.transform.position.z + 2));
                        Vector3 MovePoint2 = new Vector3(Random.Range(this.gameObject.transform.position.x - 2, this.gameObject.transform.position.x + 2), this.gameObject.transform.position.y + 2, Random.Range(this.transform.position.z - 2, this.transform.position.z + 2));
                        Instantiate(WolfGuards, MovePoint, Quaternion.identity);
                        Instantiate(WolfGuards, MovePoint2, Quaternion.identity);
                        PossessedSystem.WolfCount = 0;
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
        animator.SetBool("Attack", false);
        CanAttack = true;
    }
}
