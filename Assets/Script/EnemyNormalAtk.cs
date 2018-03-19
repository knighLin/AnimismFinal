using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalAtk : MonoBehaviour
{
    //call other class
    private Health Health;
    private TypeValue value;

    //private GameObject gameManager;
    private GameObject playerManager;
    private GameObject Player;//Find Atk Target

    //Animator
    public Animator animator;
    public float timeBetweenAttacks = 1f;//敵人攻擊的時間間距
    public int EnemyAtk = 10;//敵人攻擊力
    public Collider weaponCollider;
    Vector3 HitPoint;


    void Awake()
    {
        //set class var
        value = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<TypeValue>();
        //set Animator
        animator = GetComponent<Animator>();
        //set WeaponCollider
        weaponCollider.enabled = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), weaponCollider);//讓兩個物體不會產生碰撞
    }

    void OnTriggerEnter(Collider other)//當進入範圍的物件為主角///只後要做範圍的距離判斷，改變敵人攻擊方式
    {
        if (other.tag == "Player")
        {
             HitPoint = Vector3.Cross(Vector3.Normalize(other.transform.position), Vector3.Normalize(this.gameObject.transform.position));//正面傷害正X軸，背面傷害負X軸，左邊傷害正Z軸，右邊傷害負Z軸
            /*Debug.Log ("Player");
			//playerInRange = true;
			timer += Time.deltaTime;
			//Debug.Log (timer);
			if (timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0) {//主角進入範圍，敵人還有血量
				Debug.Log("AAA");
				Attack ();
				timer = 0;
			}*/

            Health = other.GetComponent<Health>();
            Attack();
        }
    }

    public void Attack()
    {
        if (Health.currentHealth > 0)
        {//當主角的還有血量時
            var damage = (EnemyAtk - value.PlayerDef) * Random.Range(0.9f, 1.1f);
            damage = Mathf.Round(damage);
            Health.Hurt(damage,HitPoint);//敵人的攻擊扣掉主角的防禦，然後＊隨機小數點，就是主角要被扣掉的血
        }
    }
}
//public void OnAttackTrigger()//避免走路時碰到武器，觸發事件，所以只有攻擊時，才開啟觸發
//{
//    weaponCollider.enabled = true;
//}

//public void OnDisableAttackTrigger()
//{
//    weaponCollider.enabled = false;

//}
