using System.Collections.Generic;
using PhEngine.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperGame
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Gameplay")]
        [SerializeField] CountdownTimer levelEndTimer;
        [SerializeField] int maxLifeCount = 3;
        [SerializeField] int lifeCount;
        [SerializeField] float timeScaleToAdd = 0.1f;

        [Header("UI")]
        [SerializeField] HUD hud;

        [Header("BGM")]
        [SerializeField] AudioSource bgm;
        
        bool isPaused;
        bool isGameOver;

        protected override void InitAfterAwake()
        {
            Pause();
            Reset();
            SetupHUD();
            
            //StartLevel();
        }
        
        void Reset()
        {
            lifeCount = maxLifeCount;
            hud.SetLifeCount(lifeCount);
            hud.SetLevel(LevelManager.Instance.CurrentLevel);         
        }
        void ResetObjectPooling()
        {
            ObjectPooling.Instance.Armor = 0;
            ObjectPooling.Instance.Poison = 0;
            ObjectPooling.Instance.Heart = 0;
            ObjectPooling.Instance.StopSpawnItem = false;
        }
        void SetupHUD()
        {
            levelEndTimer.OnDone += EndLevel;
            levelEndTimer.OnTimeChange += time => hud.SetGameEndCountdownTime(time, levelEndTimer.duration);

            hud.OnNext += LevelManager.Instance.MoveToNextLevel;
            hud.OnRestart += Restart;
        }

        void Update()
        {
            if (!isPaused)
                levelEndTimer.PassTime();
        }

        public void StartLevel()
        {
            isGameOver = false;
            levelEndTimer.Start();
            Resume();
            LevelManager.Instance.SetLastPlayedLevel();
            hud.SetGameEndCountdownTime(levelEndTimer.duration, levelEndTimer.duration);
        }

        public void Resume()
        {
            isPaused = false;
            var currentTimeScale = 1f + (timeScaleToAdd * ((LevelManager.Instance.CurrentLevel-1)/(LevelManager.Instance.LevelList.Count)));
            Time.timeScale = currentTimeScale;
            bgm.pitch = currentTimeScale;
        }

        void EndLevel()
        {
            AudioManager.Instance.Play("victory");
            ResetObjectPooling();
            Pause();
            hud.SetEndGameUIVisible(true, false);
        }

        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f;
        }

        public void Lose()
        {
            if (isGameOver)
                return;

            AudioManager.Instance.Play("lose");
            isGameOver = true;
            ResetObjectPooling();
            Pause();
            lifeCount--;
            hud.SetLifeCount(lifeCount);
            hud.SetEndGameUIVisible(true, true);
        }

        void Restart()
        {
            if (lifeCount == 0)
            {
                Reset();
                ResetObjectPooling();
                LevelManager.Instance.LoadFirstLevel();
            }
            else
            {
                LevelManager.Instance.RestartCurrentLevel();
            }
        }

        public int GetLifeCount()
        {
            return lifeCount;
        }
        public int GetMaxLifeCount()
        {
            return maxLifeCount;
        }
        public void AddLifeCount()
        {
            lifeCount++;
            hud.SetLifeCount(lifeCount);
        }
        public void LoseLifeCount()
        {
            lifeCount-=2;
            hud.SetLifeCount(lifeCount);
        }
    }
}