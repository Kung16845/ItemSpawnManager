using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhEngine.Core;
using TMPro;

namespace SuperGame
{

    public class ItemEffect : Singleton<ItemEffect>
    {
        protected override void InitAfterAwake()
        { PoisonUI.SetActive(false); }

        [SerializeField] GameObject Armor;
        [Header("POISON")]
        [SerializeField] GameObject PoisonUI;
        [SerializeField] TextMeshProUGUI PoisoningTimer;
        [SerializeField] public bool isPoisoning;

        public void CollectItem_Heart()
        {
            if (GameManager.Instance.GetLifeCount() < GameManager.Instance.GetMaxLifeCount()) {
                GameManager.Instance.AddLifeCount();
                Debug.Log("Heart Collect!");
            }
        }
        public void CollectItem_Armor()
        {
            Vector3 spawnPosition = new Vector3(99999999, transform.position.y, transform.position.z);
            Instantiate(Armor, spawnPosition, Quaternion.identity);
            Debug.Log("Armor Collect!");

        }
        public void CollectItem_Poison() {
            Debug.Log("Poison Collect!");

            StartCoroutine(PoisonCountdown());
        }

        private IEnumerator PoisonCountdown()
        {
            PoisonUI.SetActive(true); isPoisoning = true;
            float timer = 3;

            while (timer > 0)
            {
                PoisoningTimer.text="Poison: " + timer.ToString("F1") + " seconds";
                yield return new WaitForSeconds(1f); // Wait for 1 second
                timer -= 1f;
            }
            PoisonUI.SetActive(false); isPoisoning = false;
        }

        /*
        public void CollectItem_Death() {
            GameManager.Instance.LoseLifeCount();
            Debug.Log("Tai Ha Leaw");
        }
        */
    }
}