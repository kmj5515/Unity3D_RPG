using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMp : MonoBehaviour
{
    Slider slMp;
    Controller controller;

    public Text playerMpText;

    // Start is called before the first frame update
    void Start()
    {
        slMp = GetComponent<Slider>();
        controller = FindObjectOfType<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        slMp.value = controller.Mp / controller.MaxMp;

        playerMpText.text = (controller.Mp + "/" + controller.MaxMp);
    }
}
