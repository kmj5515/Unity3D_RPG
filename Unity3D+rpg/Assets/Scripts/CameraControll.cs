using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.Experimental.GraphView;

public class CameraControll : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    public float noise;
    public bool isShake;

    private void Start()
    {
        noise = 0.0f;
        isShake = false;

        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = noise;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShake)
        {
            noise += Time.deltaTime;
        }
        else
        {
            noise = 0f;
        }
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = noise;

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput > 0)     //마우????조작
        {

        }
        else if (wheelInput < 0)
        {

        }
    }
}

