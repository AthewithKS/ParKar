using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.Experimental.GlobalIllumination;

[ExecuteAlways]
public class DayAndNightCycle : MonoBehaviour
{
    [Header("SKY reference")]
    [SerializeField] private Texture2D MorningSky;
    [SerializeField] private Texture2D NoonSky;
    [SerializeField] private Texture2D EveningSky;
    [SerializeField] private Texture2D NightSky;
    //public Material SkyBox;
    //referance
    [SerializeField]private Light DirectionalLight;
    [SerializeField]private LightingPresets preset;
    //[SerializeField]private StreetLight StreetLight;
    //variable
    [SerializeField, Range(0, 24)] private float TimeofDay;
    [SerializeField] private float dayTransitionSpeed;
    public UnityEvent lightson;
    public UnityEvent lightsoff;
    

    bool streetLightsAreOn = false;
    private void Start()
    {
        DirectionalLight = GetComponent<Light>();
        //StreetLight = GetComponent<StreetLight>();

        //update skybox material
        RenderSettings.skybox.SetFloat("_Blend", 0);
        RenderSettings.skybox.SetTexture("_Texture1", NightSky);
        RenderSettings.skybox.SetTexture("_Texture2", MorningSky);
    }

    void Update()
    {
        if(preset == null)
        {
            return;
        }
        if (Application.isPlaying) 
        {
            TimeofDay += Time.deltaTime/dayTransitionSpeed;
            TimeofDay %= 24;

            UpdateLighting(TimeofDay / 24f);
        }
        StreetLightVehicleLightActivity();
        OnHoureChange(TimeofDay);
    }
    void StreetLightVehicleLightActivity()
    {

        if (TimeofDay >= 18f && !streetLightsAreOn)
        {
            lightson.Invoke();
            streetLightsAreOn = true;
        }
        else if (TimeofDay < 6f && !streetLightsAreOn)
        {
            lightson.Invoke();
            streetLightsAreOn = true;
        }
        else if (TimeofDay >= 6f && TimeofDay < 18f && streetLightsAreOn)
        {
            lightsoff.Invoke();
            streetLightsAreOn = false;
        }
    }
    void UpdateLighting(float timePercent)
    {
        if(DirectionalLight != null)
        {
            DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -30f, 0f));
        }
    }
    void OnHoureChange(float value)
    {
        if (value >= 5.0f && value < 7.0f)
        {
            StartCoroutine(LerpSkybox(NightSky, MorningSky, 5f));
        }
        else if (value >= 8.0f && value < 16.0f)
        {
            StartCoroutine(LerpSkybox(MorningSky, NoonSky, 5f));
        }
        else if (value >= 17.0f && value < 21.0f)
        {
            StartCoroutine(LerpSkybox(NoonSky, EveningSky, 5f));
        }
        else if (value >= 22.0f)
        {
            StartCoroutine(LerpSkybox(EveningSky, NightSky, 5f));
        }
    }
    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetFloat("_Blend", 0);
        RenderSettings.skybox.SetTexture("_Texture1", a);

        StartCoroutine(SetTexture2AfterDelay(b, 1.2f));

        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float blendValue = Mathf.SmoothStep(0, 1, elapsedTime / time);  // Eased interpolation
            RenderSettings.skybox.SetFloat("_Blend", blendValue);
            yield return null;
        }

        // Ensure final state is set
        RenderSettings.skybox.SetFloat("_Blend", 1);
        RenderSettings.skybox.SetTexture("_Texture1", b);
    }

    private IEnumerator SetTexture2AfterDelay(Texture2D b, float delay)
    {
        // Wait for the specified delay.
        yield return new WaitForSeconds(delay);

        // Set Texture2 (Texture B) after the delay.
        RenderSettings.skybox.SetTexture("_Texture2", b);
    }
}
