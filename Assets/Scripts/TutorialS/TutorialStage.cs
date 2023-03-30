using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialStage : Singleton<TutorialStage>
{
    [SerializeField] private Text aiTxt;
    Queue<string> sentences = new Queue<string>();
    public Dialogue info;
    private Coroutine sentenceCo;

    [SerializeField] private GameObject moveCheckObj;
    [SerializeField] private GameObject meteorObj;
    [SerializeField] private GameObject meteorObjs;
    [SerializeField] private GameObject meteorsObjs;

    public int tutorialNum = 0;
    public bool isEscape;

    private void Start()
    {
        NextTutorial();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tutorialNum == 3 || tutorialNum == 5 || tutorialNum == 7 || tutorialNum == 8) return;
            tutorialNum++;
            NextTutorial();
        }

    }
    public void NextTutorial()
    {
        switch (tutorialNum)
        {
            case 3:
                moveCheckObj.SetActive(true);
                break;
            case 5:
                meteorObj.SetActive(true);
                break;
            case 7:
                meteorObjs.SetActive(true);
                break;
            case 9:
                meteorsObjs.SetActive(true);
                StartCoroutine(EscapeCheck());
                break;
            default:
                Begin();
                break;
        }
    }
    [SerializeField] private GameObject escapeWnd;
    [SerializeField] private Image escapeGage;

    private IEnumerator EscapeCheck()
    {
        escapeWnd.SetActive(true);
        float t = 0;
        while (t < 2)
        {
            yield return null;
            if(Input.GetKey(KeyCode.V)) t += Time.deltaTime;
            else t -= (Time.deltaTime * 0.8f);
            escapeGage.fillAmount = t / 2;
        }
        isEscape = true;
        escapeWnd.SetActive(false);
    }

    public void Begin()
    {
        sentences.Clear();
        sentences.Enqueue(info.sentences[tutorialNum]);
        Next();
    }
    public void Next()
    {
        if (sentences.Count == 0)
        {
            End();
            return;
        }
        aiTxt.text = string.Empty;

        if (sentenceCo != null) StopCoroutine(sentenceCo);
        sentenceCo = StartCoroutine(TypeSentences(sentences.Dequeue()));
    }
    IEnumerator TypeSentences(string sentence)
    {
        foreach (var letter in sentence)
        {
            aiTxt.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        End();
    }
    public void End()
    {
        if (sentences != null)
        {
            print("end");
            StopCoroutine(sentenceCo);
            tutorialNum++;
            NextTutorial();
        }
    }


}
