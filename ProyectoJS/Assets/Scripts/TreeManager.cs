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
    [SerializeField]
    GameObject iconPrefab;
    [SerializeField]
    Sprite[] icons;

    List<GameObject> nodeData;

    [Header("Animaciones de las ventanas")]
    [SerializeField] private float minimizeDuration = 0.25f;
    [SerializeField] private float maximizeDuration = 0.25f;
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

        NodeData node = tree[n];

        background.sprite = Resources.Load<Sprite>("Backgrounds/" + node.background);

        foreach (TextWindow window in node.textWindows)
            nodeData.Add(loadTextWindow(window));
        
        foreach (ImageWindow window in node.imageWindows)
            nodeData.Add(loadImageWindow(window));
        
        nodeData.Add(loadChoiceWindow(node.choiceWindow));

        List<GameObject> hiddenWindows = new List<GameObject>();
        GameObject win;

        foreach (TextIcon icon in node.textIcons)
        {
            win = loadTextWindow(icon.textWindow);
            hiddenWindows.Add(win);
            nodeData.Add(loadIcon(icon, win));
        }
            
        foreach (ImageIcon icon in node.imageIcons)
        {
            win = loadImageWindow(icon.imageWindow);
            hiddenWindows.Add(win);
            nodeData.Add(loadIcon(icon, win));
        }
            
        foreach (GameObject gameObject in nodeData)
        {
            gameObject.SetActive(true);
            yield return StartCoroutine(MaximizeObject(gameObject));
        }

        nodeData.AddRange(hiddenWindows);
    }

    IEnumerator MaximizeObject(GameObject obj)
    {
        Vector3 originalScale = obj.transform.localScale;
        Vector3 targetScale = originalScale;
        float elapsedTime = 0f;

        while (elapsedTime < maximizeDuration)
        {
            obj.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / maximizeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Asegurarse de que el objeto tenga la escala final exacta
        obj.transform.localScale = targetScale;
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
        instantiatedPrefab.SetActive(false);
        TextMeshProUGUI textMeshPro = instantiatedPrefab.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = window.text;
        instantiatedPrefab.transform.position = new Vector2(window.initX, window.initY);
        return instantiatedPrefab;
    }

    private GameObject loadImageWindow(ImageWindow window)
    {
        GameObject instantiatedPrefab = Instantiate(imageWindowPrefab);
        instantiatedPrefab.SetActive(false);
        Image image = instantiatedPrefab.GetComponentInChildren<Image>();
        image.sprite = Resources.Load<Sprite>("Images/" + window.file);
        instantiatedPrefab.transform.position = new Vector2(window.initX, window.initY);
        return instantiatedPrefab;
    }

    private GameObject loadChoiceWindow(ChoiceWindow window)
    {
        GameObject instantiatedPrefab = Instantiate(choiceWindowPrefab);

        instantiatedPrefab.SetActive(false);

        Button[] buttons = instantiatedPrefab.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int dst = window.choices[i].dst;
            buttons[i].onClick.AddListener(() => StartCoroutine(loadNode(dst)));

            TextMeshProUGUI textMeshPro = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = window.choices[i].text;        
        }

        instantiatedPrefab.transform.position = new Vector2(window.initX, window.initY);

        return instantiatedPrefab;
    }

    private GameObject loadIcon(Icon icon, GameObject window)
    {
        GameObject instantiatedPrefab = Instantiate(iconPrefab);

        instantiatedPrefab.SetActive(false);

        instantiatedPrefab.GetComponentInChildren<Image>().sprite = icons[icon.icon];

        Button button = instantiatedPrefab.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => onIconClick(window));      

        instantiatedPrefab.transform.position = new Vector2(icon.initX, icon.initY);

        return instantiatedPrefab;
    }

    private void onIconClick(GameObject window)
    {
        if (!window.activeSelf)
        {
            window.SetActive(true);
            StartCoroutine(MaximizeObject(window));
        }
        else
        {        
            StartCoroutine(MinimizeObject(window));
            //window.SetActive(false);
        }
    }

}


