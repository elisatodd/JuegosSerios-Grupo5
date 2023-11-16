using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private float initialVolume = 1.0f;
    private float volumeIncrement = 0.1f;
    private bool mute = false;

    void Start()
    {
        SetVolume(initialVolume);
    }

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        AudioListener.volume = volume;
    }

    public void ToggleMute()
    {
        Debug.Log("Mute");

        mute = !mute;

        if (mute)
            SetVolume(0);
        else
            SetVolume(initialVolume);
    }

    public void LowerVolume()
    {
        float currentVolume = AudioListener.volume;
        SetVolume(currentVolume - volumeIncrement);
    }

    public void IncrementVolume()
    {
        float currentVolume = AudioListener.volume;
        SetVolume(currentVolume + volumeIncrement);
    }
}
