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
        [SerializeField] DifficultyManager difficultyManager;

        [Header("UI")]
        [SerializeField] HUD hud;

        [Header("BGM")]
        [SerializeField] AudioSource bgm;

        bool isPaused;
        bool isGameOver;

        protected override void InitAfterAwake()
        {
            Pause();
            SetupHUD();
            Reset();
            difficultyManager = DifficultyManager.Instance;
        }
        
        void Reset()
        {

            lifeCount = maxLifeCount;
            hud.SetLifeCount(lifeCount);
            hud.SetLevel(LevelManager.Instance.CurrentLevel);         
            ItemEffect.Instance.isPoisoning = false;
            ItemEffect.Instance.PoisonUI.SetActive(false);
            ResetObjectPooling();
        }
        void ResetObjectPooling()
        {
            SetItemPoolFromDifficulty();
            ObjectPooling.Instance.Cleanup();
            ObjectPooling.Instance.UseSpawnItemsCoroutine();
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
            Reset();
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
            Reset();
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
                LevelManager.Instance.LoadFirstLevel();
            }
            else
            {   
                Reset();
                LevelManager.Instance.RestartCurrentLevel();
            }
        }
        public void SetItemPoolFromDifficulty()
        {
            // Define a class or dictionary to map difficulty levels to item pool limits
        Dictionary<int, ItemPoolLimits> difficultyToLimits = new Dictionary<int, ItemPoolLimits>
        {
        { 0, new ItemPoolLimits(0, 0, 0) }, 
        { 1, new ItemPoolLimits(2, 0, 1) },
        { 2, new ItemPoolLimits(1, 1, 1) },
        { 3, new ItemPoolLimits(0, 1, 0) },
        };

        int difficultyLevel = difficultyManager.DifficultyLevel;

        if (difficultyToLimits.TryGetValue(difficultyLevel, out ItemPoolLimits limits))
        {
        ObjectPooling.Instance.limitArmor = limits.Armor;
        ObjectPooling.Instance.limitPoison = limits.Poison;
        ObjectPooling.Instance.limitHeart = limits.Heart;
        }
        else
        {   
        // Handle the case where the difficulty level is not found (set defaults or show an error)
        Debug.LogError("Difficulty level not found: " + difficultyLevel);
        }

        // Reset other item pool settings
        ObjectPooling.Instance.Armor = 0;
        ObjectPooling.Instance.Poison = 0;
        ObjectPooling.Instance.Heart = 0;
        ObjectPooling.Instance.StopSpawnItem = false;
        Debug.Log("Item set.");
    }

        public int GetDifficultylevel()
        {
            int difficultyLevel = difficultyManager.DifficultyLevel;

            return difficultyLevel;
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
    public class ItemPoolLimits
    {
    public int Armor { get; }
    public int Poison { get; }
    public int Heart { get; }

    public ItemPoolLimits(int armor, int poison, int heart)
    {
        Armor = armor;
        Poison = poison;
        Heart = heart;
    }
    }
    }