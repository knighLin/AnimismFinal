using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class Health : MonoBehaviour
{
    private HPcontroller HPcontroller;
    private PlayerMovement playerMovement;//角色的移動
    private RagdollBehavior ragdollBehavior;
    private PossessedSystem possessedSystem;
    private FullBodyBipedIK ik;
    private GrounderFBBIK GroundIk;

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
    private string HitCount;

    private void Awake()
    {
        possessedSystem = GetComponent<PossessedSystem>();
        playerMovement = GetComponent<PlayerMovement>();
        ik = GetComponent<FullBodyBipedIK>();
        GroundIk = GetComponent<GrounderFBBIK>();
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
    //void Update()
    //{
        //if (currentHealth <= 0 && isDead == false && this.gameObject == possessedSystem.Possessor)
        //{
        //    StopCoroutine("HurtAnimation");
        //    Death();
        //}
        //if (this.gameObject != possessedSystem.Possessor && currentHealth < MaxHealth * 0.3f)//當動物血量小於30%，分離主角，並扣出主角原本血量的一半
        //{
        //    possessedSystem.LifedPossessed();
        //    possessedSystem.Possessor.GetComponent<Health>().currentHealth = StoreHumanHealth * 0.5f;
        //    //Debug.Log(StorePlayerHealth);
        //    // CanPossessed = false;
        //    enabled = false;
        //    // HPcontroller.CharacterHpControll();
        //}
    //}

    public void Hurt(float Amount, Vector3 HitPoint)
    {

        if (this.gameObject == possessedSystem.Possessor)
        {
            if (isDead)// ... no need to take damage so exit the function.
                return;
            if (currentHealth > 0)
            {
                if (HitPoint.x > 0)//FontHit
                {
                    HitCount = 1.ToString();
                }
                else if(HitPoint.x < 0)//BackHit
                {
                    HitCount = 2.ToString();
                }
                else if(HitPoint.z > 0)//LeftHit
                {
                    HitCount = 3.ToString();
                }
                else if (HitPoint.z < 0)//RightHit
                {
                    HitCount = 4.ToString();
                }
                currentHealth -= Amount;//扣血
                StartCoroutine("HurtAnimation");
            }
            if (currentHealth <= 0.5)
            {
                StopCoroutine("HurtAnimation");
                Death();
            }
        }
        else//動物的
        {
            if (currentHealth > 0)
            {
                currentHealth -= Amount;//扣血
                audioSource.PlayOneShot(hurt);
                animator.SetTrigger("Hurt");
            }
            if (currentHealth < MaxHealth * 0.3f)
            {
                possessedSystem.LifedPossessed();
                possessedSystem.Possessor.GetComponent<Health>().currentHealth = StoreHumanHealth * 0.5f;
                this.gameObject.layer = 12;
                enabled = false;
            }
            
        }
        //audioSource.PlayOneShot(hurt);
        HPcontroller.CharacterHpControll();
        HPcontroller.Blink = true;

    }

    IEnumerator HurtAnimation()//人用的
    {
        Debug.Log(HitCount);
        audioSource.PlayOneShot(hurt);
        ik.enabled = false;
        GroundIk.enabled = false;
        animator.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        yield return new WaitForSeconds(0.5f);
        if (isDead == false)
        {
            ik.enabled = true;
            GroundIk.enabled = true;
            animator.enabled = true;
            SetHitPointUI(HitCount);
        }
        ragdollBehavior.ToggleRagdoll(false);
        //StopCoroutine(HurtAnimation());
    }

    void SetHitPointUI(string Count)
    {
        switch (Count)
        {
            case "1"://正面受傷
                animator.SetTrigger("FontHurt");
                break;
            case "2"://背面受傷
                animator.SetTrigger("BackHurt");
                break;
            case "3"://左側受傷
                animator.SetTrigger("LeftHurt");
                break;
            case "4"://右側受傷
                animator.SetTrigger("RightHurt");
                break;

        }
    }

    void Death()
    {
        isDead = true;
        ik.enabled = false;
        GroundIk.enabled = false;
        animator.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        playerMovement.enabled = false;
    }
}
