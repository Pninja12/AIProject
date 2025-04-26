using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float spawnInternal = 5f;
    [SerializeField] private Vector2 mapMin;
    [SerializeField] private Vector2 mapMax;

    private float timer;


    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInternal)
        {
            SpawnExplosion();
            timer = 0f;
        }
    }

    void SpawnExplosion()
    {
        float randomX = Random.Range(mapMin.x, mapMax.x);
        float randomZ = Random.Range(mapMin.y, mapMax.y);

        Vector3 spawnPosition = new Vector3(randomX, 1f, randomZ);

        Instantiate(explosionPrefab, spawnPosition, Quaternion.identity);

        spawnInternal = Random.Range(3f, 8f);
    }
}
