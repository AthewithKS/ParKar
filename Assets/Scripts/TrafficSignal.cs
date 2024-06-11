using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignal : MonoBehaviour
{
    public GameObject MainHighWay;
    public GameObject SubWay;
    float Traffictime=0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Traffictime += Time.deltaTime;
        if (Traffictime > 20f) Traffictime = 0f;
        TrafficControle();

    }
    void TrafficControle()
    {
        if (Traffictime > 10f)
        {
            MainHighWay.SetActive(true);
            SubWay.SetActive(false);
        }
        else
        {
            MainHighWay.SetActive(false);
            SubWay.SetActive(true);
            
        }
    }
}
