using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    public GameManager Manager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            Manager.FuelUpdate(20);
            Destroy(other.gameObject);
        }
    }
}
