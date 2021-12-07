using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public SoundClip[] clips;
    public bool autoStart;
    public bool debug;
    // Start is called before the first frame update


    private void OnEnable()
    {
        if (audioSource != null) {
            if (SoundController.instance.isMuteEffect)
            {
                audioSource.mute = true;
            }
            else
            {
                audioSource.mute = false;
            }
        }
        if (autoStart)
        {
            PlaySound();
        }
        
        this.registerListener(EventID.MUTE_SOUND_EFFECT, (sender, param) => MuteSound((int)param));
        this.registerListener(EventID.PAUSE_SOUND, (sender, param) => PauseSound((int)param));
    }

    IEnumerator DelayPlaySound(int index)
    {
        yield return new WaitForSeconds(clips[index].delay);
        if (audioSource != null) { 
            audioSource.loop = clips[index].loop;
            audioSource.clip = clips[index].clip;
            audioSource.Play();
        }
    }

    public void PauseSound(int check)
    {
        if (check == 1)
        {
            if (audioSource != null)
            {
                audioSource.UnPause();
            }
        }
        else
        {
            if (audioSource != null)
                audioSource.Pause();
        }
    }

    public void PlaySound()
    {
        if (debug)
        {
            Debug.Log(clips[0].clip.name);
        }
        for (int i = 0; i < clips.Length; i++)
        {
            StartCoroutine(DelayPlaySound(i));
        }
    }

    public void MuteSound(int value)
    {
        if (audioSource != null)
        {
            if (value > 0)
            {
                audioSource.mute = false;
            }
            else
            {
                audioSource.mute = true;
            }
        }
    }
}
