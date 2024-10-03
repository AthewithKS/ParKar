using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLight : MonoBehaviour,IHourObserver
{
    [SerializeField] private HourObserverManager observerManager;
    public void ActivateStreetlights()
    { 
        GameObject[] streetlights = GameObject.FindGameObjectsWithTag("Light");
        if (streetlights != null && streetlights.Length > 0)
        {
            foreach (GameObject streetlight in streetlights)
            {
                foreach (Transform child in streetlight.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogError("No Streetlight GameObjects found!");
        }

    }

    // Method to deactivate all street lights
    public void DeactivateStreetlights()
    {

        GameObject[] streetlights = GameObject.FindGameObjectsWithTag("Light");
        if (streetlights != null && streetlights.Length > 0)
        {
            foreach (GameObject streetlight in streetlights)
            {
                foreach(Transform child in streetlight.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("No Streetlight GameObjects found!");
        }
    }
    public void onHourChanged(float currentHour)
    {
        if(currentHour>=18f || currentHour < 6f)
        {
            ActivateStreetlights();
        }
        else
        {
            DeactivateStreetlights();
        }
    }
    private void OnEnable()
    {
        observerManager?.Register(this);    
    }
    private void OnDisable()
    {
        observerManager?.UnRegister(this);
    }
}
