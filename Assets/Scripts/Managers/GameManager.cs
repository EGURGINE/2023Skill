using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    private bool isGameStart;
    public int stageNum;
    [SerializeField] private Text stageTxt;
    [SerializeField] private string[] stageName;
    [SerializeField] private Image fuelImage;
    [SerializeField] private Text fuelValueTxt;
    private float fuel = 100;
    private float maxFuel = 100;
    public float Fuel
    {
        get
        {
            return fuel;
        }
        set
        {
            fuel = value;

            if (fuel <= 0) Die();

            fuelImage.fillAmount = fuel / maxFuel;
            fuelValueTxt.text = ((int)((fuel / maxFuel) * 100)).ToString() + "%";

        }
    }
    [SerializeField] private Image[] hpImages;
    private float hp = 6;
    private float maxHP = 6;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp >= maxHP) hp = maxHP;
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
            foreach (var item in hpImages)
            {
                item.gameObject.SetActive(false);
            }

            for (int i = 0; i < hp; i++)
            {
                hpImages[i].gameObject.SetActive(true);
            }

        }
    }

    [SerializeField] private Text[] scoreTexts;
    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;

            scoreTexts[0].text = score.ToString();
            scoreTexts[1].text = score.ToString();
        }
    }

    [SerializeField] private Player player;
    [SerializeField] private RankingBoard ranking;
    [SerializeField] private Image fade;

    private void Start()
    {
        StartSET();
    }
    void Update()
    {
        if (isGameStart == false) return;
        Fuel -= Time.deltaTime;
    }

    public void isClear()
    {
        isGameStart = false;
        player.isGameStart = isGameStart;
        StartCoroutine(ClearWndOn());
    }
    private IEnumerator ClearWndOn()
    {
        yield return new WaitForSeconds(1.5f);
        ranking.gameObject.SetActive(true);
        ranking.OnRankingWnd();
    }

    private void StartSET()
    {
        stageNum = DataManager.instance.Stage;
        hp = maxHP;
        fuel = maxFuel;
        isGameStart = true;
        if (stageNum <= 0) Score = 0;
        else Score = DataManager.instance.my.score;
        StartCoroutine(StageTxt());
        player.isGameStart = isGameStart;
        StartCoroutine(Timer());
    }
    public void NextStage()
    {
        StartCoroutine(NextStageMove());
    }
    public float timer;
    private IEnumerator Timer()
    {
        timer = 0;

        while (isGameStart == true)
        {
            yield return null;

            timer+=Time.deltaTime;
        }
    }
    public IEnumerator NextStageMove()
    {
        DataManager.instance.my.score = score;
        stageNum++;
        DataManager.instance.Stage = stageNum;


        ranking.gameObject.SetActive(false);
        fade.gameObject.SetActive(true);

        float t = 0;

        Color fadeA = Color.black;

        while (t <= 1.5f)
        {
            yield return null;
            t += Time.deltaTime;

            fadeA.a = Mathf.Lerp(0, 1, t / 1.5f);
            fade.color = fadeA;
            player.transform.position = Vector3.Lerp(player.transform.position, Vector3.up * 10f, t / 1.5f);
        }

        SceneManager.LoadScene(stageNum + 1);
    }

    private IEnumerator StageTxt()
    {
        stageTxt.gameObject.SetActive(true);
        stageTxt.text = stageName[stageNum - 1];
        Color textA = Color.white;
        float t = 0;
        while (t <= 1.5f)
        {
            yield return null;
            t += Time.deltaTime;
            textA.a = Mathf.Lerp(0, 1, t / 1.5f);
            stageTxt.color = textA;
        }
        t = 0;
        while (t <= 1.5f)
        {
            yield return null;
            t += Time.deltaTime;
            textA.a = Mathf.Lerp(1, 0, t / 1.5f);
            stageTxt.color = textA;
        }
        stageTxt.gameObject.SetActive(false);
    }

    private void Die()
    {
        isGameStart = false;
    }
}
