using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_State
{ 
    Idle, Walk, Attack_Horn1, Attack_Horn2, Attack_Leg, Attack_Legs, AttackCircle, Hit, Die
}

public class DeerAI : LivingEntity
{
    CircleIndicator circle;

    public GameObject circlePatern;
    public GameObject pizzaPatern;
    
    public GameObject nvAgent;

    public GameObject Head;

    public GameObject TownPotal;

    Animator anim;
    dest destnevi;
    GameObject player;
    public GameObject deer;

    [SerializeField]
    AI_State state;

    // hp구간
    //public float bossHp;
    //public float bossMaxHp;
   
    bool isDie;

    public int randomPatern;

    public GameObject legAttack1;
    public GameObject legAttack2;

    public int BossAttackDamage { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<CircleIndicator>();

        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        destnevi = FindObjectOfType<dest>();

        state = AI_State.Idle;
        ChangeState(state);

        isDie = false;

        Hp = 10000;
        MaxHp = 10000;

    }

    // Update is called once per frame
    void Update()
    {
        Head.transform.position = nvAgent.transform.position;     // 임시 고정
        // 매 프레임 실행되는 코드 TEST
       switch (state)
       {
           case AI_State.Idle: UpdateIdle(); break;
           case AI_State.Walk: UpdateWalk(); break;        
           case AI_State.Attack_Horn1: UpdateAttack_Horn1(); break;
           case AI_State.Attack_Horn2: UpdateAttack_Horn2(); break;
           case AI_State.Attack_Leg: UpdateAttack_Leg(); break;
           case AI_State.Attack_Legs: UpdateAttack_Legs(); break;
           case AI_State.AttackCircle: UpdateAttackCircle(); break;
           case AI_State.Hit: UpdateHit(); break;
           case AI_State.Die: UpdateDie(); break;
        }

        if (Hp <= 0)
        {
            Hp = 0;
        }
    }

    void UpdateIdle()
    {
        if (Hp <= 0)
        {
            isDie = true;
        }
    }

    void UpdateWalk()
    {
        if (destnevi.dist <= 4f)
        {
            ChangeState(AI_State.Idle);
        }
    }
    void UpdateAttack_Horn1()
    {

    }
    void UpdateAttack_Horn2()
    {

    }
    void UpdateAttack_Leg()
    {
        
    }
    void UpdateAttack_Legs()
    {
        
    }
    void UpdateAttackCircle()
    {

    }
    void UpdateHit()
    {

    }
    void UpdateDie()
    {

    }

    void ChangeState(AI_State nextState)
    {
        state = nextState;

        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsHornAttack1", false);
        anim.SetBool("IsHornAttack2", false);
        anim.SetBool("IsLegAttack", false);
        anim.SetBool("IsLegsAttack", false);
        anim.SetBool("IsCirclePattern", false);
        anim.SetBool("IsCounter", false);
        anim.SetBool("IsDead", false);

        StopAllCoroutines();
        destnevi.isTraceNevi = false;
        
        switch (state)
        {
            case AI_State.Idle: StartCoroutine(CoroutineIdle()); break;
            case AI_State.Walk: StartCoroutine(CoroutineWalk()); break;
            case AI_State.Attack_Horn1: StartCoroutine(CoroutineHornAttack1()); break;
            case AI_State.Attack_Horn2: StartCoroutine(CoroutineHornAttack2()); break;
            case AI_State.Attack_Leg: StartCoroutine(CoroutineAttack_Leg()); break;
            case AI_State.Attack_Legs: StartCoroutine(CoroutineAttack_Legs()); break;
            case AI_State.AttackCircle: StartCoroutine(CoroutineAttackCircle()); break;
            case AI_State.Hit: StartCoroutine(CoroutineHit()); break;
            case AI_State.Die: StartCoroutine(CoroutineDie()); break;
        }

    }

    IEnumerator CoroutineIdle()
	{
        anim.SetBool("IsIdle", true);
 
        while (true)
		{
            yield return new WaitForSeconds(4f);

            // 일정 시간마다 실행되는 코드
            if (!isDie)
            {               
                if (destnevi.dist >= 5f)
                {
                    ChangeState(AI_State.Walk);
                }
                else
                {
                    randomPatern = Random.Range(2, 7);

                    switch (randomPatern)
                    {
                        case 2:
                            ChangeState(AI_State.Attack_Horn1);
                            BossAttackDamage = 50;
                            break;
                        case 3:
                            ChangeState(AI_State.Attack_Horn2);
                            BossAttackDamage = 50;
                            break;
                        case 4:
                            ChangeState(AI_State.Attack_Leg);
                            BossAttackDamage = 20;
                            break;
                        case 5:
                            ChangeState(AI_State.Attack_Legs);
                            BossAttackDamage = 80;
                            break;
                        case 6:
                            ChangeState(AI_State.AttackCircle);
                            BossAttackDamage = 100;
                            break;
                    }
                }              
            }
            else
            {
                ChangeState(AI_State.Die);
            }      
            yield break;
        }
    }
    IEnumerator CoroutineWalk()
    {
        anim.SetBool("IsWalk", true);
        destnevi.isTraceNevi = true;

        while (true)
        {
            yield return new WaitForSeconds(5f);
        
            ChangeState(AI_State.Idle);
            yield break;
        }
    }
    IEnumerator CoroutineHornAttack1()
    {
        anim.SetBool("IsHornAttack1", true);

        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            ChangeState(AI_State.Idle);
            yield break;
        }
    }
    IEnumerator CoroutineHornAttack2()
    {
        anim.SetBool("IsHornAttack2", true);

        if (Hp <= (MaxHp / 2f)) // 피자패턴 추가
        {
            pizzaPatern.SetActive(true);
        }
        
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            ChangeState(AI_State.Idle);
            yield break;
        }
    }
    IEnumerator CoroutineAttack_Leg()
    {
        legAttack1.GetComponent<BoxCollider>().enabled = true;

        anim.SetBool("IsLegAttack", true);

        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            legAttack1.GetComponent<BoxCollider>().enabled = false;
            ChangeState(AI_State.Idle);
            yield break;
        }
    }
    IEnumerator CoroutineAttack_Legs()
    {
        legAttack2.GetComponent<BoxCollider>().enabled = true;

        anim.SetBool("IsLegsAttack", true);

        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            legAttack2.GetComponent<BoxCollider>().enabled = false;
            ChangeState(AI_State.Idle);
            yield break;
        }
    }
    IEnumerator CoroutineAttackCircle()
    {
        anim.SetBool("IsCirclePattern", true);
        circlePatern.SetActive(true);

        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            ChangeState(AI_State.Idle);
            yield break;
        }
    }
    IEnumerator CoroutineHit()
    {
        anim.SetBool("IsCounter", true);

        while (true)
        {
            yield return new WaitForSeconds(10.0f);

            ChangeState(AI_State.Idle);
            yield break;
        }
    }
    IEnumerator CoroutineDie()
    {
        anim.SetBool("IsDead", true);

        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            Destroy(deer);
            TownPotal.SetActive(true);
            yield break;
        }
    }

    public override void OnDamage(float damage)//, Vector3 hitPoint, Vector3 hitDirection)
    {
        base.OnDamage(damage);//, hitPoint, hitDirection);
    }

}
