using UnityEngine;
using System.Collections;

public class FireSpread : MonoBehaviour
{
    
    [SerializeField] private float spreadSpeed = 1f;
    //[SerializeField] private float maxRadius = 10f;
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private LayerMask agentLayer;
    private GameBrain brain;
    private float fireDuration = 10000000000000f;

    private float currentRadius = 1f;
    private float timer = 0f;

    void Start()
    {
        brain = FindAnyObjectByType<GameBrain>();
        StartCoroutine(DestroyFireAfterTime(fireDuration));
    }

    void Update()
    {
        Spread();
        BurnAgents();
    }

    IEnumerator DestroyFireAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }

    void Spread()
    {
        
        currentRadius += spreadSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        transform.localScale = new Vector3(currentRadius, 0.1f, currentRadius);
        
    }

    void BurnAgents()
    {
        timer += Time.deltaTime;

        if(timer >= damageInterval)
        {
            timer = 0f;

            Collider[] hitAgents = Physics.OverlapSphere(transform.position, currentRadius, agentLayer);

            foreach(Collider hit in hitAgents)
            {
                SpecIA agent = hit.GetComponent<SpecIA>();

                if(agent != null)
                {
                    float distanceToFire = Vector3.Distance(agent.transform.position, transform.position);
                    if (distanceToFire <= currentRadius)
                    {
                        brain.UpdateDead();
                        Destroy(agent.gameObject);
                    }    
                    
                }
            }
        }
    }
    
}
