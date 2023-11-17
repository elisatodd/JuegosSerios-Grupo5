using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Window
{
    public float initX;
    public float initY;
}

[Serializable]
public class TextWindow : Window
{
    public string text;
}

[Serializable]
public class ImageWindow : Window
{
    public string file;
}

[Serializable]
public class ChoiceWindow : Window
{
    public List<Choice> choices; // Opcional, solo si es tipo 2
}

[Serializable]
public class Choice
{
    public string text;
    public int dst;
}

[Serializable]
public class NodeData
{
    public string background;
    public List<TextWindow> textWindows;
    public List<ImageWindow> imageWindows;
    public ChoiceWindow choiceWindow;
}

[Serializable]
public class TreeData
{
    public List<NodeData> tree;
}

public class TreeReader : MonoBehaviour
{
    public TextAsset jsonFile; 

    public List<NodeData> readTree()
    {
        string jsonString = jsonFile.text;
        return JsonUtility.FromJson<TreeData>(jsonString).tree;
    }

}
