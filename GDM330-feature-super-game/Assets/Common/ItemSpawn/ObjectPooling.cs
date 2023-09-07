using System;
using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;
using PhEngine.Core;

namespace SuperGame
{
    public class ObjectPooling : Singleton<ObjectPooling>
    {
        protected override void InitAfterAwake()
        {
            
        }
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }
        public Transform player;
        public int Armor = 0;
        public int Heart = 0;
        public int Poison = 0;
        public float distanceTranformPlayerY;
        public float durationItemSpawn;
        public int limitArmor;
        public int limitPoison;
        public int limitHeart;
        public bool StopSpawnItem = false;
        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        void Start()
        {
            player = FindObjectOfType<Player_CheckEnterItem>().transform;
            CreateObjectPooling();
            UseSpawnItemsCoroutine();
        }
        
        private void Update() {
            player = FindObjectOfType<Player_CheckEnterItem>().transform;
        }
        public void CreateObjectPooling()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectsPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab,this.gameObject.transform);
                    obj.SetActive(false);
                    objectsPool.Enqueue(obj);
                }
                poolDictionary.Add(pool.tag, objectsPool);
            }
        }
        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag" + tag + "doesn't excist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);

            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        public Vector3 randomtranfrom()
        {
            Vector3 playerTranfrom = player.position;
            float randomX = UnityEngine.Random.Range(playerTranfrom.x - 2f, playerTranfrom.x + 2f);
            float randomY = UnityEngine.Random.Range(playerTranfrom.y + distanceTranformPlayerY, playerTranfrom.y + distanceTranformPlayerY + 1f);
            return new Vector3(randomX, randomY, playerTranfrom.z);
        }


        public string randomObject()
        {
            int attempts = 0; // ตัวแปรนี้จะช่วยป้องกันการ loop ไม่จำกัด
            int maxAttempts = 100; // ตั้งค่าจำนวนครั้งที่สูงสุดที่สามารถพยายามได้

            while (!StopSpawnItem && attempts < maxAttempts)
            {
                Pool randomPool = pools[UnityEngine.Random.Range(0, pools.Count)];
                string objectrandom = randomPool.tag;

                if (Poison >= limitPoison && Heart >= limitHeart && Armor >= limitArmor)
                {
                    StopSpawnItem = true;
                }
                else if (Armor < limitArmor && objectrandom == "Armor")
                {
                    Armor++;
                    return objectrandom;
                }
                else if (Heart < limitHeart && objectrandom == "Heart")
                {
                    Heart++;
                    return objectrandom;
                }
                else if (Poison < limitPoison && objectrandom == "Poison")
                {
                    Poison++;
                    return objectrandom;
                }
            }

            return null;
        }
        public void UseSpawnItemsCoroutine()
        {
            StartCoroutine(SpawnItemsCoroutine());
        }
        private IEnumerator SpawnItemsCoroutine()
        {
            while (!StopSpawnItem)
            {
                SpawnFromPool(randomObject(), randomtranfrom(), Quaternion.identity);
                yield return new WaitForSeconds(durationItemSpawn);
            }
        }
    }
}
