using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{

    [Header("Canvas")]
    public GameObject StartPanel;
    bool IsStart;
    public GameObject PausePanel;
    public LevelManager Levelmanager;
    public Text time;
    float newTime;
    

    //Fuel setup
    public Slider fuelBar;
    public Text FuelIndicatortext;
    public float fuelamount = 50f;
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 1f;
    // Coin
    public Text CoinText;
    public int AddCoin;


    // starting camera
    public CinemachineVirtualCamera StartingCamera;
    // Start is called before the first frame update
    private void Awake()
    {
        LoadPlayer();
    }
    void Start()
    {
        StartingCamera.Priority = 11;
        StartPanel.SetActive(true);
        IsStart = true;

        InvokeRepeating("ConsumeFuel", 1f, 20f);
    }

    // Update is called once per frame
    void Update()
    {

        //PlayTime
        
        if (Input.GetKeyDown(KeyCode.Escape) && !IsStart)
        {
            PauseGame();
        }
        RealTime();
        FuelUpdate(0);

    }
    public void RealTime()
    {
        newTime += Time.deltaTime;
        int minut = Mathf.FloorToInt(newTime / 60);
        int seconds = Mathf.FloorToInt(newTime % 60);
        time.text = string.Format("{0:00}:{1:00}", minut, seconds);

    }
    public void PauseGame()
    {
        PausePanel.SetActive(true );
        Time.timeScale = 0f;
    }
    public void FreeRome()
    {
        StartPanel.SetActive(false);
        PausePanel.SetActive(false);
        IsStart = false;
        StartingCamera.Priority = 0;
        Time.timeScale = 1f;

    }
    public void StartLevel()
    {
        Levelmanager.LevelStart();
        IsStart = false;
        StartPanel.SetActive(false);
        PausePanel.SetActive(false );
        StartingCamera.Priority= 0;
        Time.timeScale= 1f;
    }
    public void Quit()
    {
        SavePlayer();
        Application.Quit();
    }
    public void FuelUpdate(int add)
    {
        fuelamount += add;
        fuelBar.value = fuelamount;
        FuelIndicatortext.text ="Fuel "+fuelamount.ToString("00");
        if (fuelamount>100)fuelamount = 100;
    }
    private void ConsumeFuel()
    {
        if (fuelamount > 0)
        {
            fuelamount -= fuelConsumptionRate;
            FuelUpdate(0);
        }
        else
        {
            Debug.Log("Out of fuel!");
        }
    }
    public void CoinCount(int add)
    {
        AddCoin += add;
        CoinText.text ="Coin "+ AddCoin.ToString("0");   
    }
    public void SavePlayer()
    {
        CarController car = FindObjectOfType<CarController>();
        SaveGame.SavePlayer(car, Levelmanager, this);

    }
    public void LoadPlayer()
    {
        PlayerData data = SaveGame.LoadPlayer();
        if(data != null )
        {
            Levelmanager.currentPlatformIndex = data.level;
            fuelamount = data .fuel;
            Vector3 position;

            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            CarController car = FindAnyObjectByType<CarController>();
            car.transform.position = position;
        }
    }
}
