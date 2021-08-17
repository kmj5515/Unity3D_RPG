using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float maxHp;
    public float hp;
    public float maxMp;
    public float mp;
    LoadGoogleSheet info;
    List<PlayerData> p_Info;

    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("DataBase").GetComponent<LoadGoogleSheet>();
        p_Info = info.p_Info;
        Debug.Log("p_Info : " + p_Info.Count);

        //maxHp = p_Info[0].value;
        //hp = p_Info[0].value;
        //maxMp = p_Info[1].value;
        //mp = p_Info[1].value;
    }
}
