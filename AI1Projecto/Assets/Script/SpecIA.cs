using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SpectatorState
{
    Idle,
    WatchingConcert,
    GoingToGreenZone,
    GoingToFoodZone,
    GettingOut
}

public class SpecIA : MonoBehaviour
{
    private SpectatorState currentState = SpectatorState.Idle;

    private float energy = 100f;
    private float hunger = 0f;

    private GameBrain brain;

    private Transform[] stages;
    private Transform[] greenZones;
    private Transform[] foodZones;
    private Transform[] exits;
    private List<Vector3> agents = new List<Vector3>();

    private NavMeshAgent agent;

    private bool isParalyzed = false;
    private bool isSlowedDown = false;
    private float originalSpeed;
    private float paralysisTimer = 0f;

    void Start()
    {
        brain = FindAnyObjectByType<GameBrain>();
        energy = Random.Range(30f, 100f);
        hunger = Random.Range(0f, 70f);
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1.0f;
        agent.avoidancePriority = Random.Range(30, 60);
        stages = brain.GetStagePosition();
        greenZones = brain.GetGreenPosition();
        foodZones = brain.GetFoodPosition();
        exits = brain.GetExitsPosition();
        agent.SetDestination(GetPosition(stages));

        originalSpeed = agent.speed;
    }

    void Update()
    {
        if (IsOnExit(exits))
        {
            Destroy(gameObject);
        }

        if (isParalyzed)
            {
                paralysisTimer -= Time.deltaTime;
                if (paralysisTimer <= 0f)
                {
                    isParalyzed = false;
                    agent.isStopped = false;
                    SlowDown();
                }

                return;
            }

        energy -= Time.deltaTime * 3;
        hunger += Time.deltaTime * 0.75f;

        switch (currentState)
        {
            case SpectatorState.Idle:
                CheckNeeds();
                break;

            case SpectatorState.WatchingConcert:
                if (!agent.pathPending && agent.remainingDistance
                 <= agent.stoppingDistance)
                    agent.isStopped = true;
                if (energy < 30) ChangeState(SpectatorState.GoingToGreenZone);
                else if (hunger > 70) ChangeState(SpectatorState.GoingToFoodZone);
                break;

            case SpectatorState.GoingToGreenZone:
                if (!agent.pathPending && agent.remainingDistance
                 <= agent.stoppingDistance)
                    agent.isStopped = true;
                if (agent.remainingDistance < 1f)
                {
                    energy += Time.deltaTime * 20f;
                    if (energy >= 100)
                    {
                        energy = 100;
                        ChangeState(SpectatorState.WatchingConcert);
                    }
                }
                break;
            case SpectatorState.GoingToFoodZone:
                if (!agent.pathPending && agent.remainingDistance
                 <= agent.stoppingDistance)
                    agent.isStopped = true;
                if (agent.remainingDistance < 1f)
                {
                    hunger -= Time.deltaTime * 30f;
                    if (hunger <= 0)
                    {
                        hunger = 0;
                        ChangeState(SpectatorState.WatchingConcert);
                    }
                }
                break;
        }
    }


    public void ChangeState(SpectatorState newState)
    {
        currentState = newState;
        agent.isStopped = false;

        switch (newState)
        {
            case SpectatorState.WatchingConcert:
                agent.SetDestination(GetPosition(stages));
                break;

            case SpectatorState.GoingToGreenZone:
                agent.SetDestination(GetPosition(greenZones));
                break;

            case SpectatorState.GoingToFoodZone:
                agent.SetDestination(GetPosition(foodZones));
                break;

            case SpectatorState.GettingOut:
                agent.SetDestination(GetPosition(exits));
                break;

            case SpectatorState.Idle:
                agent.ResetPath();
                break;


        }
    }

    void CheckNeeds()
    {
        if (energy < 30) ChangeState(SpectatorState.GoingToGreenZone);
        else if (hunger > 70) ChangeState(SpectatorState.GoingToFoodZone);
        else ChangeState(SpectatorState.WatchingConcert);
    }

