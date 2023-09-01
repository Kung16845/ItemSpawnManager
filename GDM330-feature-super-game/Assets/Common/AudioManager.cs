using System;
using System.Collections.Generic;
using PhEngine.Core;
using UnityEngine;

namespace SuperGame
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] List<AudioRecord> recordList = new List<AudioRecord>();
        
        protected override void InitAfterAwake()
        {
            
        }

        public void Play(string id)
        {
            foreach (var record in recordList)
            {
                if (record.id == id)
                {
                    var newAudioSource = Instantiate(audioSource, transform, true);
                    newAudioSource.clip = record.clip;
                    newAudioSource.Play();
                }
            }
        }
    }

    [Serializable]
    public class AudioRecord
    {
        public string id;
        public AudioClip clip;
    }
}