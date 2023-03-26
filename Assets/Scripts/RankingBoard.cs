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
    [SerializeField] private InputField inputField;
    [SerializeField] private List<Ranker> rankers = new List<Ranker>();
    [SerializeField] private Ranker my = new Ranker();
    [SerializeField] private GameObject inputNameWnd;
    [SerializeField] private GameObject rankingWnd;
    [SerializeField] private Text[] rankerNames;
    [SerializeField] private Text[] rankerScores;
    [SerializeField] private Text myName;
    [SerializeField] private Text myScore;


    public void UploadRanking(string txt)
    {
        if (txt.Length < 3) return; 
        my.score = GameManager.Instance.Score;
        my.name = txt;

        RankingSet();
    }

    private void RankingSet()
    {
        Ranker myIn = new Ranker();
        myIn.score = my.score;
        myIn.name = my.name;
        rankers.Add(myIn);
        rankers = rankers.OrderByDescending(item => item.score).ToList();

        rankers.RemoveAt(rankers.Count - 1);
        RankingBoardSet();
    }

    private void RankingBoardSet()
    {
        inputNameWnd.SetActive(false);
        rankingWnd.SetActive(true);


        for (int i = 0; i < rankers.Count; i++)
        {
            rankerNames[i].text = $"{i + 1}. {rankers[i].name}";
            rankerScores[i].text = $"{rankers[i].score}";
        }

        myName.text = my.name;
        myScore.text = my.score.ToString();
    }

}
