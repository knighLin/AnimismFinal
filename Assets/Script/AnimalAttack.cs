using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAttack : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private TypeValue typeValue;
    private PossessedSystem possessedSystem;
    private Animator Anim;
    [SerializeField] private Rigidbody WolfGuards;//召喚狼
    public Collider weaponCollider;

    //Audio
    private AudioSource audioSource;
    public AudioClip attack;
    
    void Start()
    {
        Anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        typeValue = GameObject.FindWithTag("PlayerManager").GetComponent<TypeValue>();
        possessedSystem = GetComponent<PossessedSystem>();
    }

    int AttackRender()
    {
        int AttackCount = Random.Range(0, 2);
        return AttackCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (possessedSystem.Possessor.tag == "Wolf")
        {
            if (Input.GetButtonDown("SquareAttack") && weaponCollider.enabled == false)
            {
                Anim.SetTrigger("Attack");
                Anim.SetInteger("Render", AttackRender());
            }

            if (Input.GetMouseButtonDown(1) && PossessedSystem.WolfCount >= 1 && PossessedSystem.OnPossessed == true)
            {
                Vector3 MovePoint = new Vector3(Random.Range(this.gameObject.transform.position.x - 2, this.gameObject.transform.position.x + 2), this.gameObject.transform.position.y, Random.Range(this.transform.position.z - 2, this.transform.position.x + 2));
                Vector3 MovePoint2 = new Vector3(Random.Range(this.gameObject.transform.position.x - 2, this.gameObject.transform.position.x + 2), this.gameObject.transform.position.y, Random.Range(this.transform.position.z - 2, this.transform.position.x + 2));
                Instantiate(WolfGuards, MovePoint, Quaternion.identity);
                Instantiate(WolfGuards, MovePoint2, Quaternion.identity);

                PossessedSystem.WolfCount = 0;
            }
        }
        else if (possessedSystem.Possessor.tag == "Bear")
        {
        }

        
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
