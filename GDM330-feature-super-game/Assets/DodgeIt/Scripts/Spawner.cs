using UnityEngine;
using Random = UnityEngine.Random;

namespace SuperGame.DodgeIt
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] Obstacle prefab;
        [SerializeField] float targetDelay = 3f;
        [SerializeField] float randomOffset = 5f;
    
        float currentDelay;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (currentDelay > 0)
            {
                currentDelay -= Time.deltaTime;
            }
            else
            {
                var newObstacle = Instantiate(prefab, GetRandomSpawnPosition(), Quaternion.identity);
                currentDelay = targetDelay / DifficultyManager.Instance.DifficultyLevel;
            }
        }

        Vector3 GetRandomSpawnPosition()
        {
            return new Vector3(transform.position.x + Random.Range(-randomOffset, randomOffset), transform.position.y, transform.position.z);
        }
    }
}
