﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    //使用其他腳本函數
    private EnemyShoot enemyShoot;
    //狀態變數設定
    public enum EnemyState
    {
        Enemy_Idle = 0,
        Enemy_Walk,
        Enemy_Rotation,
        Enemy_Catch,
        Enemy_NormalAttack,
        Enemy_Shooting
    }
    EnemyState enemyState;
    private float EnemyThinkTime = 3f;//思考時間
    private float timer = 0;//變換狀態時間
    private int NewState;//思考後的新狀態
    private bool isThink;
    private NavMeshAgent nav;
    //判斷主角
    private GameObject Target;
    private GameObject[] WolfGuardTarget;
    private Vector3 EnemyLookRotate;
    private float EnemyToPlayerDis;//主角跟敵人的距離
    //巡邏範圍計算變數
    private Vector3 MovePoint;//要移動的位置
    private Vector3 OriginPoint;//敵人最初位置
    private float PatorlRadius = 15f;//巡邏半徑
    //動作宣告
    private Animator animator;
    private float animSpeed;


    void Awake()
    {
        enemyShoot = GetComponentInChildren<EnemyShoot>();
        enemyState = EnemyState.Enemy_Idle;//敵人最初狀態
        isThink = true;
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = false;
       
        animator = GetComponent<Animator>();
        //nav.updateRotation = false;
    }
    private void Start()
    {
        nav.enabled = true;
        OriginPoint = transform.position;//敵人最初的位置
    }
    void Update()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
        }

        if (GameObject.FindGameObjectWithTag("WolfGuard"))
        {
            WolfGuardTarget = GameObject.FindGameObjectsWithTag("WolfGuard");
        }
        else
            WolfGuardTarget = null;
        ThinkState();
        if (Target.tag != "Pillar")
            TargetInRange();
        else if(!isThink)
            isThink = true;
        if (nav.remainingDistance < nav.stoppingDistance) //如果移動位置小於停止位置，不跑步
        {
            nav.isStopped = true;
            animSpeed = nav.desiredVelocity.sqrMagnitude;
            animator.SetFloat("Speed", animSpeed);
        }
        else
        {
            nav.isStopped = false;
            animSpeed = nav.desiredVelocity.sqrMagnitude;
            animator.SetFloat("Speed", animSpeed);
        }
    }

    void ThinkState()
    {
        if (isThink == true)
        {
            if (Time.time - timer > EnemyThinkTime)
            {
                NewState = Random.Range(0, 2);
                timer = Time.time;
                switch (NewState)
                {
                    case 0:
                        SetEnemyState(EnemyState.Enemy_Idle);
                        break;
                    case 1:
                        SetEnemyState(EnemyState.Enemy_Walk);
                        break;
                    //case 2:
                    //    SetEnemyState(EnemyState.Enemy_Rotation);
                    //    break;
                }
            }
        }
    }

    Vector3 PatrolPoint()//巡邏半徑內，隨機選一個點
    {
        MovePoint = new Vector3(Random.Range(OriginPoint.x - PatorlRadius, OriginPoint.x + PatorlRadius), transform.position.y, Random.Range(OriginPoint.z - PatorlRadius, OriginPoint.z + PatorlRadius));
        return MovePoint;
    }
    
    private void TargetInRange()
    {
        //Debug.Log(Target);
        if (WolfGuardTarget == null && GameObject.FindGameObjectWithTag("Player"))//如果沒有召喚狼，敵人攻擊目標是主角
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            EnemyToPlayerDis = Vector3.Distance(transform.position, Target.transform.position);//去判斷跟主角的範圍
        }
        else if (WolfGuardTarget !=  null && Vector3.Distance(transform.position, Target.transform.position) > 3)//如果有召喚狼，並且上個目標跟敵人距離大於3時，去判斷誰距離敵人最近，並且攻擊
        {
            float ShortestRange;//敵人跟目標最短距離
            GameObject[] ChooseTarget = { GameObject.FindGameObjectWithTag("Player"), WolfGuardTarget[0], WolfGuardTarget[1] };//抓取主角與召喚狼

            for (int i=0; i < ChooseTarget.Length-1; i++)//用迴圈去判斷敵人與三個目標的距離
            {
                if ((Vector3.Distance(transform.position, ChooseTarget[i].transform.position) < Vector3.Distance(transform.position, ChooseTarget[i+1].transform.position)))
                {//第一個目標與第二個目標判斷最小距離，當一小於二
                    ShortestRange = Vector3.Distance(transform.position, ChooseTarget[i].transform.position);
                    Target = ChooseTarget[i];//將敵人攻擊目標轉成一
                    ChooseTarget[i + 1] = ChooseTarget[i];//將目標二變成目標一，之後在跟目標三比
                    EnemyToPlayerDis = ShortestRange;
                }
                else//將敵人攻擊目標轉成二，之後繼續迴圈跟下個目標距離比
                {
                    ShortestRange = Vector3.Distance(transform.position, ChooseTarget[i+1].transform.position);
                    Target = ChooseTarget[i+1];
                    EnemyToPlayerDis = ShortestRange;
                }
            }
        }
        
        if (EnemyToPlayerDis <= 10 && Health.isDead == false)
        {
            EnemyLookRotate = new Vector3((Target.transform.position.x - transform.position.x), 0, (Target.transform.position.z - transform.position.z));//Turn to Target
            transform.rotation = Quaternion.LookRotation(EnemyLookRotate);
            isThink = false;//Stop think

            if (EnemyToPlayerDis <= 8 )//小於8大於3的距離射擊
            {
                SetEnemyState(EnemyState.Enemy_Shooting);
            }
            //else if (EnemyToPlayerDis <= 2)//距離小於3  普通攻擊
            //{
            //    SetEnemyState(EnemyState.Enemy_NormalAttack);//距離在10內追擊主角
            //}
            else
            {
                SetEnemyState(EnemyState.Enemy_Catch);
            }
        }
        else//當距離大於10，敵人重新思考
        {
            isThink = true;
            if (enemyState == EnemyState.Enemy_Catch || enemyState == EnemyState.Enemy_Shooting || enemyState == EnemyState.Enemy_NormalAttack)
            {
                SetEnemyState(EnemyState.Enemy_Idle);
            }
        }
    }

    void SetEnemyState(EnemyState State)
    {
        enemyState = State;//如果不是就變成思考後的新狀態
        switch (State)
        {
            case EnemyState.Enemy_Idle:
                nav.isStopped = true;
                animator.SetBool("Catch", false);
                break;
            //case EnemyState.Enemy_Rotation:
            //    //Debug.Log("Rotation");
            //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, ((int)Random.Range(1, 4) * 90), 0), Time.deltaTime * 8f);
            //    nav.isStopped = true;
            //    break;
            case EnemyState.Enemy_Walk:
                //isThink = false;
                nav.SetDestination(PatrolPoint());
                nav.isStopped = false;
                animator.SetBool("Catch", false);

                break;
            case EnemyState.Enemy_Catch:
                nav.isStopped = false;
                animator.SetBool("Catch",true);
                nav.SetDestination(Target.transform.position);
                break;
            case EnemyState.Enemy_NormalAttack:
                animator.SetBool("Catch", false);
                nav.isStopped = true;
                //Start attack animation
                break;
            case EnemyState.Enemy_Shooting:
                animator.SetBool("Catch", false);
                //start shoot animation
                if (Time.time - timer > 3f)
                {
                    animator.SetTrigger("Shoot");
                    enemyShoot.Shooting(Target.transform);
                    
                    timer = Time.time;
                }
                break;
        }
    }
}