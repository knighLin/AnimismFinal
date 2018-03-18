using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    private RagdollBehavior ragdollBehavior;
    private EnemyAI enemyAI;
	public float MaxHealth = 100; //最大HP
	public float currentHealth; //當前HP
    //public CapsuleCollider body;
	bool isDead;//是否死亡
                
    private Animator Anim;

    //audio
    private AudioSource audioSource;
    public AudioClip hurt;
    

    void Awake()
	{
        ragdollBehavior = GetComponent<RagdollBehavior>();
        enemyAI = GetComponent<EnemyAI>();
        currentHealth = MaxHealth;//開始時，當前ＨＰ回最大ＨＰ
        audioSource = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
	}

	public void Hurt(float Amount)
	{
        if (isDead)// ... no need to take damage so exit the function.
            return;
        if (currentHealth > 0)
        {
            currentHealth -= Amount;//扣血
            StartCoroutine("HurtAnimation");
        }
		if(currentHealth <= 0)
		{
			Death ();
		}
	}

    IEnumerator HurtAnimation()//人用的
    {
        audioSource.PlayOneShot(hurt);
        Anim.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        yield return new WaitForSeconds(0.3f);
        if (isDead == false)
        {
            Anim.enabled = true;
        }
        ragdollBehavior.ToggleRagdoll(false);
        //StopCoroutine(HurtAnimation());
    }

    void Death()
	{
        StopCoroutine("HurtAnimation");
        isDead = true;
        Anim.enabled = false;
        ragdollBehavior.ToggleRagdoll(true);
        enemyAI.enabled = false;
		Destroy (gameObject, 4f);
	}

}
