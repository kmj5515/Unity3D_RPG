using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AttackType { None, Normal, Shield, Ultimate } 

public class Controller : LivingEntity
{
    private Behavior behavior; // Ä³ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½àµ¿ ï¿½ï¿½Å©ï¿½ï¿½Æ®
    private Camera mainCamera; // ï¿½ï¿½ï¿½ï¿½ Ä«ï¿½Þ¶ï¿½
   
    private Vector3 targetPos; // Ä³ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ìµï¿½ Å¸ï¿½ï¿½ ï¿½ï¿½Ä¡

    private float maxDistance = 10000f;

    private bool moveSkill_On;
    private bool ultimate_On;
    private bool shield_On;
    private bool attack_On;

    private Rigidbody rb;
    private bool IsHit;

    public Image[] image;

    public ParticleSystem sword_limit;
    public AudioSource limit_Audio;

    public ParticleSystem sword_attack1;
    public AudioSource attack1_Audio;

    public ParticleSystem sword_shield;
    public AudioSource shield_Audio;

    public AudioSource moveSkill_Audio;

    public CameraControll cameraControll;

    public GameObject limitObject;
    public GameObject shieldObject;
    public GameObject attackObject;



    AttackType attackType;
    public int AttackDamage { get; private set; }

    public float Mp;
    public float MaxMp;

    public bool isChat;

    void Start()
    {
        CleanPos();

        var obj = FindObjectsOfType<Controller>();
        if (obj.Length == 1) { DontDestroyOnLoad(gameObject) ; }
        else { Destroy(gameObject); }

        behavior = GetComponent<Behavior>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        moveSkill_On = true;
        ultimate_On = true;
        shield_On = true;
        attack_On = true;

        attackType = AttackType.None;
        AttackDamage = 0;

        rb = GetComponent<Rigidbody>();
        IsHit = false;

        Hp = 1000;
        MaxHp = 1000;
        Mp = 1000;
        MaxMp = 1000;

        isChat = false;
    }

    void Update()
    {
        //if (!isChat)
        //{
        //    ControllPlayer();
        //}

        ControllPlayer();

        if (Input.GetKeyDown(KeyCode.N))
        {
            IsHit = true;
        }

        if (IsHit)  //?‰ë°± ?ŒìŠ¤??
        {
            rb.AddForce(-transform.forward * 20f, ForceMode.Impulse);
            StartCoroutine("Hit_CoolTime");
        }

        if (Mp >= MaxMp)
        {
            Mp = MaxMp;
        }
        if (Mp <= 0)
        {
            Mp = 0;
        }
    }

    public void CleanPos()
	{
        targetPos = transform.position;
    }

    void Move()
	{
        int layerMask = 3 << LayerMask.NameToLayer("Ground");

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            targetPos = hit.point;
        }
    }

    void ControllPlayer()
    {
        if (Input.GetMouseButtonDown(0) && attack_On)    // ÀÏ¹Ý °ø°Ý
        {
            attackType = AttackType.Normal;

            attackObject.GetComponent<BoxCollider>().enabled = true;

            Mp += 50;
            attack_On = false;
            behavior.Turn(targetPos);
            //transform.Rotate(targetPos);
            behavior.Attack1(true);
            sword_attack1.Play();
            attack1_Audio.Play();

            StartCoroutine("attack_CoolTime");
        }
        else if (Input.GetKeyDown(KeyCode.A) && shield_On)   // counter
        {
            attackType = AttackType.Shield;

            shieldObject.GetComponent<BoxCollider>().enabled = true;

            Mp -= 50;
            shield_On = false;
            behavior.Defend(true);
            sword_shield.Play();
            shield_Audio.Play();

            StartCoroutine("ShieldSkill_CoolTime", (5f));
            StartCoroutine("ShieldSkillOn");
        }
        else if (Input.GetKeyDown(KeyCode.S) && ultimate_On)   // ultimate
        {
            attackType = AttackType.Ultimate;

            Mp -= 800;
            ultimate_On = false;
            limit_Audio.Play();
            cameraControll.isShake = true;

            StartCoroutine("Ultimate");
            StartCoroutine("Ultimate_shake");
            StartCoroutine("Ultimate_CoolTime", (30f));
            StartCoroutine("UltimateOn");
        }
        else if (Input.GetKey(KeyCode.D))   // ultimate
        {     
            StartCoroutine("Die");          
        }
        else if (Input.GetKeyDown(KeyCode.Space) && moveSkill_On)   // ï¿½Ìµï¿½ ï¿½ï¿½Å³
        {
            behavior.accelSpeed = 8.0f;
            moveSkill_Audio.Play();
            StartCoroutine("MoveSkill");
            moveSkill_On = false;
            StartCoroutine("MoveSkill_CoolTime", (3f));
        }
        else if (Input.GetMouseButton(1))    // ï¿½Ìµï¿½
        {
            Move();
        }
        else
        {
            behavior.Idle();
            behavior.Attack1(false);
            behavior.Attack2(false);
            behavior.Defend(false);

            attackObject.GetComponent<BoxCollider>().enabled = false;
            limitObject.GetComponent<BoxCollider>().enabled = false;
            shieldObject.GetComponent<BoxCollider>().enabled = false;
        }

        // °ø°Ý ÀÔ·Â µ¥ÀÌÅÍ
        SetAttackData(attackType);

        if (behavior.Run(targetPos))
        {
            behavior.Turn(targetPos);
        }
        else
        {
            //behavior.Idle();
        }
    }

    void SetAttackData(AttackType type)
    {
        switch(type)
        {
            case AttackType.Normal:
                AttackDamage = 100;         
                break;
            case AttackType.Shield:
                AttackDamage = 5000;
                break;
            case AttackType.Ultimate:
                AttackDamage = 4000;
                break;
        }
    }

    IEnumerator attack_CoolTime()
    {
        yield return new WaitForSeconds(0.5f);
        attack_On = true;
    }

    IEnumerator MoveSkill()
	{
        yield return new WaitForSeconds(0.1f);
        behavior.accelSpeed = 1.0f;
    }

    IEnumerator MoveSkill_CoolTime(float cool)
	{
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            image[2].fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }
        moveSkill_On = true;
    }

    IEnumerator ShieldSkillOn()
    {
        yield return new WaitForSeconds(5f);
        shield_On = true;
    }

	IEnumerator ShieldSkill_CoolTime(float cool)
	{
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            image[1].fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator Ultimate_shake()
    {
        yield return new WaitForSeconds(5f);
        cameraControll.isShake = false;
    }

    IEnumerator Ultimate()
    {
        yield return new WaitForSeconds(4.5f);
        sword_limit.Play();
        limitObject.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator UltimateOn()
    {
        yield return new WaitForSeconds(10f);
        ultimate_On = true;
    }

    IEnumerator Ultimate_CoolTime(float cool)
    {
        sword_limit.Stop();
        
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            image[0].fillAmount = (1.0f / cool);            
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Hit_CoolTime()
    {
        yield return new WaitForEndOfFrame();
        IsHit = false;
        CleanPos();
    }

    IEnumerator Die()
    {
        behavior.Die();
        yield return new WaitForSeconds(7f); 
    }
}