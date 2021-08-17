using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerCinema : MonoBehaviour
{
    public CinemachineVirtualCamera cinemaCam;

    bool isCinema;
    // Start is called before the first frame update
    void Start()
    {
        cinemaCam.Priority = 11;

        cinemaCam = GetComponent<CinemachineVirtualCamera>();

        isCinema = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCinema)
        {
            StartCoroutine("Cinema");
        }
        else
        {
            cinemaCam.Priority = 9;
        }    
    }

    IEnumerator Cinema()
    {
        cinemaCam.Priority = 11;
        yield return new WaitForSeconds(7f);
        isCinema = false;
    }
}
