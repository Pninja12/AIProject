using System.Collections.Generic;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class GameBrain : MonoBehaviour
{
    [SerializeField] private Transform[] stages;  // Assign the square GameObject in the Inspector
    [SerializeField] private Transform[] greenZones;
    [SerializeField] private Transform[] foodZones;
    private List<Vector3> agentPositions = new List<Vector3>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update()
    {

        agentPositions.Clear();
        NavMeshAgent[] agents = FindObjectsOfType<NavMeshAgent>();
        
        foreach (NavMeshAgent agent in agents)
        {
            if (agent.isActiveAndEnabled)

                agentPositions.Add(agent.transform.position);
        }

    }

    public List<Vector3> GetAgentPositions()
    {
        return new List<Vector3>(agentPositions); // Return a copy

    }

    public Transform[] GetStagePosition()
    {
        return stages;
    }

    public Transform[] GetGreenPosition()
    {
        return greenZones;
    }

    public Transform[] GetFoodPosition()
    {
        return foodZones;
    }
}
