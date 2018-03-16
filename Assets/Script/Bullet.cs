using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private TypeValue typeValue;
    private Health health;
	private float BulletAtk = 10;

    //audio
    private AudioSource audioSource;
    public AudioClip FreshHit;


    void Awake()
	{
        typeValue = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<TypeValue>();
        audioSource = GetComponent<AudioSource>();
        Destroy(this.gameObject, 5f);
    }

	void OnCollisionEnter(Collision Target)
	{
		if (Target.transform.tag == "Player")
		{
            health = Target.gameObject.GetComponent<Health>();
            
            if(health.currentHealth > 0)
			{//當主角的還有血量時
                var damage = (BulletAtk - typeValue.PlayerDef) * Random.Range(0.9f, 1.1f);
                damage = Mathf.RoundToInt(damage);
                audioSource.PlayOneShot(FreshHit);
                health.Hurt(damage);//敵人的攻擊扣掉主角的防禦，然後＊隨機小數點，就是主角要被扣掉的血
                DeleteBullet();
            }
		}
	}

     void DeleteBullet()
    {
        Destroy(this.gameObject, 0.01f);
    }
}
