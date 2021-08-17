using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCamer : MonoBehaviour
{
    public GameObject haveObject;

    void Awake()
    {
        var obj = FindObjectsOfType<Controller>();
        if (obj.Length == 2) { DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }
}
