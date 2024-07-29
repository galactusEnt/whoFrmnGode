using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Button))]

public class UIButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool doHover = true;
    public float factor = 1;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!doHover || rectTransform == null) return;
        // Grow to 1.1x when mouse hovers
        rectTransform.DOScale(1.1f * factor, 0.1f).SetEase(Ease.Linear);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!doHover || rectTransform == null) return;
        // Revert to original size when mouse leaves
        rectTransform.DOScale(1f, 0.1f).SetEase(Ease.Linear);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (rectTransform == null) return;
        // Shrink to 0.9x when mouse button is pressed
        rectTransform.DOScale(0.9f * factor, 0.1f).SetEase(Ease.Linear);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (rectTransform == null) return;
        // Revert to original size when mouse button is released
        rectTransform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnDestroy()
    {
        rectTransform.DOKill();
    }

}
