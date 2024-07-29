using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class gameStatDisplay : MonoBehaviour
{
    private List<Color> modifColors = new List<Color>
    {
        new Color(221/255f, 86/255f, 85/255f),
        new Color(157/255f, 133/255f, 173/255f),
        new Color(89/255f, 221/255f, 85/255f)
    };
    private TextMeshProUGUI textUI;
    private Image iconUI;

    [SerializeField] private string text;
    [SerializeField] private Sprite icon;

    public float normalV;
    public float value;
    private float lastValue = -1;
    [SerializeField] private bool leftToRight = true;


    void Start()
    {
        textUI = transform.Find("text").GetComponent<TextMeshProUGUI>();
        iconUI = transform.Find("icon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastValue == value) return;

        lastValue = value;

        //recolor based on value
        if (leftToRight)
        {
            if (value > normalV)
            {
                textUI.color = modifColors[2];
                iconUI.color = modifColors[2];
            }
            else if (value < normalV)
            {
                textUI.color = modifColors[0];
                iconUI.color = modifColors[0];
            }
            else
            {
                textUI.color = modifColors[1];
                iconUI.color = modifColors[1];
            }
        }
        else
        {
            if (value < normalV)
            {
                textUI.color = modifColors[2];
                iconUI.color = modifColors[2];
            }
            else if (value > normalV)
            {
                textUI.color = modifColors[0];
                iconUI.color = modifColors[0];
            }
            else
            {
                textUI.color = modifColors[1];
                iconUI.color = modifColors[1];
            }
        }

        //replace value in format string
        textUI.text = text.Replace("0", value.ToString());
        iconUI.GetComponent<Image>().sprite = icon;
    }
}
