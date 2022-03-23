using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableData;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayerOnEvent : MonoBehaviour
{
    [Header("Audio Source that will play")]
    [SerializeField][Tooltip("If this does not get set in editor, it assigns the audio source already on this object")]
    private AudioSource audioSource;
    [Header("Trigger Conditionals")]
    [SerializeField][Tooltip("Assign SDBool data to listen to, if you change the value to true somewhere else, it gets triggered here and the sound will play")]
    private SDBool playEvent;
    [SerializeField][Tooltip("Should the audio play when the PlayEvent value is true, false, or both?")]
    private bool triggerOnFalse = false, triggerOnTrue = true;

    private void Awake()
    {
        if(!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if(!playEvent)
        {
            string objName = transform.parent ? gameObject.name + ", child of: " + transform.parent.gameObject.name : gameObject.name;
            Debug.LogWarning("Matthijs, you didn't assign the play event in editor on obj: " + objName);
            return;
        }
        playEvent.OnValueChangedEvent += PlayAudio;
    }

    private void PlayAudio(bool b)
    {
        if (triggerOnFalse && b == false)
        {
            audioSource.Play();
        }
        else if(triggerOnTrue && b == true)
        {
            audioSource.Play();
        }
    }
}
