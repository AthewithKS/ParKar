using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelPassedPanel;
    public GameObject platformParent;
    public GameObject LevelFailedPanel;

    private int frontCount = 0;
    private int backCount = 0;
    public int currentPlatformIndex = 0;
    //level timer;
    public Text Timer;
    public float MaxLevelTimer ;
    float LevelTimer ;
    bool platformActive = false;
    bool lvlpass = false;
    private List<Transform> platforms;

    public GameManager gameManager;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        LevelTimer = MaxLevelTimer;
        levelPassedPanel.SetActive(false);

        // Collect all platform children
        platforms = new List<Transform>();
        foreach (Transform child in platformParent.transform)
        {
            platforms.Add(child);
            child.gameObject.SetActive(false);
        }

        // Activate the first platform
        
        
    }
    private void Update()
    {
        if (platformActive && !lvlpass)
        {
            LevelTimer -= Time.deltaTime;
        }
        else LevelTimer = MaxLevelTimer;
        if (LevelTimer <= 0)
        {
            LevelFailled();
        }
        Timer.text = LevelTimer.ToString("0");
    }
    public void LevelStart()
    {
        if (platforms.Count > 0)
        {
            platforms[currentPlatformIndex].gameObject.SetActive(true);platformActive = true;
        }

    }
    public void CarEnteredPlatform(bool isFront)
    {
        if (isFront)
        {
            frontCount++;
        }
        else
        {
            backCount++;
        }

        CheckLevelPassed();
    }

    public void CarExitedPlatform(bool isFront)
    {
        if (isFront)
        {
            frontCount--;
        }
        else
        {
            backCount--;
        }
    }
    public void FreeRome()
    {
        levelPassedPanel.SetActive(false);
        LevelFailedPanel.SetActive(false);
        platforms[currentPlatformIndex].gameObject.SetActive(false); platformActive = false;

    }
    public void LevelFailled()
    {
        levelPassedPanel.SetActive(false);
        LevelFailedPanel.SetActive(true);
        platforms[currentPlatformIndex].gameObject.SetActive(false); platformActive= false;
    }
    private void CheckLevelPassed()
    {
        if (frontCount > 0 && backCount > 0)
        {
            levelPassedPanel.SetActive(true);
            lvlPassCoins();
            lvlpass = true;
        }
        else
        {
            levelPassedPanel.SetActive(false);
            lvlpass=false;
        }
    }

    public void ActivateNextPlatform()
    {
        if (currentPlatformIndex < platforms.Count - 1)
        {
            levelPassedPanel.SetActive(false);lvlpass = false;
            platforms[currentPlatformIndex].gameObject.SetActive(false); platformActive= false;
            currentPlatformIndex++;
            platforms[currentPlatformIndex].gameObject.SetActive(true); platformActive= true; LevelTimer = MaxLevelTimer;
            frontCount = 0;
            backCount = 0;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        platforms[currentPlatformIndex].gameObject.SetActive(true); platformActive= true; LevelTimer = MaxLevelTimer;
        currentPlatformIndex = 0;
        LevelFailedPanel.SetActive(false);
        gameManager.PausePanel.SetActive(false);
        LevelTimer = MaxLevelTimer;

    }
    public void lvlPassCoins()
    {
        int coin =(int)LevelTimer * 7;
        gameManager.CoinCount(coin);
    }
}
