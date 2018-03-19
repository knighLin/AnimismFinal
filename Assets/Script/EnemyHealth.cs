using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RootMotion.FinalIK;

public class EnemyHealth : MonoBehaviour
{
    private RagdollBehavior ragdollBehavior;
    private EnemyAI enemyAI;
    private AimIK aim;
    private FullBodyBipedIK ik;
    private GrounderFBBIK GroundIk;
    private SecondHandOnGun secondHandOnGun;

    public float MaxHealth = 100; //最大HP
    public float currentHealth; //當前HP
                                //public CapsuleCollider body;
    bool isDead;//是否死亡

    private Animator Anim;
    private NavMeshAgent nav;

    //audio
    private AudioSource audioSource;
    public AudioClip hurt;


    void Awake()
    {
        ragdollBehavior = GetComponent<RagdollBehavior>();
        enemyAI = GetComponent<EnemyAI>();
        aim = GetComponent<AimIK>();
        ik = GetComponent<FullBodyBipedIK>();
        GroundIk = GetComponent<GrounderFBBIK>();
        secondHandOnGun = GetComponent<SecondHandOnGun>();
        currentHealth = MaxHealth;//開始時，當前ＨＰ回最大ＨＰ
        audioSource = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    public void Hurt(float Amount)
    {
        if (isDead)// ... no need to take damage so exit the function.
            return;

        if (currentHealth > 0)
        {
            currentHealth -= Amount;//扣血
                                    //audioSource.PlayOneShot(hurt);
                                    //Anim.SetTrigger("Hurt");
            StartCoroutine("HurtAnimation");
        }
        if (currentHealth <= 0)
        {
            StopCoroutine("HurtAnimation");
            Death();
        }
    }

    IEnumerator HurtAnimation()//人用的
    {
        audioSource.PlayOneShot(hurt);
        aim.enabled = false;
        ik.enabled = false;
        GroundIk.enabled = false;
        secondHandOnGun.enabled = false;
        nav.enabled = false;
        Anim.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        yield return new WaitForSeconds(0.1f);
        if (isDead == false)
        {
            aim.enabled = true;
            ik.enabled = true;
            GroundIk.enabled = true;
            secondHandOnGun.enabled = true;
            nav.enabled = true;
            Anim.enabled = true;
        }
        ragdollBehavior.ToggleRagdoll(false);
    }

    void Death()
    {
        isDead = true;
        aim.enabled = false;
        ik.enabled = false;
        GroundIk.enabled = false;
        secondHandOnGun.enabled = false;
        nav.enabled = false;
        Anim.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        enemyAI.enabled = false;
        Destroy (gameObject, 5f);
    }

}
