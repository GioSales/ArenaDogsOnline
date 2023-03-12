using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using GTools.Singletons;

namespace GTools.Audio
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        public AudioSource audioPrefab;
        private ObjectPool<AudioSource> audioPool;

        private void Start()
        {
            audioPool = new ObjectPool<AudioSource>(audioPrefab, 10, 20, true, transform);
        }

        public void PlaySound(AudioSO audioSO, Vector3 audioPos)
        {
            var audioObj = audioPool.GetObject();
            AudioSourceConfig(audioObj, audioSO);
            PlayClip(audioSO, audioObj);
            StartCoroutine(DespawnAudioObj(audioObj));
        }

        private void AudioSourceConfig(AudioSource clipAudSource, AudioSO audioSO)
        {
            clipAudSource.name = audioSO.name;
            clipAudSource.clip = audioSO.clip;
        }

        private void PlayClip(AudioSO audioSO, AudioSource audio)
        {
            if (audioSO.oneShot)
                audio.PlayOneShot(audio.clip);
            else
                audio.Play();
        }

        private IEnumerator DespawnAudioObj(AudioSource audio)
        {
            yield return new WaitForSeconds(audio.clip.length);
            audioPool.ReleaseObject(audio);
        }


    }

}

