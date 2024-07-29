using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ZIndexUI : MonoBehaviour
{
    public int ZIndex = 0;
    private int lastZIndex;

    private Transform lastParent;

    private int lastChildNumber = 0;

    void Start()
    {
        lastZIndex = ZIndex;
        transform.SetSiblingIndex(ZIndex);
    }

    void Update()
    {
        if (ZIndex != lastZIndex)
        {
            Start();
        }
        if (transform.parent != lastParent)
        {
            lastParent = transform.parent;
            Start();
        }
        if (lastChildNumber != transform.parent.childCount)
        {
            lastChildNumber = transform.parent.childCount;
            Start();
        }
    }

}
