using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private TypeValue typeValue;

    void Awake()
    {
        typeValue = GameObject.FindWithTag("PlayerManager").GetComponent<TypeValue>();
    }
    void OnTriggerEnter(Collider other)//當進入範圍的物件為主角///只後要做範圍的距離判斷，改變敵人攻擊方式
    {
        if (other.tag == "Enemy")
        {
           //Time.timeScale = 0.4f;
            enemyHealth = other.GetComponent<EnemyHealth>();
            if(enemyHealth.currentHealth > 0)
            {
                var damage = typeValue.PlayerAtk * Random.Range(0.9f, 1.1f);
                damage = Mathf.Round(damage);
                enemyHealth.Hurt(damage);//敵人的攻擊扣掉主角的防禦，然後＊隨機小數點，就是主角要被扣掉的血
            }
        }
    }

    //void OnTriggerExit(Collider other)//當進入範圍的物件為主角///只後要做範圍的距離判斷，改變敵人攻擊方式
    //{
    //        Time.timeScale = 1f;
    //}
}
