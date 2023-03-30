using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] private GameObject player;

    [SerializeField] private Image fade;

    private void Start()
    {
        NextTutorial();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tutorialNum == 3 || tutorialNum == 5 || tutorialNum == 7 || tutorialNum == 9) return;
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
            case 10:
                StartCoroutine(NextStage());
                break;
            default:
                Begin();
                break;
        }
    }
    [SerializeField] private GameObject escapeWnd;
    [SerializeField] private Image escapeGage;

    private IEnumerator NextStage()
    {
        fade.gameObject.SetActive(true);
        Color a = new Color();
        float t = 0;
        while (t<1)
        {
            yield return null;
            t += Time.deltaTime;

            a.a = Mathf.Lerp(0, 1, t / 1);
            fade.color = a;
        }
        DataManager.instance.Stage++;
        SceneManager.LoadScene("Stage1");
    }
    private IEnumerator EscapeCheck()
    {
        escapeWnd.SetActive(true);
        float t = 0;
        while (t < 2)
        {
            yield return null;
            if(Input.GetKey(KeyCode.V)) t += Time.deltaTime;
            else t -= (Time.deltaTime * 0.8f);
            if (t <= 0) t = 0;
            escapeGage.fillAmount = t / 2;
        }
        isEscape = true;
        escapeWnd.SetActive(false);
        StartCoroutine(PlayerEscape());
    }
    private IEnumerator PlayerEscape()
    {
        player.GetComponent<Player>().isGameStart = false;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = Vector3.up * 15;

        Begin();

        float t = 0;
        while (t < 2) 
        {
            yield return null;
            t+= Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPos, endPos, t / 2);
        }

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
            StopCoroutine(sentenceCo);
            tutorialNum++;
            NextTutorial();
        }
    }


}
