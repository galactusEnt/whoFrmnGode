using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class dialogueMaaster : MonoBehaviour
{
    //public variables
    [HideInInspector] public int typeStatus = 0;

    //events
    public UnityEvent SequenceFinished = new UnityEvent();

    //ui
    [SerializeField] private GameObject textBox, characterPrefab, imagePrefab, background, skip;

    //private variables
    private Dictionary<string, GameObject> characterList = new Dictionary<string, GameObject>();
    private int nImgLeft = 0, nImgRight = 0;
    private int diaLineId = 0;
    private StorySegment segment = new();
    private bool isPaused = false;
    private Coroutine type;

    //progress dialog when spacebar / click
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDiaProgress();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool b = false;
            if (!transform.Find("inGameMenu").gameObject.activeSelf)
            {
                b = true;
            }
            transform.Find("inGameMenu").gameObject.SetActive(b);
            PauseDialogue(b);
        }
    }

    public void DisplayLevelStory(string chapter, int segment)
    {
        this.segment = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>().gameData.dialogueStoryMode[chapter][segment];
        DisplayDiaLine();
    }

    public void DisplayCharacter(string character, bool isOnRight)
    {
        if (character == "") return;

        //creation
        GameObject charObj;
        if (!characterList.ContainsKey(character))
        {
            charObj = Instantiate(characterPrefab, transform.Find("charContainer"));
            characterList.Add(character, charObj);
        } 
        else
        {
            charObj = characterList[character];
        }
        
        charObj.name = character;
        charObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Characters/" + character);

        //position
        if (isOnRight)
        {
            charObj.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            charObj.GetComponent<UIScale>().relativePosition = new Vector2(.95f - nImgRight * .05f, .25f);
            nImgRight++;
        }
        else
        {
            charObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            charObj.GetComponent<UIScale>().relativePosition = new Vector2(.05f + nImgLeft * .05f, .25f);
            nImgLeft++;
        }

    }

    public void MakeSpeakerImportant(string speakerName)
    {
        foreach (var kvp in characterList)
        {
            //scale back to normal
            if (kvp.Value.GetComponent<RectTransform>().localScale.z < 1)
            {
                kvp.Value.GetComponent<RectTransform>().localScale *= 1.25f;
            }

            //make visible
            if (kvp.Key == speakerName)
            {

                kvp.Value.GetComponent<Image>().color = new Color(1, 1, 1);
                continue;
            };

            //make unimportant the back characters
            kvp.Value.GetComponent<Image>().color = new Color(.59f, .59f, .59f);
            kvp.Value.GetComponent<RectTransform>().localScale *= .8f;
        }
    }

    public void DisplayPicture(string path)
    {
        var newPic = Instantiate(imagePrefab, transform.Find("picContainer"));
        newPic.name = "image";
        newPic.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        newPic.GetComponent<Image>().SetNativeSize();
        newPic.GetComponent<UIScale>().Init();
        newPic.GetComponent<UIScale>().CalculateAspectRatio();
        newPic.GetComponent<UIScale>().UpdateScaleAndPosition();
        newPic.SetActive(true);
    }

    public void DisplayDiaLine()
    {
        if (segment.sequence.Count <= diaLineId)
        {
            SkipDialogue();
            return;
        }

        var item = segment.sequence[diaLineId];

        //change background
        if (item.pathToBackground == "")
        {
            background.GetComponent<Image>().color = new Color(.9f, .9f, .9f, 0);
        } else
        {
            background.GetComponent<Image>().color = new Color(.9f, .9f, .9f, 1);
            background.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.pathToBackground);
        }

        var name = textBox.transform.Find("name");
        if (item.characters.Count > 0)
        {
            name.gameObject.SetActive(true);
            name.Find("text").GetComponent<TextMeshProUGUI>().text = item.characters[0];
        } else
        {
            name.gameObject.SetActive(false);
        }

        //reset characters list
        nImgLeft = nImgRight = 0;
        if (item.destroyPreviousCharacters)
        {
            foreach (var kvp in characterList)
            {
                Destroy(kvp.Value);
            }
        }

        //display new characters lsit
        for (int i = 0; i < item.characters.Count; i++)
        {
            if (item.isOnRight.Count - 1 < i)
            {
                DisplayCharacter(item.characters[i], false);
            }
            else
            {
                DisplayCharacter(item.characters[i], item.isOnRight[i]);
            }
        }
        if (item.characters.Count > 0)
        {
            MakeSpeakerImportant(item.characters[0]);
        }

        if (item.destroyPreviousPicture && transform.Find("picContainer").Find("image") != null)
        {
            Destroy(transform.Find("picContainer").Find("image").gameObject);
        }

        if (item.pathToPicture != "")
        {
            if (transform.Find("picContainer").Find("image") != null)
            {
                Destroy(transform.Find("picContainer").Find("image").gameObject);
            }
            DisplayPicture(item.pathToPicture);
        }

        //display navigation arrows
        if (segment.canBeNavigated)
        {
            if (diaLineId < segment.sequence.Count - 1 && segment.sequence.Count > 1)
            {
                textBox.transform.Find("goFront").gameObject.SetActive(true);
            } else
            {
                textBox.transform.Find("goFront").gameObject.SetActive(false);
            }

            if (diaLineId > 0 && segment.sequence.Count > 1)
            {
                textBox.transform.Find("goBack").gameObject.SetActive(true);
            }
            else
            {
                textBox.transform.Find("goBack").gameObject.SetActive(false);
            }
        }

        //Typewriter effect
        var text = textBox.transform.Find("text");
        string tempText = item.text;
        text.GetComponent<TextMeshProUGUI>().text = "";
        IEnumerator typeEffect()
        {
            foreach (char letter in tempText.ToCharArray())
            {
                if (typeStatus == 1) break;
                yield return new WaitWhile(() => isPaused);
                text.GetComponent<TextMeshProUGUI>().text += letter;
                yield return new WaitForSeconds(.02f);
            }
            typeStatus = 1;
            text.GetComponent<TextMeshProUGUI>().text = tempText;
        }
        type = StartCoroutine(typeEffect());
    }

    bool debounce = false;
    public void OnDiaProgress()
    {
        if (debounce) return;
        debounce = true;
        Util.ExecuteAfterDelay(this, () => { debounce = false; }, .5f);

        if (typeStatus == 0)
        {
            typeStatus = 1;
            return;
        }
        //progress to next text
        ProgressDia(1);
    }

    public void ProgressDia(int dir) // 1 or -1
    {
        StopCoroutine(type);
        diaLineId = Mathf.Max(0, diaLineId + dir);
        typeStatus = 0;
        DisplayDiaLine();
    }

    public void PauseDialogue(bool state)
    {
        isPaused = state;
    }

    public void TerminateDialogue()
    {
        typeStatus = 1;
        GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>().ShowPanel();
        Destroy(gameObject);
    }

    public void RestartDialogue()
    {
        diaLineId = 0;
        typeStatus = 0;
        DisplayDiaLine();
    }

    public void SkipDialogue()
    {
        SequenceFinished.Invoke();
        TerminateDialogue();
    }
}
