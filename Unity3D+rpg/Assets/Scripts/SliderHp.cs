using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHp : MonoBehaviour
{
    Slider slHp;

    Controller controller;

    public Text playerHpText;

    // Start is called before the first frame update
    void Start()
    {
        slHp = GetComponent<Slider>();
        controller = FindObjectOfType<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        slHp.value = controller.Hp / controller.MaxHp;

        playerHpText.text = (controller.Hp + "/" + controller.MaxHp);
    }
}
