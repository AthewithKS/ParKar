using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacters : MonoBehaviour
{
    //public GameObject[] characters;

    public NavMeshAgent agent;
    public Animator animator;

    public GameObject path;
    private Transform[] PathPoints;

    public float miniDistance;
    //public int index = 0;

    private int currentIdx = 0;
    private int targetIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        PathPoints = new Transform[path.transform.childCount];
        //to find all the child object of path prefab and fill it in pathPoints
        for(int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = path.transform.GetChild(i);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (agent.speed > 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
    void LateUpdate()
    {
        Roam();
    }
    //void Roam()
    //{
    //    if (Vector3.Distance(transform.position, PathPoints[index].position) < miniDistance)
    //    {
    //        if (index + 1 != PathPoints.Length)
    //        {
    //            index++;
    //        }
    //        else
    //        {
    //            index = 0;
    //        }
    //    }
    //    agent.SetDestination(PathPoints[index].position);
    //}
    void Roam()
    {
        if (Vector3.Distance(transform.position, PathPoints[targetIdx].position) < miniDistance)
        {
            SetRandomDestination();
        }
        agent.SetDestination(PathPoints[targetIdx].position);
    }
    void SetRandomDestination()
    {
        // Ensure a new random target index different from the current one
        int newTargetIndex;
        do
        {
            newTargetIndex = Random.Range(0, PathPoints.Length);
        } while (newTargetIndex == currentIdx);

        currentIdx = targetIdx;
        targetIdx = newTargetIndex;
    }
}
