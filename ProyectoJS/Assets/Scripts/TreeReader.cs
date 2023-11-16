using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Choice
{
    public string text;
    public int dst;
}

[Serializable]
public class Window
{
    public float initX;
    public float initY;
    public int type;
    public string text; // Opcional, solo si es tipo 0
    public string file; // Opcional, solo si es tipo 1
    public List<Choice> choices; // Opcional, solo si es tipo 2
}

[Serializable]
public class NodeData
{
    public string background;
    public List<Window> windows;
}

[Serializable]
public class TreeData
{
    public List<NodeData> tree;
}

public class TreeReader : MonoBehaviour
{
    public TextAsset jsonFile; // Asigna el archivo JSON desde el Editor
    public TreeData treeData;

    void Awake()
    {
        if (jsonFile != null)
        {
            string jsonString = jsonFile.text;
            treeData = JsonUtility.FromJson<TreeData>(jsonString);

            // Accede a los datos deserializados
            foreach (NodeData scene in treeData.tree)
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
                        Debug.Log("Type 1 - Path: " + window.file);
                    }
                    else if (window.type == 2)
                    {
                        foreach (Choice option in window.choices)
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
