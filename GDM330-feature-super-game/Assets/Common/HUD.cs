using System;
using System.Collections.Generic;
using PhEngine.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SuperGame
{
    public class HUD : Singleton<HUD>
    {
        [Header("Status")] [SerializeField] Image lifePointPrefab;
        [SerializeField] List<Image> lifePointList = new List<Image>();
        [SerializeField] TMP_Text levelText;

        [Header("Time")] [SerializeField] TMP_Text gameStartCountdownText;
        [SerializeField] Image countdownGaugeImage;
        [SerializeField] TMP_Text gameEndCountdownTimeText;

        [Header("End Game")] 
        [SerializeField] CanvasGroup endGameMenuCanvasGroup;
        [SerializeField] TMP_Text resultText;
        [SerializeField] Button restartButton;
        [SerializeField] Button nextLevel;

        public event Action OnRestart;
        public event Action OnNext;

        protected override void InitAfterAwake()
        {
            nextLevel.onClick.AddListener(NotifyOnNext);
            restartButton.onClick.AddListener(NotifyOnRestart);
        }

        public void NotifyOnNext()
        {
            SetEndGameUIVisible(false,false);
            OnNext?.Invoke();
        }
        
        public void NotifyOnRestart()
        {
            SetEndGameUIVisible(false,false);
            OnRestart?.Invoke();
        }

        public void SetGameEndCountdownTime(float timeLeft, float duration)
        {
            countdownGaugeImage.transform.localScale = new Vector3(timeLeft / duration, 1, 1);
            gameEndCountdownTimeText.text = Mathf.RoundToInt(timeLeft).ToString();
        }

        public void SetLifeCount(int count)
        {
            var maxLife = count - lifePointList.Count;
            for (int i = 0; i <= maxLife; i++)
                lifePointList.Add(Instantiate(lifePointPrefab, lifePointPrefab.transform.parent, false));

            var index = 0;
            foreach (var image in lifePointList)
            {
                image.gameObject.SetActive(index < count);
                index++;
            }
        }

        public void SetEndGameUIVisible(bool isVisible, bool isLose)
        {
            resultText.text = isLose ? "You Lose!" : "Level Cleared!";
            endGameMenuCanvasGroup.gameObject.SetActive(isVisible);
            nextLevel.interactable = !isLose;
        }
        
        public void SetLevel(int level)
        {
            levelText.text = "Level : " + level;
        }
    }
}