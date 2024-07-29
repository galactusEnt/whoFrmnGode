using DG.Tweening;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    //properties
    public float maxTime = 5, currentTime = 5;
    public bool isFrozen = false;

    //ui elements
    [SerializeField] private GameObject progressBar = null, hourglass = null, text = null;

    //event
    public UnityEvent onTimeEnd = new UnityEvent();

    //private variables
    private int hourglassDir = 1;
    Coroutine routine;

    private IEnumerator Timer()
    {
        while (currentTime > 0)
        {
            yield return null;
            if (isFrozen) continue;
            currentTime -= Time.deltaTime;
        }
        onTimeEnd.Invoke();
    }

    public void startTimer()
    {
        if (!gameObject.activeSelf) return;
        isFrozen = false;
        routine = StartCoroutine(Timer());
    }

    public void pauseTimer(bool pauseState)
    {
        isFrozen = pauseState;
        GetComponent<AudioSource>().Stop();
    }

    public void restartTimer()
    {
        if (!gameObject.activeSelf) return;
        if (routine != null) StopCoroutine(routine);

        currentTime = maxTime + Time.deltaTime;
        startTimer();
    }

    public void stopTimer()
    {
        if (!gameObject.activeSelf) return;
        if (routine != null) StopCoroutine(routine);
        GetComponent<AudioSource>().Stop();
        isFrozen = true;
    }

    public void updateUI()
    {
        if (progressBar != null)
        {
            progressBar.GetComponent<UIScale>().relativeSize = new Vector2(Mathf.Clamp01(currentTime / maxTime), 1);
        }

        if (hourglass != null)
        {
            float z = hourglass.GetComponent<RectTransform>().rotation.z;
            if (z > 0.26) hourglassDir = -1;
            else if (z < 0) hourglassDir = 1;
            hourglass.GetComponent<RectTransform>().Rotate(0, 0, 30 * Time.deltaTime * hourglassDir * maxTime / currentTime);
        }

        if (text != null)
        {

        }

        //audio
        var audio = GetComponent<AudioSource>();
        //audio.pitch = Mathf.Lerp(1, 10, Mathf.Pow(2, 10 * (1 - currentTime / maxTime) - 10));
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    public void tweenBar(bool add)
    {
        var progress = transform.Find("progress");
        var color = new Color(239 / 255f, 66 / 255f, 63 / 255f);
        if (add)
        {
            color = new Color(63 / 255f, 239 / 255f, 66 / 255f);
        }
        DOTween.To(() => progress.GetComponent<Image>().color, x => progress.GetComponent<Image>().color = x, color, .5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
        DOTween.To(() => progress.GetComponent<UIScale>().relativeSize, x => progress.GetComponent<UIScale>().relativeSize = x, new Vector2(currentTime / maxTime, 1f), .5f).SetEase(Ease.OutQuad);
    }

    //Unity functions
    void Update()
    {
        if (currentTime > 0 && !isFrozen)
        {
            updateUI();
        }
    }
}
