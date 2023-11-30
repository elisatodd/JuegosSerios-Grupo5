using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
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
                textoActual += textoCompleto[textoActual.Length];
                textoUI.text = textoActual;
            }
            else
            {
                finishEvent.Invoke(finishEvent.value);
            }
        }
    }
}
