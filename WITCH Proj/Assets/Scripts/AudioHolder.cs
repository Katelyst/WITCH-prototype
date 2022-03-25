using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableData;


//WIP
public class AudioHolder : MonoBehaviour
{
    [SerializeField]
    private List<AudioEventData> audioClipEvents = new List<AudioEventData>();

    [SerializeField]
    private Dictionary<int, AudioEventData> audioClipsDict = new Dictionary<int, AudioEventData>();

    private void Awake()
    {
        foreach(AudioEventData aed in audioClipEvents)
        {
            aed.triggerEvent.OnValueChangedEvent += AssignEvent;
        }
    }

    private void AssignEvent(bool aed)
    {
        //external object has said this sound effect should be played
        audioClipEvents[0].clipDataEvent.Invoke(audioClipEvents[0].source);
    }

}

public class AudioEventObject : ScriptableObject
{

}
