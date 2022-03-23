using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableData
{
    [System.Serializable]
    public struct AudioEventData
    {
        public AudioSource source;
        public SDBool triggerEvent;
        public SDAudioSource clipDataEvent;
    }

    [CreateAssetMenu(menuName = "ScriptableData/Data/AudioEventData", order = 146)]
    public class SDAudioEventData : ScriptableData<AudioEventData> { }
}
