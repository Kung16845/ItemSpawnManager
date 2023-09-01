using System;
using UnityEngine;

namespace SuperGame
{
    [Serializable]
    public class CountdownTimer
    {
        public float duration;
        float currentTime;
        bool isStarted;
        
        public event Action OnDone;
        public event Action<float> OnTimeChange;

        public void Start()
        {
            isStarted = true;
            currentTime = duration;
        }

        public void PassTime()
        {
            if (!isStarted)
                return;
            
            currentTime -= Time.deltaTime;
            OnTimeChange?.Invoke(currentTime);
            if (currentTime <= 0)
            {
                OnDone?.Invoke();
                isStarted = false;
                currentTime = 0;
            }
        }
    }
}