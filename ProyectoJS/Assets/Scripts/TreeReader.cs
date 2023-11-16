using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Option
{
    public string text;
    public int dst;
}

[Serializable]
public class Window
{
    public int type;
    public string text; // Opcional, solo si es tipo 0
    public string path; // Opcional, solo si es tipo 1
    public List<Option> options; // Opcional, solo si es tipo 2
}

[Serializable]
public class Scene
{
    public string background;
    public List<Window> windows;
}

[Serializable]
public class TreeData
{
    public List<Scene> tree;
}

public class TreeReader : MonoBehaviour
{
    public TextAsset jsonFile; // Asigna el archivo JSON desde el Editor

    void Start()
    {
        if (jsonFile != null)
        {
            string jsonString = jsonFile.text;
            TreeData treeData = JsonUtility.FromJson<TreeData>(jsonString);

            // Accede a los datos deserializados
            foreach (Scene scene in treeData.tree)
            {
                Debug.Log("Background: " + scene.background);
                foreach (Window window in scene.windows)
                {
                    if (window.type == 0)
                    {
                        Debug.Log("Type 0 - Text: " + window.text);
                    }
                    else if (window.type == 1)
                    {
                        Debug.Log("Type 1 - Path: " + window.path);
                    }
                    else if (window.type == 2)
                    {
                        foreach (Option option in window.options)
                        {
                            Debug.Log("Type 2 - Option: " + option.text + ", Destination: " + option.dst);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Error: No se pudo cargar el archivo JSON.");
        }
    }
}
