using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float paralysisDuration = 5f;
    [SerializeField] private LayerMask agentLayer;
    [SerializeField] private float killRadius = 3f;
    [SerializeField] private float fleeRadius = 8f;

    void Start()
    {
        Explode();
        HighlightExplosionArea();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, fleeRadius, agentLayer);

        foreach(Collider nearbyObject in colliders)
        {
            SpecIA agent = nearbyObject.GetComponent<SpecIA>();

            if(agent != null)
            {
                float distance = Vector3.Distance(transform.position, agent.transform.position);

                if(distance <= killRadius)
                {
                    Destroy(agent.gameObject);
                }
                else if(distance < explosionRadius)
                {
                    agent.TakeExplosion(paralysisDuration);
                }
                else if(distance <= fleeRadius)
                {
                    agent.FleeFromExplosion(transform.position);
                }
            }
        }

        Destroy(gameObject, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        
    }

    void HighlightExplosionArea()
    {
        Renderer renderer = GetComponent<Renderer>();

        if(renderer != null)
        {
            renderer.material = new Material(renderer.material);

            renderer.material.color = Color.red;

            float diameter = explosionRadius * 2f;

            transform.localScale = new Vector3(diameter, 0.1f, diameter);
        }
    }
}
