using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhEngine.Core;

namespace SuperGame
{

    public class ItemEffect : Singleton<ItemEffect>
    {
        protected override void InitAfterAwake()
        { }

        [SerializeField] GameObject Armor;

        public void CollectItem_Heart()
        {
            if (GameManager.Instance.GetLifeCount() < GameManager.Instance.GetMaxLifeCount()) {
                GameManager.Instance.AddLifeCount();
                Debug.Log("heal Leaw");
            }
        }
        public void CollectItem_Armor()
        {
            Vector3 spawnPosition = new Vector3(99999999, transform.position.y, transform.position.z);
            Instantiate(Armor, spawnPosition, Quaternion.identity);
        }

        public void CollectItem_Death() {
            GameManager.Instance.LoseLifeCount();
            Debug.Log("Tai Ha Leaw");
        }

    }
}