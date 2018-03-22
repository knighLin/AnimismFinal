using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
    }

    void OnCollisionEnter(Collision Target)
    {
        if (Target.transform.tag == "Player")
        {
            //Debug.Log(Vector3.Cross(Vector3.Normalize(Target.transform.forward), Vector3.Normalize(this.gameObject.transform.forward)));
            // Vector3 HitPoint = Vector3.Cross(Vector3.Normalize(Target.transform.position), Vector3.Normalize(this.gameObject.transform.position));//正面傷害正X軸，背面傷害負X軸，左邊傷害正Z軸，右邊傷害負Z軸
            //Vector3 toOther = (Target.transform.position - transform.position).normalized;
            //Debug.Log("ToPine"+toOther);
            var dot = Vector3.Dot(Target.transform.forward.normalized, transform.forward.normalized);
            //Debug.Log(dot);
            health = Target.gameObject.GetComponent<Health>();

            if (health.currentHealth > 0)
            {//當主角的還有血量時
                var damage = (BulletAtk - typeValue.PlayerDef) * Random.Range(0.9f, 1.1f);
                damage = Mathf.RoundToInt(damage);
                audioSource.PlayOneShot(FreshHit);
                health.Hurt(damage, dot);//敵人的攻擊扣掉主角的防禦，然後＊隨機小數點，就是主角要被扣掉的血 
            }
        }
        Destroy(gameObject);
    }

}
