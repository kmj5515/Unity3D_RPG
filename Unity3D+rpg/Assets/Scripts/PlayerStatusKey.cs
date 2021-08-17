using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusKey : MonoBehaviour
{
    public GameObject playerUI;
    bool isStatus;

    // Start is called before the first frame update
    void Start()
    {
        isStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isStatus)
            {
                playerUI.SetActive(true);
                isStatus = true;
            }
            else
            {
                playerUI.SetActive(false);
                isStatus = false;
            }
        }
    }
}
