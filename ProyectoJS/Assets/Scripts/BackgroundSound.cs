using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSound : MonoBehaviour
{
    private static BackgroundSound instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (!(scene.name == "MainMenu" || scene.name == "SettingsScene"))
        {
            Destroy(this.gameObject);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }     
    }
}
