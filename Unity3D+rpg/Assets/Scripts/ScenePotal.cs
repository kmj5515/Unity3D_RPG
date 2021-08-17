using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenePotal : MonoBehaviour
{
    public Behavior player;

    public string bgmName = "";
    private GameObject camObject;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Behavior>();

        camObject = GameObject.Find("Main Camera");
        //camObject.GetComponent<BackGroundMusic>().PlayBGM("town");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
	{
        if (other.name.Equals("Player"))
        {
			if (SceneManager.GetActiveScene().name == "TownScene")
			{
                LoadingSceneMgr.LoadScene("BossScene");
                camObject.GetComponent<BackGroundMusic>().PlayBGM("boss");
                text.text = "아르고스 신전";
            }
			if (SceneManager.GetActiveScene().name == "BossScene")
			{
                LoadingSceneMgr.LoadScene("TownScene");
                camObject.GetComponent<BackGroundMusic>().PlayBGM("town");
                text.text = "로아 마을";
            }
        }
    }
}
