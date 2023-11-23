using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
public class ClickDrag : MonoBehaviour
{
    public static int currentLayerOrder = 1;

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

            transform.position = new Vector2(mousePos.x + offset.x, mousePos.y + offset.y);
        }
    }
}
