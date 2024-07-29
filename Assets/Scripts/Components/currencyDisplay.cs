using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class currencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cStarText, vStarText;

    private GameManager gameManager;

    private int lastCStars = 0, lastVStars = 0;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (lastCStars != gameManager.playerData.stars)
        {
            lastCStars = gameManager.playerData.stars;
            cStarText.text = lastCStars.ToString("N0");
        }
        if (lastVStars != gameManager.playerData.voidStars)
        {
            lastVStars = gameManager.playerData.voidStars;
            vStarText.text = lastVStars.ToString("N0");
        }
    }
}
