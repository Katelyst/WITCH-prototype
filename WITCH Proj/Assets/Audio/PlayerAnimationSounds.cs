using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSounds : MonoBehaviour
{

    AudioSource AnimationSoundPlayer;
    [SerializeField]
    AudioClip FootstepClip;
    [SerializeField]
    AudioClip JumpClip;

    // Start is called before the first frame update
    void Start()
    {
        AnimationSoundPlayer = GetComponent<AudioSource>();
        // FootstepClip = Resources.Load("Assets/Resources/AudioResources/Witch_Player_Footstep_Stone01.wav") as AudioClip;
        // JumpClip = Resources.Load("Assets/Resources/AudioResources/Witch_Hop.wav") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayFootstepAudio()
    {
        AnimationSoundPlayer.clip = FootstepClip;
        AnimationSoundPlayer.volume = Random.Range(0.1f,0.3f);
        AnimationSoundPlayer.pitch = Random.Range(0.8F,1.2F);
        AnimationSoundPlayer.Play();
    }

    void PlayJumpAudio()
    {
        AnimationSoundPlayer.clip = JumpClip;
        AnimationSoundPlayer.volume = Random.Range(0.8f,1.1f);
        AnimationSoundPlayer.pitch = Random.Range(0.9F,1.1F);
        AnimationSoundPlayer.Play();
    }
}
