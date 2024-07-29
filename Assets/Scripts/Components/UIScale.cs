using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class UIScale : MonoBehaviour
{
    public Vector2 relativePosition;
    public Vector2 relativeSize = new Vector2(0.2f, 0.1f);
    public float aspectRatio = 0;
    public bool doPosition = true;
    public RectTransform overridenParentRectTransform = null;

    private RectTransform rectTransform;
    private RectTransform parentRectTransform;

    private RectTransform lastOverridenParentRectTransform = null;
    private Vector2 lastParentSize;
    private Vector2 lastRelativePosition;
    private Vector2 lastRelativeSize;
    private float lastAspectRatio;
    private Vector2 lastPivot;

    void Awake()
    {
        Init();
        UpdateScaleAndPosition();
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;

        if (parentRectTransform == null)
        {
            return;
        }

        if (relativePosition == Vector2.zero)
        {
            InitializeRelativePosition();
        }

        lastRelativePosition = relativePosition;
        lastRelativeSize = relativeSize;
        lastAspectRatio = aspectRatio;
    }

    void Update()
    {
        if (parentRectTransform == null) return;
        //overriden parent changed?
        if (overridenParentRectTransform != lastOverridenParentRectTransform)
        {
            lastOverridenParentRectTransform = overridenParentRectTransform;
            parentRectTransform = overridenParentRectTransform;
            UpdateScaleAndPosition();
        }

        //parent changed and not overriden?
        if (parentRectTransform != rectTransform.parent as RectTransform && overridenParentRectTransform == null)
        {
            parentRectTransform = rectTransform.parent as RectTransform;

            if (parentRectTransform == null)
            {
                return;
            }

            lastParentSize = parentRectTransform.rect.size;
            UpdateScaleAndPosition();
        }

        //parent size changed?
        if (lastParentSize != parentRectTransform.rect.size)
        {
            lastParentSize = parentRectTransform.rect.size;
            UpdateScaleAndPosition();
        }

        //properties changed?
        if (relativePosition != lastRelativePosition || relativeSize != lastRelativeSize || aspectRatio != lastAspectRatio)
        {
            lastRelativePosition = relativePosition;
            lastRelativeSize = relativeSize;
            lastAspectRatio = aspectRatio;
            UpdateScaleAndPosition();
        }

        //anchor changed?
        if (rectTransform.pivot != lastPivot)
        {
            lastPivot = rectTransform.pivot;
            UpdateScaleAndPosition();
        }
    }

    //calculate first properties based on unity's ui properties
    void InitializeRelativePosition()
    {
        // Initialize relativePosition based on the current anchored position and parent size
        float parentWidth = parentRectTransform.rect.width;
        float parentHeight = parentRectTransform.rect.height;

        relativePosition = new Vector2(
            (rectTransform.anchoredPosition.x + parentWidth * 0.5f) / parentWidth,
            (rectTransform.anchoredPosition.y + parentHeight * 0.5f) / parentHeight
        );
        relativeSize = new Vector2(rectTransform.rect.width / parentWidth, rectTransform.rect.height / parentHeight);
        CalculateAspectRatio();
    }

    public void CalculateAspectRatio()
    {
        aspectRatio = rectTransform.rect.width / rectTransform.rect.height;
    }

    public void UpdateScaleAndPosition()
    {
        if (rectTransform == null || parentRectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
            parentRectTransform = rectTransform.parent as RectTransform;
            if (parentRectTransform == null)
            {
                return;
            }
        }

        float parentWidth = parentRectTransform.rect.width;
        float parentHeight = parentRectTransform.rect.height;
        float width = parentWidth * relativeSize.x;
        float height = parentHeight * relativeSize.y;

        if (aspectRatio != 0)
        {
            if ((float)width / height > aspectRatio)
                width = height * aspectRatio;
            else
                height = width / aspectRatio;
        }

        rectTransform.sizeDelta = new Vector2(width, height);

        if (doPosition == true)
        {
            rectTransform.anchoredPosition = new Vector2(parentWidth * relativePosition.x - parentWidth * 0.5f, parentHeight * relativePosition.y - parentHeight * 0.5f);
        }
    }
}
