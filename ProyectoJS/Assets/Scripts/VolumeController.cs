using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private float initialVolume = 1.0f;
    private float volumeIncrement = 0.1f;

    [SerializeField]
    Sprite volumeOn;

    [SerializeField]
    Sprite volumeOff;

    Image image;
    ButtonSound buttonSound;

    void Start()
    {
        image = GetComponent<Image>();
        buttonSound = GetComponent<ButtonSound>();

        if (GameManager.Instance().IsMuted())
            image.sprite = volumeOff;
    }

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        AudioListener.volume = volume;
    }

    public void ToggleMute()
    {
        if (GameManager.Instance().ToggleMute())
        {
            buttonSound.enabled = false;
            SetVolume(0);
            image.sprite = volumeOff;
        }

        else
        {
            SetVolume(initialVolume);
            image.sprite = volumeOn;
            buttonSound.enabled = true;
            buttonSound.playPonterDownSound();
        }
            
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
