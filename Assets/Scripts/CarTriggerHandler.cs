using UnityEngine;

public class CarTriggerHandler : MonoBehaviour
{
    public LevelManager levelManager;
    public bool isFront;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            levelManager.CarEnteredPlatform(isFront);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            levelManager.CarExitedPlatform(isFront);
        }
    }
}
