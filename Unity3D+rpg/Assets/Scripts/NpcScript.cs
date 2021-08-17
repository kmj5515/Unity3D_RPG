using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    public GameObject npcText;

    private bool inNpc;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        npcText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && inNpc)
        {
            Debug.Log("�����غ� �Ϸ�");
            player.GetComponent<Controller>().isChat = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NPC�� �����ƴ�.");
            npcText.SetActive(true);
            inNpc = true;
            StartCoroutine("TextCool");
        }        
    }

    IEnumerator TextCool()
    {
        yield return new WaitForSeconds(1f);
        npcText.SetActive(false);
        inNpc = false;
    }
}
