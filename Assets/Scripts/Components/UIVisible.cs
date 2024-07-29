using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIVisible : MonoBehaviour
{
    public bool visible = true;
    private bool lastVisible = true;

    void OnValidate()
    {
        if (lastVisible != visible)
        {
            lastVisible = visible;

            if (visible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }

    // Call this method to hide the UI element
    public void Hide()
    {
        // Disable CanvasRenderer components
        CanvasRenderer[] canvasRenderers = gameObject.GetComponentsInChildren<CanvasRenderer>();
        foreach (CanvasRenderer canvasRenderer in canvasRenderers)
        {
            canvasRenderer.SetAlpha(0.0f);
        }

        // Disable interaction
        Graphic[] graphics = gameObject.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = false;
        }
    }

    // Call this method to show the UI element again
    public void Show()
    {
        // Enable CanvasRenderer components
        CanvasRenderer[] canvasRenderers = gameObject.GetComponentsInChildren<CanvasRenderer>();
        foreach (CanvasRenderer canvasRenderer in canvasRenderers)
        {
            canvasRenderer.SetAlpha(1.0f);
        }

        // Enable interaction
        Graphic[] graphics = gameObject.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = true;
        }
    }
}
