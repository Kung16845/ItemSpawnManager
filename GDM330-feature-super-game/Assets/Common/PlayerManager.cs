using PhEngine.Core;
using UnityEngine;

namespace SuperGame
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        protected override void InitAfterAwake()
        {
            
        }

        public GameObject GetPlayer()
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
    }
}