using System.Collections;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField] private GameObject agentPrefab;
    public void StartSpawningAgents(int count)
    {
        StartCoroutine(SpawnAgentsOverTime(count));
    }
    private IEnumerator SpawnAgentsOverTime(int total)
    {
        for (int i = 0; i < total; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 5f;
            pos.y = 0;
            GameObject agent = Instantiate(agentPrefab, transform.position + pos, Quaternion.identity);
            agent.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            yield return new WaitForSeconds(0.5f);
            // Wait half a second 
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartSpawningAgents(100);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
