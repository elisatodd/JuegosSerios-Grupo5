using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    AudioSource pointerDownSound;
    AudioSource pointerUpSound;

    public void Start()
    {
        // Cargar los clips de audio desde la carpeta Resources
        AudioClip pointerDownClip = Resources.Load<AudioClip>("SFX/buttonPressed");
        AudioClip pointerUpClip = Resources.Load<AudioClip>("SFX/buttonRealease");

        // Asegúrate de tener AudioSource en este GameObject
        pointerDownSound = gameObject.AddComponent<AudioSource>();
        pointerUpSound = gameObject.AddComponent<AudioSource>();

        pointerDownSound.playOnAwake = false;
        pointerUpSound.playOnAwake = false;

        // Asignar los clips a los AudioSources
        pointerDownSound.clip = pointerDownClip;
        pointerUpSound.clip = pointerUpClip;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownSound.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerUpSound.Play();
    }

    public void playPonterDownSound()
    {
        pointerDownSound.Play();
    }
}
