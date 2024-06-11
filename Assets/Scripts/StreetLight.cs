using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLight : MonoBehaviour
{
    
    
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
}
