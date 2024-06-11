using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera platformCamera;
    public float SwithcDelya = 3f;

    // Start is called before the first frame update
    void Start()
    {
        platformCamera.Priority = 11;
    
        StartCoroutine(SwitchToCarCamera());
    }

    // Update is called once per frame
    private IEnumerator SwitchToCarCamera()
    {
        yield return new WaitForSeconds(SwithcDelya);

        platformCamera.Priority = 0;
    }
}
