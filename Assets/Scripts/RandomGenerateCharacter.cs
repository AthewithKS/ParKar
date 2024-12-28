using UnityEngine;

public class RandomGenerateCharacter : MonoBehaviour
{
    public GameObject[] characters;

    public GameObject path;
    private Transform[] PathPoints;

    // Start is called before the first frame update
    void Start()
    {

        PathPoints = new Transform[path.transform.childCount];
        //to find all the child object of path prefab and fill it in pathPoints
        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = path.transform.GetChild(i);
        }
        for (int i = 0; i < 10; i++)
        {
            CharacterGenerate();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }
    void CharacterGenerate()
    {
        // Select a random path point
        int RandomPathPoint = Random.Range(0, PathPoints.Length);
        // Select a random character prefab
        int RandomChara = Random.Range(0, characters.Length);

        // Instantiate the selected character at the chosen path point
        GameObject newCharacter = Instantiate(characters[RandomChara], PathPoints[RandomPathPoint].position, Quaternion.identity);

        // Set up the new character's components
        AICharacters characterScript =newCharacter.GetComponent<AICharacters>();
        characterScript.path = path;
    }
}
