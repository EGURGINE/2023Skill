using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[Serializable]
public class Dialogue
{
    public List<string> sentences;
}

public class Title : MonoBehaviour
{
    Queue<string> sentences = new Queue<string>();
    public Dialogue info;
    private Coroutine sentenceCo;
    [SerializeField] private GameObject introObjs;
    [SerializeField] private Text introText;
    [SerializeField] private GameObject TitleObjs;
    [SerializeField] private GameObject RankingWnd;
    [SerializeField] private GameObject HowToPlayWnd;

    private void Start()
    {
        StartCoroutine(IntroMove());
        Begin(info);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void RankingWndON()
    {
        RankingWnd.SetActive(true);
        HowToPlayWnd.SetActive(false);
    }
    public void HowToPlayWndOn()
    {
        RankingWnd.SetActive(false);
        HowToPlayWnd.SetActive(true);
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
    IEnumerator IntroMove()
    {
        Vector3 startPos = introText.transform.position;
        Vector3 endPos = introText.transform.position + Vector3.up * 75;

        float t = 0;
        float maxT = 10;
        while (t < maxT) 
        {
            yield return null;
            t += Time.deltaTime;

            introText.transform.position = Vector3.Lerp(startPos, endPos, t / maxT);
        }
    }
    public void Begin(Dialogue info)
    {
        sentences.Clear();

        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Next();
    }
    public void Next()
    {
        if (sentences.Count == 0)
        {
            End();
            return;
        }
        introText.text = string.Empty;
        
        if(sentenceCo != null) StopCoroutine(sentenceCo);
        sentenceCo = StartCoroutine(TypeSentences(sentences.Dequeue()));
    }
    IEnumerator TypeSentences(string sentence)
    {
        foreach (var letter in sentence)
        {
            introText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        End();
    }
    public void End()
    {
        if (sentences != null)
        {
            print("end");
            StopAllCoroutines();
            introObjs.gameObject.SetActive(false);
            TitleObjs.SetActive(true);
        }
    }
}
