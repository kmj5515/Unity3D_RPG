using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgText : MonoBehaviour
{
    public GameObject hubDamageText;
    public Transform hubPos;

    public void TakeDamage(int damage)
    {
        GameObject hubText = Instantiate(hubDamageText);
        hubText.transform.position = hubPos.position;
        hubText.GetComponent<DamageText>().damage = damage;
    }
}
