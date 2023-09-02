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

        public void CollectItem_Heart()
        {
            Debug.Log("Dearn Tid Leaw");
            if (GameManager.Instance.GetLifeCount() < GameManager.Instance.GetMaxLifeCount()) {
                GameManager.Instance.AddLifeCount();
                Debug.Log("heal Leaw");
            }
        }

        public void CollectItem_Death() {
            GameManager.Instance.LoseLifeCount();
            Debug.Log("Tai Ha Leaw");
        }

    }
}