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

    [Header("Animaciones de las ventanas")]
    [SerializeField] private static float minimizeDuration = 0.25f;
    [SerializeField] private static float maximizeDuration = 0.25f;
    [SerializeField] private static float targetYPosition = -5;

    public void Clear()
    {
        myWindows.Clear();
    }
    public void AddWindow(GameObject window)
    {
        myWindows.Add(window);

        ClickDrag cd = gameObject.GetComponent<ClickDrag>();
        if (cd != null)
        {
            cd.SetSize(window.transform.localScale);
            cd.SetScreenPosition(window.transform.position);
        }
    }

    public void ToggleWindows()
    {
        foreach (var win in myWindows)
        {
            if (win.activeInHierarchy)
            {
                StartCoroutine(MinimizeObject(win));
                win.SetActive(false);
            }
            else
            {
                win.SetActive(true);
                StartCoroutine(MaximizeObject(win));
            }
        }
    }

    public void HideWindows()
    {
        foreach (var win in myWindows)
        {
            if (win.activeInHierarchy)
            {
                StartCoroutine(MinimizeObject(win));
                win.SetActive(false);
            }
        }
    }

    public bool hasWindow(GameObject o)
    {
        return myWindows.Contains(o);
    }

    public static IEnumerator MaximizeObject(GameObject obj) // debe tener ClickDrag
    {
        ClickDrag cd = obj.GetComponent<ClickDrag>();

        Vector3 originalPosition = obj.transform.position;
        Vector3 targetScale;
        Vector3 targetPosition;
        if (cd != null)
        {
            targetScale = cd.GetSize();
            targetPosition = cd.GetScreenPosition();
        }
        else
        {
            targetScale = obj.transform.localScale;
            targetPosition = obj.transform.localPosition;
        }

        float elapsedTime = 0f;

        while (elapsedTime < maximizeDuration)
        {
            obj.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / maximizeDuration);
            obj.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / minimizeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Asegurarse de que el objeto tenga la escala final exacta
        obj.transform.localScale = targetScale;
        obj.transform.localPosition = targetPosition;
    }

    public static IEnumerator MinimizeObject(GameObject obj)
    {
        ClickDrag cd = obj.GetComponent<ClickDrag>();

        Vector3 originalScale;
        Vector3 originalPosition;
        if (cd != null)
        {
            originalScale = cd.GetSize();
            originalPosition = cd.GetScreenPosition();
        }
        else
        {
            originalScale = obj.transform.localScale;
            originalPosition = obj.transform.localPosition;
        }

        Vector3 targetScale = Vector3.zero;
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
}
