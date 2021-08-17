using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
    DeerAI deer;

    BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        deer = FindObjectOfType<DeerAI>();
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LivingEntity>().OnDamage(deer.BossAttackDamage);
        }
    }
}
