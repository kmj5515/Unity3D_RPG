using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    Controller pCtrl;

    BoxCollider boxCollider;

    LoadGoogleSheet info;
    List<PlayerData> p_Info;
    public int p_Critical;
    public int countDamage;

    public GameObject hubDamageText;
    public Transform hubPos;

    public DmgText textPos;

    private void Start()
    {
        pCtrl = FindObjectOfType<Controller>();
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.enabled = false;

        info = GameObject.Find("DataBase").GetComponent<LoadGoogleSheet>();
        p_Info = info.p_Info;

        textPos = FindObjectOfType<DmgText>();
    }

    void OnTriggerEnter(Collider other)     // 데미지 처리
    {
        if (other.CompareTag("Boss"))
        {
            countDamage = SetCriticalAttack(pCtrl.AttackDamage);
            other.GetComponent<LivingEntity>().OnDamage(countDamage);
            textPos.TakeDamage(countDamage);
            Debug.Log(SetCriticalAttack(countDamage));
        }
    }

    int SetCriticalAttack(int AttackDamage)
    {
        p_Critical = p_Info[4].value;
        int randNum = Random.Range(0, 100);

        if (randNum <= p_Critical)  //크리티컬시
        {
            Debug.Log("크리티컬 발동!");
            return AttackDamage *= 2;
        }
        else
        {
            Debug.Log("크리티컬 못함");
            return AttackDamage;
        }
    }

    //public void TakeDamage(int damage)
    //{
    //    GameObject hubText = Instantiate(hubDamageText);
    //    hubText.transform.position = hubPos.position;
    //    hubText.GetComponent<DamageText>().damage = damage;
    //}
}
