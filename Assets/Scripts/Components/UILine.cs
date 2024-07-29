using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
public class UILine : Graphic
{
    public RectTransform startPoint;
    public RectTransform endPoint;

    [Range(-1f, 1f)] public float curveFactor = 0f;
    [Range(-0.5f, 0.5f)] public float offset = 0f;

    public int segmentCount = 10;
    public float segmentLength = 10f;
    public float segmentWidth = 2f;
    public bool isDotted = false;

#if UNITY_EDITOR
    private void Update()
    {
        if(gameObject.name != startPoint.gameObject.name + " --> " + endPoint.gameObject.name)
            gameObject.name = startPoint.gameObject.name + " --> " + endPoint.gameObject.name;
    }
#endif

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (startPoint == null || endPoint == null || startPoint == endPoint)
            return;

        Vector2 start = startPoint.anchoredPosition;
        Vector2 end = endPoint.anchoredPosition;
        Vector2 mid = Vector2.Lerp(start, end, 0.5f + offset);
        mid += Vector2.Perpendicular(end - start).normalized * Vector2.Distance(start, end) * curveFactor;

        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)(segmentCount - 1);
            Vector2 point = Mathf.Pow(1 - t, 2) * start + 2 * (1 - t) * t * mid + Mathf.Pow(t, 2) * end;
            points.Add(point);
        }

        if (isDotted)
        {
            DrawDottedLine(vh, points);
        }
        else
        {
            DrawSolidLine(vh, points);
        }
    }

    void DrawSolidLine(VertexHelper vh, List<Vector2> points)
    {
        for (int i = 0; i < points.Count - 1; i++)
        {
            DrawLineSegment(vh, points[i], points[i + 1]);
        }
    }

    void DrawDottedLine(VertexHelper vh, List<Vector2> points)
    {
        float distanceCovered = 0f;
        for (int i = 0; i < points.Count - 1; i++)
        {
            float segmentDistance = Vector2.Distance(points[i], points[i + 1]);
            if (distanceCovered + segmentDistance >= segmentLength)
            {
                float remainingDistance = segmentLength - distanceCovered;
                Vector2 newEnd = Vector2.Lerp(points[i], points[i + 1], remainingDistance / segmentDistance);
                DrawLineSegment(vh, points[i], newEnd);
                distanceCovered = 0f;
            }
            else
            {
                distanceCovered += segmentDistance;
            }
        }
    }

    void DrawLineSegment(VertexHelper vh, Vector2 start, Vector2 end)
    {
        Vector2 perpendicular = Vector2.Perpendicular((end - start).normalized) * segmentWidth;
        UIVertex[] verts = new UIVertex[4];

        verts[0].position = start - perpendicular;
        verts[1].position = start + perpendicular;
        verts[2].position = end + perpendicular;
        verts[3].position = end - perpendicular;

        for (int i = 0; i < 4; i++)
        {
            verts[i].color = color;
        }

        vh.AddUIVertexQuad(verts);
    }
}