    Vector3 GetPosition(Transform[] locations)
    {

        Transform chosenLocation = locations[Random.Range(0, locations.Length)];
        if (chosenLocation.GetComponent<Renderer>() == null)
        {

            return chosenLocation.position;
        }
        List<Vector3> randomPoints = GenerateRandomPoints(chosenLocation);

        Vector3 chosenPoint = new Vector3(0, 0, 0);



        agents = brain.GetAgentPositions();
        List<Vector3> foundAgents = new List<Vector3>();

        foreach (Vector3 otherAgent in agents)
        {
            Vector3 chosenLocationCenter = chosenLocation.position;

            Vector3 chosenLocationSize = chosenLocation.localScale;

            float halfX = chosenLocationSize.x / 2f;

            float halfZ = chosenLocationSize.z / 2f;

            bool insideX = otherAgent.x >= chosenLocationCenter.x - halfX
            && otherAgent.x <= chosenLocationCenter.x + halfX;

            bool insideZ = otherAgent.z >= chosenLocationCenter.z - halfZ
            && otherAgent.z <= chosenLocationCenter.z + halfZ;

            if (insideX && insideZ)
            {
                foundAgents.Add(otherAgent);
            }
        }


        if (foundAgents.Count == 0)
        {
            chosenPoint = randomPoints[0];
        }
        else
        {
            chosenPoint = GetMostIsolatedPoint(randomPoints);
        }


        return chosenPoint;
    }

    List<Vector3> GenerateRandomPoints(Transform areaObject)

    {

        List<Vector3> points = new List<Vector3>();

        Vector3 center = areaObject.position;

        Vector3 size = areaObject.localScale;

        float halfWidth = size.x / 2f;

        float halfDepth = size.z / 2f;

        for (int i = 0; i < 10; i++)

        {

            float x = Random.Range(center.x - halfWidth, center.x + halfWidth);

            float z = Random.Range(center.z - halfDepth, center.z + halfDepth);

            points.Add(new Vector3(x, 0f, z));

        }

        return points;

    }

    Vector3 GetMostIsolatedPoint(List<Vector3> points)

    {

        Vector3 bestPoint = Vector3.zero;

        float maxMinDistance = float.MinValue;

        foreach (Vector3 point in points)

        {

            float minDistanceToAgents = float.MaxValue;

            foreach (Vector3 agent in agents)

            {

                float distance = Vector3.Distance(point, agent);

                if (distance < minDistanceToAgents)

                    minDistanceToAgents = distance;

            }

            if (minDistanceToAgents > maxMinDistance)

            {

                maxMinDistance = minDistanceToAgents;

                bestPoint = point;

            }

        }

        return bestPoint;

    }

    public void TakeExplosion(float paralysisDuration)
    {
        if (!isParalyzed && !isSlowedDown)
        {
            isParalyzed = true;
            paralysisTimer = paralysisDuration;
            agent.isStopped = true;
        }
    }

    private void SlowDown()
    {
        if (!isSlowedDown)
        {
            isSlowedDown = true;
            agent.speed = originalSpeed * 0.5f;
        }
    }

    public void FleeFromExplosion(Vector3 explosionPosition)
    {
        if (agent != null)
        {
            Vector3 fleeDirection = (transform.position - explosionPosition).normalized;
            Vector3 fleeTarget = transform.position + fleeDirection * 10f;

            agent.speed = 5f;
            agent.isStopped = false;
            agent.SetDestination(fleeTarget);
        }
    }

    bool IsOnExit(Transform[] exits)
    {
        foreach (Transform exit in exits)
        {
            Vector3 exitCenter = exit.position;
            Vector3 exitSize = exit.localScale;
            Vector3 agentPos = transform.position;

            float halfX = exitSize.x / 2f;
            float halfZ = exitSize.z / 2f;

            bool insideX = agentPos.x >= (exitCenter.x - halfX)
             && agentPos.x <= (exitCenter.x + halfX);
            bool insideZ = agentPos.z >= (exitCenter.z - halfZ)
             && agentPos.z <= (exitCenter.z + halfZ);

            if (insideX && insideZ)
                return true;
        }
        return false;
        
    }
}