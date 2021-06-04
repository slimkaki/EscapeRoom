using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class soundSlider : MonoBehaviour{
    public AudioMixer audioMixer;
    public AudioMixer soundFxMixer;
    // public float volume = 0;

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetSoundFx (float volume)
    {
        soundFxMixer.SetFloat("volume", volume);
    }
    


}
