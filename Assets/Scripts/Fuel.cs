using UnityEngine;

public class Fuel : MonoBehaviour
{
    public GameManager Manager;
    public GameObject fuelFill_Text;
    bool isNearPump = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            Manager.FuelUpdate(20);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Coin"))
        {
            Manager.CoinCount(20);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("GasPump"))
        {
            isNearPump=true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GasPump"))
        {
            isNearPump = false;
        }
    }
    private void Update()
    {
        fuelFill_Text.SetActive(isNearPump);
        if (isNearPump && Input.GetKeyUp(KeyCode.F))
        {
            Manager.FuelUpdate(100);
        }
    }
}
