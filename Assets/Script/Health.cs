using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    private HPcontroller HPcontroller;
    private PlayerMovement playerMovement;//角色的移動
    private RagdollBehavior ragdollBehavior;
    private PossessedSystem possessedSystem;

    public float MaxHealth = 100; //最大HP
    public float currentHealth; //當前HP
    private Animator animator;
    public static bool isDead;//是否死亡
    //audio
    private AudioSource audioSource;
    public AudioClip hurt;

    private float timer;//開啟ragdoll的時間
    //public CapsuleCollider m_collider;
    private float StoreHumanHealth;
    public static bool CanPossessed;

    private void Awake()
    {
        possessedSystem = GetComponent<PossessedSystem>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        HPcontroller = GameObject.Find("PlayerManager").GetComponent<HPcontroller>();
        possessedSystem = GetComponent<PossessedSystem>();
        currentHealth = MaxHealth;//開始時，當前ＨＰ回最大ＨＰ
        if (this.gameObject == possessedSystem.Possessor)
        {
            ragdollBehavior = GetComponent<RagdollBehavior>();
        }
    }

    private void OnEnable()
    {
        if (this.gameObject != possessedSystem.Possessor)
        {
            StoreHumanHealth = possessedSystem.Possessor.GetComponent<Health>().currentHealth;
        }
    }
    void Update()
    {
        if (currentHealth <= 0 && isDead == false && this.gameObject == possessedSystem.Possessor)
        {
            StopCoroutine("HurtAnimation");
            Death();
        }
        if (this.gameObject != possessedSystem.Possessor && currentHealth < MaxHealth * 0.3f)//當動物血量小於30%，分離主角，並扣出主角原本血量的一半
        {
            possessedSystem.LifedPossessed();
            possessedSystem.Possessor.GetComponent<Health>().currentHealth = StoreHumanHealth * 0.5f;
            //Debug.Log(StorePlayerHealth);
            // CanPossessed = false;
            enabled = false;
            // HPcontroller.CharacterHpControll();
        }
    }

    public void Hurt(float Amount)
    {
        
        if(currentHealth > 0)
        {
            currentHealth -= Amount;//扣血
            if (this.gameObject == possessedSystem.Possessor)
            {
                StartCoroutine("HurtAnimation");
            }
            else//動物的
            {
                animator.SetTrigger("Hurt");

            }
            //audioSource.PlayOneShot(hurt);
            HPcontroller.CharacterHpControll();
            HPcontroller.Blink = true;
        }
        
    }

    IEnumerator HurtAnimation()//人用的
    {
        animator.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        yield return new WaitForSeconds(0.8f);
        if (isDead == false)
        {
            animator.enabled = true;
        }
        ragdollBehavior.ToggleRagdoll(false);
        //StopCoroutine(HurtAnimation());
    }
    void Death()
    {
        isDead = true;
        // m_collider.enabled = false;
        animator.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        
        print("END");
        playerMovement.enabled = false;
        //enabled = false;
        //animator.SetBool("Die",isDead);
        //Destroy(gameObject,4f);
    }
}
