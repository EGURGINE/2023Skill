using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public class Ranker
{
    public int score;
    public string name;
}

public class RankingBoard : MonoBehaviour
{
    [SerializeField] private List<Ranker> rankers = new List<Ranker>();
    [SerializeField] private Text[] rankerNames;
    [SerializeField] private Text[] rankerScores;
    [SerializeField] private Text myName;
    [SerializeField] private Text myScore;
    [SerializeField] private Text inputMyScore;
    [SerializeField] private Text stageNClear;
    [SerializeField] private Text timer;
    public void OnRankingWnd()
    {
        stageNClear.text = $"Stage {GameManager.Instance.stageNum} Clear";
        inputMyScore.text = $"Score : {GameManager.Instance.Score}";
        print((int)GameManager.Instance.timer);
        timer.text =  "Timer "+((int)GameManager.Instance.timer).ToString("00:00");
        //RankingSet();
    }
    private void RankingSet()
    {
        Ranker myIn = new Ranker();
        myIn.score = DataManager.instance.my.score;
        myIn.name = DataManager.instance.my.name;
        rankers.Add(myIn);
        rankers = rankers.OrderByDescending(item => item.score).ToList();

        rankers.RemoveAt(rankers.Count - 1);
        RankingBoardSet();
    }

    private void RankingBoardSet()
    {
        for (int i = 0; i < rankers.Count; i++)
        {
            rankerNames[i].text = $"{i + 1}. {rankers[i].name}";
            rankerScores[i].text = $"{rankers[i].score}";
        }

        myName.text = DataManager.instance.my.name;
        myScore.text = DataManager.instance.my.score.ToString();
    }

    public void NextStage()
    {
        RankingSave();
        GameManager.Instance.NextStage();
    }

    private void RankingSave()
    {
        DataManager.instance.my.score = GameManager.Instance.Score;
        //for (int i = 0; i < rankers.Count; i++)
        //{
        //    PlayerPrefs.SetString($"RankerName{i}", rankers[i].name);
        //    PlayerPrefs.SetFloat($"RankerScore{i}", rankers[i].score);
        //}
    }
}
