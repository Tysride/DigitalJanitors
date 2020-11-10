using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    public TextMeshPro hour;

    void Start()
    {
        Invoke("Set10", 60f);
        Invoke("Set11", 120f);
        Invoke("Set12", 180f);
        Invoke("Set1", 240f);
        Invoke("Set2", 300f);
        Invoke("Set3", 360f);
        Invoke("Set4", 420f);
        Invoke("Set5", 480f);
        Invoke("Set6", 540f);
        Invoke("Set7", 600f);


        hour = GetComponent<TextMeshPro>();
        hour.text = "9:00 AM";

    }
    void Set10()
    {
        hour.text = "10:00 AM";
    }
    void Set11()
    {
        hour.text = "11:00 AM";
    }
    void Set12()
    {
        hour.text = "12:00 AM";
    }
    void Set1()
    {
        hour.text = "1:00 PM";
    }
    void Set2()
    {
        hour.text = "2:00 PM";
    }
    void Set3()
    {
        hour.text = "3:00 PM";
    }
    void Set4()
    {
        hour.text = "4:00 PM";
    }
    void Set5()
    {
        hour.text = "5:00 PM";
    }
    void Set6()
    {
        hour.text = "6:00 PM";
    }
    void Set7()
    {
        hour.text = "7:00 PM";
    }
}
