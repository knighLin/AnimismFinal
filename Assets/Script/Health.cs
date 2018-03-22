using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class Health : MonoBehaviour
{
    private TypeValue value;
    private HurtBlood HurtBlood;
    private HPcontroller HPcontroller;
    private PlayerMovement playerMovement;//角色的移動
    private RagdollBehavior ragdollBehavior;
    private PossessedSystem possessedSystem;
    private FullBodyBipedIK ik;
    private GrounderFBBIK GroundIk;

    public float MaxHealth = 100; //最大HP
    public float currentHealth; //當前HP
    private Rigidbody m_rigidbody;
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
        value = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<TypeValue>();
        HurtBlood = GameObject.Find("PlayerManager").GetComponent<HurtBlood>();
        possessedSystem = GetComponent<PossessedSystem>();
        playerMovement = GetComponent<PlayerMovement>();
        ik = GetComponent<FullBodyBipedIK>();
        GroundIk = GetComponent<GrounderFBBIK>();
        m_rigidbody = GetComponent<Rigidbody>();
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
    
    public void Hurt(float Amount, float HitPoint)
    {
        if (this.gameObject == possessedSystem.Possessor)
        {
            if (isDead)// ... no need to take damage so exit the function.
                return;
            if (m_rigidbody.isKinematic == false)
            {
                if (currentHealth > 0)
                {
                    if (HitPoint < -0.35f)  
                    {
                        Debug.Log("FontHit");
                        HitCount = 1.ToString();
                    }
                    else if (HitPoint > 0.35f)
                    {
                        Debug.Log("BackHit");
                        HitCount = 2.ToString();
                    } 
                    else if (HitPoint < 0.35f && HitPoint > -0.35f && HitPoint > 0)
                    {
                        Debug.Log("RightHit");
                        HitCount = 3.ToString();
                    }
                    else if (HitPoint < 0.35f && HitPoint > -0.35f && HitPoint < 0)
                    {
                        Debug.Log("LeftHit");
                        HitCount = 4.ToString();
                    }
                    currentHealth -= Amount;//扣血

                    m_rigidbody.isKinematic = true;
                    animator.SetFloat("Speed", 0);
                   // PlayerMovement._Speed = 0;
                    animator.SetTrigger("Hurt");
                    audioSource.PlayOneShot(hurt);
                    Invoke("ResetRigidbodyFlag", 0.8f);
                    SetHitPointUI(HitCount);
                    //StartCoroutine("HurtAnimation");
                }
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
        HPcontroller.CharacterHpControll();
        HPcontroller.Blink = true;

    }

    IEnumerator HurtAnimation()//人用的
    {
        SetHitPointUI(HitCount);
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
        }
        ragdollBehavior.ToggleRagdoll(false);
    }

    void SetHitPointUI(string Count)
    {
        switch (Count)
        {
            case "1"://正面受傷
                //animator.SetTrigger("FontHurt");
                HurtBlood.Up = true;
                HurtBlood.Uptime = 0;
                break;
            case "2"://背面受傷
                //animator.SetTrigger("BackHurt");
                HurtBlood.Down = true;
                HurtBlood.Downtime = 0;
                break;
            case "3"://左側受傷
                //animator.SetTrigger("LeftHurt");
                HurtBlood.Left = true;
                HurtBlood.Lefttime = 0;
                break;
            case "4"://右側受傷
               // animator.SetTrigger("RightHurt");
                HurtBlood.Right = true;
                HurtBlood.Righttime = 0;
                break;
        }
    }

    void ResetRigidbodyFlag()
    {
         m_rigidbody.isKinematic = false;
        //PlayerMovement._Speed = value.MoveSpeed;
    }

    void Death()
    {
        isDead = true;
        ik.enabled = false;
        GroundIk.enabled = false;
        animator.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        playerMovement.enabled = false;
        Invoke("DelayGameOver", 3f);
    }
    void DelayGameOver()
    {
        Time.timeScale = 0;
        GameObject.Find("PlayerManager").GetComponent<Death>().IsDeath = true;
    }
}
