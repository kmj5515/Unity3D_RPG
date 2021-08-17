using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public float Hp { get; protected set; }
    public float MaxHp { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public virtual void OnDamage(float damage)//, Vector3 hitPoint, Vector3 hitDirection)
    {
        Hp -= damage;
    }
}
