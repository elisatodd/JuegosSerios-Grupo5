using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    List<NodeData> tree;
    int node = 0;

    [Header("Valores base")]
    [SerializeField]
    Image background;
    [SerializeField]
    GameObject textWindowPrefab;
    [SerializeField]
    GameObject imageWindowPrefab;
    [SerializeField]
    GameObject choiceWindowPrefab;

    List<GameObject> nodeData;

    [Header("Animación de minimizar")]
    [SerializeField] private float minimizeDuration = 0.25f;
    [SerializeField] private float targetYPosition = -5; 

    void Start()
    {
        nodeData = new List<GameObject>();
        tree = GetComponent<TreeReader>().readTree();
        StartCoroutine(loadNode(node));
    }

    private IEnumerator loadNode(int n)
    {
        foreach (GameObject gameObject in nodeData)
        {
            yield return StartCoroutine(MinimizeObject(gameObject));
            Destroy(gameObject);
        }
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

    IEnumerator MinimizeObject(GameObject obj)
    {
        Vector3 originalScale = obj.transform.localScale;
        Vector3 targetScale = Vector3.zero;

        Vector3 originalPosition = obj.transform.position;
        Vector3 targetPosition = new Vector3(originalPosition.x, targetYPosition, originalPosition.z);

        float elapsedTime = 0f;

        while (elapsedTime < minimizeDuration)
        {
            obj.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / minimizeDuration);
            obj.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / minimizeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que el objeto tenga la escala final y posición exactas
        obj.transform.localScale = targetScale;
        obj.transform.position = targetPosition;
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
            buttons[i].onClick.AddListener(() => StartCoroutine(loadNode(dst)));

            TextMeshProUGUI textMeshPro = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = window.choices[i].text;        
        }

        return instantiatedPrefab;
    }

}


