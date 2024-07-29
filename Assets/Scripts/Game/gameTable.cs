using Coffee.UIExtensions;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class gameTable : MonoBehaviour
{
    //init parameters
    public string type = "grid";

    //private variables
    private List<Image> pool = new List<Image>();

    //events
    public UnityEvent onRoundClear = new UnityEvent();

    private void Start()
    {
        picPrefab.DORestart();
    }

    public void generate()
    {
        switch (type)
        {
            case "grid":
                grid_generate();
                break;
            default:
                grid_generate();
                break;
        }
    }

    public void clear()
    {
        if (grid != null)
        {
            foreach(Image image in pool)
            {
                image.gameObject.SetActive(false);
            }
            grid.gameObject.SetActive(false);
        }
    }

    #region grid

    #region grid properties
    [SerializeField] private Image picPrefab;

    [SerializeField] private GridLayoutGroup grid;

    [SerializeField] private ParticleSystem starParticle;

    public int size = 4, targetCount = 1;
    public Sprite[] spriteTable;

    public List<Image> targets = new List<Image>();
    #endregion

    private void grid_generate()
    {
        //clean up last round
        clear();
        //calculate size
        int root = size;
        size = (int)(Mathf.Pow(root, 2));

        RectTransform panelRectTransform = grid.GetComponent<RectTransform>();
        float panelWidth = panelRectTransform.rect.width;
        float panelHeight = panelRectTransform.rect.height;
        float cellWidth = (panelWidth - (grid.spacing.x * (root - 1))) / root;
        float cellHeight = (panelHeight - (grid.spacing.y * (root - 1))) / root;
        grid.cellSize = new Vector2(cellWidth, cellHeight);

        //calculate impostors

        targets.Clear();

        List<int> impostors = new List<int>();
        for (int i = 0; i < targetCount; i++)
        {
            int a = Random.Range(0, size);
            while (impostors.Contains(a))
            {
                a = Random.Range(0, size);
            }
            impostors.Add(a);
        }

        int img1 = Random.Range(0, spriteTable.Length);
        int img2 = Random.Range(0, spriteTable.Length);

        int c = 0;
        while (img2 == img1 || c > 25)
        {
            img2 = Random.Range(0, spriteTable.Length);
            c++;
        }

        //populate grid
        while (pool.Count < size)
        {
            pool.Add(Instantiate(picPrefab, grid.transform));
        }
        grid.gameObject.SetActive(true);

        for (int i = 0; i < size; i++)
        {
            Image imageInstance = pool[i];
            Image img = imageInstance.transform.Find("obj").GetComponent<Image>();

            img.sprite = spriteTable[img1];
            img.color = new Color(1, 1, 1, 0);
            imageInstance.transform.Find("obj").GetComponent<UIScale>().relativeSize = new Vector2(1.4f, 1.4f);
            
            imageInstance.name = (i % size).ToString() + (i / size).ToString();
            if (impostors.Contains(i))
            {
                img.sprite = spriteTable[img2];
                targets.Add(imageInstance);
                impostors.Remove(i);
            }
            imageInstance.GetComponent<Button>().onClick.AddListener(() => grid_onTargetFound(imageInstance));
            imageInstance.gameObject.SetActive(true);
            img.DOColor(new Color(1, 1, 1, 1), .2f).SetEase(Ease.InBack);
            DOTween.To(() => imageInstance.transform.Find("obj").GetComponent<UIScale>().relativeSize, x => imageInstance.transform.Find("obj").GetComponent<UIScale>().relativeSize = x, new Vector2(.9f, .9f), .3f);
        }
    }

    private void grid_onTargetFound(Image image)
    {
        if (targets.Contains(image))
        {
            targets.Remove(image);
            //play sound
            var a = GetComponent<AudioSource>();
            a.PlayOneShot(a.clip);

            // Instantiate the particle system
            ParticleSystem p = Instantiate(starParticle, transform.parent.parent);

            // Set the position of the particle system
            p.transform.position = image.GetComponent<RectTransform>().position;
            p.GetComponent<UIParticle>().scale = 35 * Mathf.Clamp01(Mathf.Sqrt(4f / size));

            Util.EmitParticlesAndDestroy(p, (int)(6 * PlayerPrefs.GetInt("Particles", 3) / 3f));

            if (targets.Count == 0)
            {
                //nrOfImpostors found => progress to next round
                onRoundClear.Invoke();
                return;
            }
            DOTween.To(() => image.transform.Find("obj").GetComponent<UIScale>().relativeSize, x => image.transform.Find("obj").GetComponent<UIScale>().relativeSize = x, new Vector2(0, 0), .2f);
        }
    }

    #endregion
}
