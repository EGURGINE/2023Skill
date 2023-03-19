using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public class RankI
{
    public string name;
    public int score;
}
public class RankingBoard : MonoBehaviour
{
    [SerializeField] private List<List<RankI>> rankInformation = new List<List<RankI>>();
    [SerializeField] private Text[] rankTxt;
    private void Awake()
    {
        Load();
    }

    public void SetTxts(int stage)
    {
        for (int i = 0; i < 5; i++)
        {
            print($" {rankInformation[stage][i].name} : {rankInformation[stage][i].score}");
            rankTxt[i].text = $"{5 - i}. {rankInformation[stage][i].name} : {rankInformation[stage][i].score}";
        }
    }

    private void Load()
    {
        rankInformation.Add(new List<RankI>());
        rankInformation.Add(new List<RankI>());
        rankInformation.Add(new List<RankI>());
        PlayerPrefs.DeleteAll();
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 5; i++)
            {

                if(PlayerPrefs.HasKey($"{j}StageRanking{i}") == false)
                {
                    PlayerPrefs.SetString($"{j}StageRankingName{i}", $"NONE{i}");
                    PlayerPrefs.SetInt($"{j}StageRanking{i}", 0);
                }
                RankI rank = new RankI();
                rank.score = PlayerPrefs.GetInt($"{j}StageRanking{i}");
                rank.name = PlayerPrefs.GetString($"{j}StageRankingName{i}");
                rankInformation[j].Add(rank);
            }
        }
    }

    public void Save(int stage, string name, int score)
    {
        RankI rank = new RankI();
        rank.score = score;
        rank.name = name;

        rankInformation[stage].Add(rank);
        var b = rankInformation[stage].OrderByDescending(x => x.score);

        rankInformation[stage].RemoveAt(0);

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetString($"{stage}StageRankingName{i}", rankInformation[stage][i].name);
            PlayerPrefs.SetInt($"{stage}StageRanking{i}", rankInformation[stage][i].score);
        }
    }
}
