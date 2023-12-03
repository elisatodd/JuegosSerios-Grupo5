using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> myWindows;

    [SerializeField]
    private Button myButton;

    public void AddWindow(GameObject window)
    {
        myWindows.Add(window);  
    }

    public void ToggleWindows()
    {
        foreach (var win in myWindows)
        {
            win.SetActive(!win.activeInHierarchy);
        }
    }

    public void HideWindows()
    {
        foreach (var win in myWindows)
        {
            win.SetActive(false);
        }
    }

    public bool hasWindow(GameObject o)
    {
        return myWindows.Contains(o);
    }


}
