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

    public void UploadRanking(string txt)
    {
        my.score = GameManager.Instance.Score;
        my.name = txt;

        RankingSet();
    }

    private void RankingSet()
    {
        Ranker myIn = my;
        rankers.Add(myIn);
        List<Ranker> newList = rankers.OrderByDescending(item => item.score).ToList();

        newList.RemoveAt(newList.Count-1);
        rankers = newList;
    }



}
