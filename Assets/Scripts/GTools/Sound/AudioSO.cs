using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GTools.Audio
{
    [CreateAssetMenu(fileName = "Audio SO", menuName = "Audio")]
    public class AudioSO : ScriptableObject
    {
        public AudioClip clip;
        public AudioSource audioSourceConfig;
        public bool oneShot;
    }
}
