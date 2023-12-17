using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    List<NodeData> tree;
    int node = 0;

    [SerializeField]
    private List<WindowManager> windowManagers = new List<WindowManager>();

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


    void Start()
    {
        nodeData = new List<GameObject>();
        tree = GetComponent<TreeReader>().readTree();
        StartCoroutine(loadNode(node));
    }

    private IEnumerator loadNode(int n)
    {
        foreach(WindowManager w in windowManagers)
        {
            w.Clear();
        }

        foreach (GameObject gameObject in nodeData)
        {
            yield return StartCoroutine(WindowManager.MinimizeObject(gameObject));
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
            if (windowManagers[0].hasWindow(gameObject))
            {
                gameObject.SetActive(true);
                yield return StartCoroutine(WindowManager.MaximizeObject(gameObject));
            }
        }

        nodeData.AddRange(hiddenWindows);
    }

    private GameObject loadTextWindow(TextWindow window)
    {
        GameObject instantiatedPrefab = Instantiate(textWindowPrefab);
        instantiatedPrefab.SetActive(false);

        TextMeshProUGUI textMeshPro = instantiatedPrefab.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = window.text;

        instantiatedPrefab.transform.position = new Vector2(window.initX, window.initY);

        windowManagers[window.category].AddWindow(instantiatedPrefab);

        return instantiatedPrefab;
    }

    private GameObject loadImageWindow(ImageWindow window)
    {
        GameObject instantiatedPrefab = Instantiate(imageWindowPrefab);
        instantiatedPrefab.SetActive(false);

        Image image = instantiatedPrefab.GetComponentInChildren<Image>();
        image.sprite = Resources.Load<Sprite>("Images/" + window.file);

        instantiatedPrefab.transform.position = new Vector2(window.initX, window.initY);

        windowManagers[window.category].AddWindow(instantiatedPrefab);

        return instantiatedPrefab;
    }

    private GameObject loadChoiceWindow(ChoiceWindow window)
    {
        GameObject instantiatedPrefab = Instantiate(choiceWindowPrefab);

        instantiatedPrefab.SetActive(false);

        Button[] buttons = instantiatedPrefab.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI textMeshPro = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = window.choices[i].text;

            int dst = window.choices[i].dst;
            if (dst >= 0)
                buttons[i].onClick.AddListener(() => StartCoroutine(loadNode(dst)));
            else
            {
                SceneChanger sceneChanger = buttons[i].gameObject.AddComponent<SceneChanger>();
                sceneChanger.setScene("MainMenu");
                for (int j = i + 1; j < buttons.Length; j++)
                    Destroy(buttons[j].gameObject);
                break;            
            }           
        }

        instantiatedPrefab.transform.position = new Vector2(window.initX, window.initY);

        windowManagers[window.category].AddWindow(instantiatedPrefab);

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
            StartCoroutine(WindowManager.MaximizeObject(window));
        }
        else
        {        
            StartCoroutine(WindowManager.MinimizeObject(window));
            //window.SetActive(false);
        }
    }

}


