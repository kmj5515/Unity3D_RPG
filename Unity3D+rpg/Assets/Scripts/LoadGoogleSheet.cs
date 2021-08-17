using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int ID;
    public string Type;
    public int value;
}

public class LoadGoogleSheet : MonoBehaviour
{
    public int LoadedCount { get; private set; }
    public int ListCount { get; private set; }

    public bool isLoad = false;
    public List<PlayerData> p_Info = new List<PlayerData>();
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        UnityGoogleSheet.LoadFromGoogle<int, GameBalance.Data>((list, map) =>
        {
            ListCount = list.Count;
            foreach (var value in list)
            {
                //Debug.Log(value.TypeValue + " " + value.intValue); 
            }

            for (int i = 0; i < list.Count; i++)
			{
                p_Info.Add(new PlayerData());
                p_Info[i].ID = list[i].index;
                p_Info[i].Type = list[i].TypeValue;
                p_Info[i].value = list[i].intValue;

                LoadedCount++;
            }
        }, false);
        isLoad = true;
    }
}
