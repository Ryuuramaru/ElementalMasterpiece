using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemaining : MonoBehaviour
{

    public Slider slider;
    public GameObject PlayerCamera;
   
    void Start()
    {
        slider.maxValue = PlayerCamera.GetComponent<TimeWarp>().maxDuration;
        slider.value = slider.maxValue;
    }


    void FixedUpdate()
    {
        slider.value = PlayerCamera.GetComponent<TimeWarp>().availableTime;
    }
}
