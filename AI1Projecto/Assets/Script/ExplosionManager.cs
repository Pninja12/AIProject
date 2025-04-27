using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    //[SerializeField] private float spawnInternal = 5f;
    [SerializeField] private Vector2 mapMin;
    [SerializeField] private Vector2 mapMax;

    private float timer;


    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPosition = hit.point;
                spawnPosition.y += 0.5f;

                Instantiate(explosionPrefab, spawnPosition, Quaternion.identity); 
            }
        }
    }

}
