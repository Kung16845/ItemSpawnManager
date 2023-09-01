using UnityEngine;

namespace SuperGame.DoodleJump
{
    public class PlatformGenerator : MonoBehaviour
    {
        public GameObject platformPrefab;
        public int numberOfPlatforms = 20;
        public float minY = 1f;
        public float maxY = 3.5f;
        public float platformXRange = 4f;

        private Transform playerTransform;
        private float lastPlatformY;

        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            lastPlatformY = transform.position.y;

            GenerateInitialPlatforms();
        }

        private void Update()
        {
            if (playerTransform.position.y > lastPlatformY - 5f)
            {
                GeneratePlatform();
            }
        }

        private void GenerateInitialPlatforms()
        {
            for (int i = 0; i < numberOfPlatforms / DifficultyManager.Instance.DifficultyLevel; i++)
            {
                GeneratePlatform();
            }
        }

        private void GeneratePlatform()
        {
            Vector3 platformPosition = new Vector3(
                Random.Range(-platformXRange, platformXRange),
                lastPlatformY + Random.Range(minY, maxY),
                0f
            );

            Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            lastPlatformY = platformPosition.y;
        }
    

    }
}
