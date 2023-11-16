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

    // Start is called before the first frame update
    void Start()
    {
        tree = GetComponent<TreeReader>().treeData.tree;
        loadNode(node);
    }

    private void loadNode(int n)
    {
        NodeData node = tree[n];

        
        foreach (Window window in node.windows)
        {
            GameObject go = null;
            switch (window.type)
            {
                case 0:
                    //go = loadTextWindow(window);
                    break;
                case 1:
                    go = loadImageWindow(window);
                    break;
                case 2:
                    break;
            }

            if (go != null)
                go.transform.position = new Vector2(window.initX, window.initY);
        }
    }

    private GameObject loadTextWindow(Window window)
    {
        GameObject instantiatedPrefab = Instantiate(textWindowPrefab);
        TextMeshProUGUI textMeshPro = instantiatedPrefab.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = window.text;
        return instantiatedPrefab;
    }

    private GameObject loadImageWindow(Window window)
    {
        GameObject instantiatedPrefab = Instantiate(imageWindowPrefab);
        Image image = instantiatedPrefab.GetComponentInChildren<Image>();
        image.sprite = Resources.Load<Sprite>("Images/" + window.file);
        return instantiatedPrefab;
    }

}


