using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    public TMP_Text textoUI;

    [TextArea(10, 10)]
    public string textoCompleto;

    public float velocidadEscritura = 0.1f;

    private string textoActual = "";
    private float tiempoPasado = 0f;

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
        }
    }
}
