using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIndicator : MonoBehaviour
{
    public GameObject deer;
    public GameObject maxCircle;
    public GameObject patern;

    public bool isFullCircle;
    public float scaleSpeed = 1f;
    float addScale;

    public bool isRotaeCircle;
    public float roateSpeed = 1f;
    float addRotate;

    private DeerAI deerAi;

    // Start is called before the first frame update
    void Start()
    {  
        addScale = 1f;
        addRotate = 1f;

        deerAi = FindObjectOfType<DeerAI>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = deer.transform.position;

        if(isFullCircle)
        {
            if (maxCircle.transform.localScale.x <= addScale)
            {
                //isFullCircle = false;
                addScale = 1f;
                patern.SetActive(false);
            }
            else
            {
                addScale += Time.deltaTime * scaleSpeed;
            }

            transform.localScale = new Vector3(addScale, addScale, addScale);
        }

        if (isRotaeCircle)
        {
            transform.Rotate(Vector3.forward * addRotate);
        }
    }
}
