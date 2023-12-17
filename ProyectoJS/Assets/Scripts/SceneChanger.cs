using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour, IPointerUpHandler
{
    [SerializeField]
    string sceneName;

    public void OnPointerUp(PointerEventData eventData)
    {
        // Meto delay al cambio de escena para que se escuche el sonido
        Invoke("changeScene", 0.1f);
    }

    private void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void setScene(string sceneName)
    {
        this.sceneName = sceneName;
    }
}
