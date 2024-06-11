using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightSignal : MonoBehaviour
{
    public GameObject LightGreen;
    public GameObject LightOrange;
    public GameObject LightRed;
    float Traffictime=0f;

    public float GreenLightTime ;
    public float OrangeLightTime ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Traffictime += Time.deltaTime;
        if (Traffictime > 20f) Traffictime = 0f;
        TrafficLightControle();

    }
    void TrafficLightControle()
    {
        //MainHighway traffic signal
        if (Traffictime < GreenLightTime)
        {
            LightGreen.SetActive(true);
            LightOrange.SetActive(false);
            LightRed.SetActive(false);
        }
        else if(Traffictime>=GreenLightTime && Traffictime < OrangeLightTime)
        {
            LightGreen.SetActive(false);
            LightOrange.SetActive(true);
            LightRed.SetActive(false);
        }
        else
        {
            LightGreen.SetActive(false);
            LightOrange.SetActive(false);
            LightRed.SetActive(true);
        }
    }
}
