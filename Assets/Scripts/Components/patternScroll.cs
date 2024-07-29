using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class patternScroll : MonoBehaviour
{
    [SerializeField] private RawImage rawI;
    [SerializeField] private float x = 0.01f, y = 0.01f;
    [SerializeField] private float repeatNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        rawI.uvRect = new Rect(rawI.uvRect.position, new Vector2(gameObject.GetComponent<RectTransform>().rect.width / 256f * repeatNum, gameObject.GetComponent<RectTransform>().rect.height / 256f * repeatNum));
    }

    // Update is called once per frame
    void Update()
    {

        rawI.uvRect = new Rect(rawI.uvRect.position + new Vector2(x, y) * Time.deltaTime, new Vector2(gameObject.GetComponent<RectTransform>().rect.width / 256f * repeatNum, gameObject.GetComponent<RectTransform>().rect.height / 256f * repeatNum));
    }
}
