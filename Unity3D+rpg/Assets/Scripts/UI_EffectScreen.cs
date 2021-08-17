using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EffectScreen : MonoBehaviour
{
    public Controller playerHp;
    public Image screenImg;

    int effectR;
    int effectG;
    int effectB;
    float effectA;

    bool UpAndDown;

    // Start is called before the first frame update
    void Start()
    {
        playerHp = FindObjectOfType<Controller>();

        GameObject.Find("PrevColor").GetComponent<Image>().color =
            new Color(effectR / 255, effectG / 255, effectB / 255, effectA / 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHp.Hp <= 300f && playerHp.Hp >= 0.1f)   // Emergency
        {
            effectR = 255; effectG = 0; effectB = 0; //effectA = 60;

            if (!UpAndDown)
            {
                effectA -= 0.1f;

                if (effectA <= 10f)
                {
                    UpAndDown = true;
                }
            }
            else
            {
                effectA += 0.1f;

                if (effectA >= 80f)
                {
                    UpAndDown = false;
                }
            }
        }
        else if (playerHp.Hp == 0f)    // Dead
        {
            effectR = 120; 
            effectG = 120; 
            effectB = 120;
            effectA = 150;
        }
        else
        {
            effectA = 0f;
        }

        GameObject.Find("PrevColor").GetComponent<Image>().color =
                new Color(effectR / 255, effectG / 255, effectB / 255, effectA / 255);
    }
}
