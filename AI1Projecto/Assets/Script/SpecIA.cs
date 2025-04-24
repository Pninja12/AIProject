using UnityEngine;
using UnityEngine.AI;

public enum SpectatorState
{
    Idle,
    WatchingConcert,
    GoingToGreenZone,
    GoingToFoodZone
}

public class SpecIA : MonoBehaviour
{
    private SpectatorState currentState = SpectatorState.Idle;

    private float energy = 100f;
    private float hunger = 0f;

    [SerializeField] private Transform[] stages;
    [SerializeField] private Transform[] greenZones;
    [SerializeField] private Transform[] foodZones;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(SpectatorState.WatchingConcert);
    }

    void Update()
    {
        energy -= Time.deltaTime * 5;
        hunger += Time.deltaTime * 1.5f;

        switch(currentState)
        {
            case SpectatorState.Idle:
                CheckNeeds();
                break;

            case SpectatorState.WatchingConcert:
                if(energy < 30) ChangeState(SpectatorState.GoingToGreenZone);
                else if(hunger > 70) ChangeState(SpectatorState.GoingToFoodZone);
                break;
            
            case SpectatorState.GoingToGreenZone:
                if(agent.remainingDistance < 1f)
                {
                    energy += Time.deltaTime * 20f;
                    if(energy >= 100)
                    {
                        energy = 100;
                        ChangeState(SpectatorState.WatchingConcert);
                    }
                }
                break;
            case SpectatorState.GoingToFoodZone:
                if(agent.remainingDistance < 1f)
                {
                    hunger -= Time.deltaTime * 30f;
                    if(hunger <= 0)
                    {
                        hunger = 0;
                        ChangeState(SpectatorState.WatchingConcert);
                    }
                }
                break;
        }
    }


    void ChangeState(SpectatorState newState)
    {
        currentState = newState;

        switch(newState)
        {
            case SpectatorState.WatchingConcert:
                agent.SetDestination(GetRandomPosition(stages).position);
                break;
        
            case SpectatorState.GoingToGreenZone:
                agent.SetDestination(GetRandomPosition(greenZones).position);
                break;
        
            case SpectatorState.GoingToFoodZone:
                agent.SetDestination(GetRandomPosition(foodZones).position);
                break;
            case SpectatorState.Idle:
                agent.ResetPath();
                break;
        }
    }

    void CheckNeeds()
    {
        if(energy < 30) ChangeState(SpectatorState.GoingToGreenZone);
        else if(hunger > 70) ChangeState(SpectatorState.GoingToFoodZone);
        else ChangeState(SpectatorState.WatchingConcert);
    }

    Transform GetRandomPosition(Transform[] locations)
    {
        int i = Random.Range(0, locations.Length);
        return locations[i];
    }
}