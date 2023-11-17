using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    List<NodeData> tree;
    int node = 0;

    [SerializeField]
    Image background;
    [SerializeField]
    GameObject textWindowPrefab;
    [SerializeField]
    GameObject imageWindowPrefab;
    [SerializeField]
    GameObject choiceWindowPrefab;

    List<GameObject> nodeData;

    // Start is called before the first frame update
    void Start()
    {
        nodeData = new List<GameObject>();
        tree = GetComponent<TreeReader>().readTree();
        loadNode(node);
    }

    private void loadNode(int n)
    {
        foreach (GameObject gameObject in nodeData)
            Destroy(gameObject);
        nodeData.Clear();

        Debug.Log("Nodo actual: " + n);

        NodeData node = tree[n];
        GameObject go;

        background.sprite = Resources.Load<Sprite>("Backgrounds/" + node.background);

        foreach (TextWindow window in node.textWindows)
        {
            go = loadTextWindow(window);
            Debug.Log(window.initX);
            go.transform.position = new Vector2(window.initX, window.initY);
            nodeData.Add(go);
        }

        foreach (ImageWindow window in node.imageWindows)
        {
            go = loadImageWindow(window);
            Debug.Log(window.initX);
            go.transform.position = new Vector2(window.initX, window.initY);
            nodeData.Add(go);
        }

        go = loadChoiceWindow(node.choiceWindow);
        go.transform.position = new Vector2(node.choiceWindow.initX, node.choiceWindow.initY);
        nodeData.Add(go);
    }

    private GameObject loadTextWindow(TextWindow window)
    {
        GameObject instantiatedPrefab = Instantiate(textWindowPrefab);
        TextMeshProUGUI textMeshPro = instantiatedPrefab.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = window.text;
        return instantiatedPrefab;
    }

    private GameObject loadImageWindow(ImageWindow window)
    {
        GameObject instantiatedPrefab = Instantiate(imageWindowPrefab);
        Image image = instantiatedPrefab.GetComponentInChildren<Image>();
        image.sprite = Resources.Load<Sprite>("Images/" + window.file);
        return instantiatedPrefab;
    }

    private GameObject loadChoiceWindow(ChoiceWindow window)
    {
        GameObject instantiatedPrefab = Instantiate(choiceWindowPrefab);

        Button[] buttons = instantiatedPrefab.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int dst = window.choices[i].dst;
            buttons[i].onClick.AddListener(() => loadNode(dst));

            TextMeshProUGUI textMeshPro = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = window.choices[i].text;        
        }

        return instantiatedPrefab;
    }

}


