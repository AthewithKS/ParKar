using UnityEngine;

[System.Serializable]// able to save it in file
public class PlayerData
{
    public int level;
    public float fuel;
    public float[] position;

    public PlayerData(LevelManager player , CarController carPosition ,GameManager gamemanager)//constructor
    {
        level = player.currentPlatformIndex;
        fuel = gamemanager.fuelamount;

        position = new float[3];
        position[0] = carPosition.transform.position.x;
        position[1] = carPosition.transform.position.y;
        position[2] = carPosition.transform.position.z;

    }
}
