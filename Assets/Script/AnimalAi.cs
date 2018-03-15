using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAi : MonoBehaviour
{

    public enum State
    {
        Idle = 0,
        Walk,
        Sleep,
        laying,
        Seat
    }
    State m_State;
    private float ThinkTime = 10f;//思考時間
    private float timer = 0;//變換狀態時間
    private State LastState;
    private int NewState;//思考後的新狀態
    private Animator animator;
    private NavMeshAgent nav;
    float m_TurnAmount;//轉向值
    private void Awake()
    {
        m_State = State.Idle;//敵人最初狀態
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        //if(this.tag == "WolfMaster")
        //{
            
        //}
    }
    // Update is called once per frame
    void Update()
    {
        ThinkState();
    }

    int Render()
    {
        int RenderCount = Random.Range(0, 4);
        return RenderCount;
    }

    void ThinkState()
    {
        if (Time.time - timer > ThinkTime)
        {
            NewState = Random.Range(0, 5);
            timer = Time.time;
            switch (NewState)
            {
                case 0:
                    SetEnemyState(State.Idle);
                    break;
                case 1:
                    SetEnemyState(State.Walk);
                    break;
                case 2:
                    SetEnemyState(State.Sleep);
                    break;
                case 3:
                    SetEnemyState(State.laying);
                    break;
                case 4:
                    SetEnemyState(State.Seat);
                    break;
            }
        }
    }
    void SetEnemyState(State State)
    {
        m_State = State;//如果不是就變成思考後的新狀態
        switch (State)
        {
            case State.Idle:
                nav.isStopped = true;
                animator.SetBool("Idle", true);
                animator.SetInteger("IdleCount", Render());
                break;
            case State.Walk:
                animator.SetBool("Idle", false);
                animator.SetBool("Sleep", false);
                animator.SetBool("Seat", false);
                animator.SetBool("Laying", false);
                nav.isStopped = false;
                animator.SetFloat("Speed", nav.desiredVelocity.sqrMagnitude, 0.1f, Time.deltaTime);
                animator.SetFloat("Turn", nav.angularSpeed, 0.1f, Time.deltaTime);
                break;
            case State.Sleep:
                nav.isStopped = true;
                animator.SetBool("Idle", false);
                animator.SetBool("Seat", false);
                animator.SetBool("Laying", false);
                animator.SetBool("Sleep", true);
                break;
            case State.laying:
                nav.isStopped = true;
                animator.SetBool("Idle", false);
                animator.SetBool("Seat", false);
                animator.SetBool("Sleep", false);
                animator.SetBool("Laying", true);
                break;
            case State.Seat:
                nav.isStopped = true;
                animator.SetBool("Idle", false);
                animator.SetBool("Sleep", false);
                animator.SetBool("Laying", false);
                if (LastState == State.Sleep)
                    animator.SetTrigger("SleepToSeat");
                if(LastState == State.laying)
                    animator.SetTrigger("LayingToSeat");
                animator.SetBool("Seat", true);
                break;
        }
        LastState = State;
    }
}
