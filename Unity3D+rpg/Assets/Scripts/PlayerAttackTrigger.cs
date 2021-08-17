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

    void OnTriggerEnter(Collider other)     // ������ ó��
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

        if (randNum <= p_Critical)  //ũ��Ƽ�ý�
        {
            Debug.Log("ũ��Ƽ�� �ߵ�!");
            return AttackDamage *= 2;
        }
        else
        {
            Debug.Log("ũ��Ƽ�� ����");
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
