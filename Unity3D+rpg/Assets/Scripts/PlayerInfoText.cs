using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoText : MonoBehaviour
{
	LoadGoogleSheet info;
	List<PlayerData> p_Info;

	public Text infoText;

	int i = 0;

	private void Start()
	{
		info = GameObject.Find("DataBase").GetComponent<LoadGoogleSheet>();
		p_Info = info.p_Info;
	}

	// Update is called once per frame
	void Update()
    {
		if (i < p_Info.Count)
        {
			infoText.text += p_Info[i].Type + " : " + p_Info[i].value + "\n";
			i++;
		}
	}
}
