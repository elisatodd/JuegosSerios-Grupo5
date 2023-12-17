using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    [SerializeField]
    AudioClip[] keySounds;

    List<AudioSource> audioSourcePool;

    [SerializeField]
    [Range(0f, 1f)] // Rango para el pitch: mínimo 0.5, máximo 2.0
    private float minPitch = 0.5f;

    [SerializeField]
    [Range(1f, 2f)] // Rango para el pitch: mínimo 0.5, máximo 2.0
    private float maxPitch = 2.0f;

    [SerializeField]
    [Range(0f, 1f)] // Rango para el volumen: mínimo 0.0, máximo 1.0
    private float minVolume = 0.0f;

    [SerializeField]
    [Range(1f, 2f)] // Rango para el volumen: mínimo 0.0, máximo 1.0
    private float maxVolume = 1.0f;

    [SerializeField]
    int charsPerSound;

    int charsCount;

    [SerializeField]
    private TMP_Text textoUI;

    [TextArea(10, 10)]
    [SerializeField]
    private string textoCompleto;

    [SerializeField]
    private float velocidadEscritura = 0.1f;

    private string textoActual = "";
    private float tiempoPasado = 0f;

    [System.Serializable]
    public class FinishEvent : UnityEvent<object>
    {
        public object value;
    }
    [SerializeField]
    private FinishEvent finishEvent = new();


    private void Start()
    {
        audioSourcePool = new List<AudioSource>();

        textoUI.text = "";
    }

    private void Update()
    {
        tiempoPasado += Time.deltaTime;

        if (tiempoPasado > velocidadEscritura)
        {
            tiempoPasado = 0f;

            if (textoActual.Length < textoCompleto.Length)
            {
                char nextChar = textoCompleto[textoActual.Length];

                textoActual += nextChar;
                textoUI.text = textoActual;

                if (nextChar + "" != " ")
                {
                    if (charsCount % charsPerSound == 0)
                    {
                        AudioSource audioSource = getAudioSource();
                        audioSource.clip = keySounds[Random.Range(0, keySounds.Length)];
                        audioSource.pitch = Random.Range(minPitch, maxPitch);
                        audioSource.volume = Random.Range(minVolume, maxVolume);
                        audioSource.Play();
                    }
                    charsCount++;
                }           
            }
            else
            {
                finishEvent.Invoke(finishEvent.value);
            }
        }
    }

    private AudioSource getAudioSource()
    {
        int i = 0;
        while (i < audioSourcePool.Count && audioSourcePool[i].isPlaying)
            i++;

        if (i < audioSourcePool.Count)
            return audioSourcePool[i];
        else
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSourcePool.Add(audioSource);
            return audioSource;
        }
    }
}
