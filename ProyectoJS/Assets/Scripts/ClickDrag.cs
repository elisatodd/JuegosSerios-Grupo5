using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
public class ClickDrag : MonoBehaviour
{
    public static int currentLayerOrder = 2;
    
    private static int minYPos = -4;

    private BoxCollider2D boxCollider;
    private bool dragging = false;
    private Vector2 offset;

    private int initialOrderInLayer;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Canvas myCanvas;

    private void Awake()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        initialOrderInLayer = currentLayerOrder;
        currentLayerOrder++;

        spriteRenderer.sortingOrder = initialOrderInLayer;
        myCanvas.sortingOrder = initialOrderInLayer;
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (boxCollider.OverlapPoint(mousePos))
        {
            dragging = true;
            offset = transform.position - mousePos;
            spriteRenderer.sortingOrder = (currentLayerOrder + 1);
            myCanvas.sortingOrder = spriteRenderer.sortingOrder;
            currentLayerOrder = (currentLayerOrder + 1) % 32767;
        }
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    private void Update()
    {
        if (dragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

           // transform.position = new Vector2(mousePos.x + offset.x, mousePos.y + offset.y);

            // Limitar el movimiento hacia abajo
            float newY = mousePos.y + offset.y;
            if (newY < minYPos)
            {
                newY = minYPos;
            }

            transform.position = new Vector2(mousePos.x + offset.x, newY);
        }
    }
}
