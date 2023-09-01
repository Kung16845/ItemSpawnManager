using System.Collections;
using UnityEngine;

namespace SuperGame.SubwaySurfer2D
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject spawnerLeft;
        [SerializeField] private GameObject spawnerMiddle;
        [SerializeField] private GameObject spawnerRight;
        [SerializeField] private GameObject fencePrefab;
        [SerializeField] private GameObject trainPrefab;
        [SerializeField] private GameObject coinsPrefab;
        [SerializeField] private float delay;
        // Start is called before the first frame update
        void Start()
        {
            Invoke("ObstructionRandom", delay / DifficultyManager.Instance.DifficultyLevel);
        }

        // Update is called once per frame
        void ObstructionRandom()
        {
            delay = Random.Range(0.5f, 1.5f);
            float ObstructionRandom = Random.Range(1,4);//3 is coins
            float PositionRandom = Random.Range(1, 4);

            switch (PositionRandom)
            {
                case 1:
                    if (ObstructionRandom == 1)
                        ObstructionSpawn(fencePrefab, spawnerLeft);
                    else if (ObstructionRandom == 2)
                        ObstructionSpawn(trainPrefab, spawnerLeft);
                    else
                        StartCoroutine(CoinsSpawn(spawnerLeft));
                    break;
                case 2:
                    if (ObstructionRandom == 1)
                        ObstructionSpawn(fencePrefab, spawnerMiddle);
                    else if (ObstructionRandom == 2)
                        ObstructionSpawn(trainPrefab, spawnerMiddle);
                    else
                        StartCoroutine(CoinsSpawn(spawnerMiddle));
                    break;
                case 3:
                    if (ObstructionRandom == 1)
                        ObstructionSpawn(fencePrefab, spawnerRight);
                    else if (ObstructionRandom == 2)
                        ObstructionSpawn(trainPrefab, spawnerRight);
                    else
                        StartCoroutine(CoinsSpawn(spawnerRight));
                    break;
            }
            Invoke("ObstructionRandom", delay);
        }
        void ObstructionSpawn(GameObject Obstruction, GameObject spawnPosition)
        {
            Instantiate(Obstruction, spawnPosition.transform.position, transform.rotation);
        }
        IEnumerator CoinsSpawn(GameObject spawnPosition)
        {
            float CoinsAmountRandom = Random.Range(1, 5);
            for (float CoinsNumber = 0; CoinsNumber < CoinsAmountRandom; CoinsNumber++)
            {
                yield return new WaitForSeconds(0.2f);
                Instantiate(coinsPrefab, spawnPosition.transform.position, transform.rotation);
            }
        }
    }
}
