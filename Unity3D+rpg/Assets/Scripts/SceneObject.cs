using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    public GameObject haveObject;

    void Awake()
	{
        var obj = FindObjectsOfType<Controller>();
        if (obj.Length == 1) { DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }
}
