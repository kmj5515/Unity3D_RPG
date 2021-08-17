using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    Slider slBossHp;
    DeerAI deer;

    public Text bossHpText;

    // Start is called before the first frame update
    void Start()
    {
        slBossHp = GetComponent<Slider>();
        deer = FindObjectOfType<DeerAI>();
    }

    // Update is called once per frame
    void Update()
    {
        slBossHp.value = deer.Hp / deer.MaxHp;

        bossHpText.text = (deer.Hp + "/" + deer.MaxHp);
    }
}
