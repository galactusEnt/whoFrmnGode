using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private RectTransform starRect;
    [SerializeField] private UIScale barScale;

    private void Start()
    {
        Util.ExecuteAfterDelay(this, () =>
        {
            StartCoroutine(Load());
        }, 1.5f);
    }

    private void Update()
    {
        starRect.Rotate(0, 0, -180 * Time.deltaTime);
    }

    IEnumerator Load()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            barScale.relativeSize = new Vector2(progress, .01f);
            yield return null;
        }
    }
}
