using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllRankingWnd : MonoBehaviour
{
    [SerializeField] private Text[] rankerNames;
    [SerializeField] private Text[] rankerScores;

    private void CreatePlayerPrefs()
    {
        print("dd");
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetString($"RankerName{i}","OOO");
            PlayerPrefs.SetFloat($"RankerScore{i}",0);
        }
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(PlayerPrefs.GetString($"RankerName{0}")) == false)
        {
            CreatePlayerPrefs();
        }
        RankingSet();
    }

    private void RankingSet()
    {
        for (int i = 0; i < 3; i++)
        {
            rankerNames[i].text = $"{i + 1}. {PlayerPrefs.GetString($"RankerName{i}")}";
            rankerScores[i].text = $"{PlayerPrefs.GetFloat($"StageRankerScore{i}")}";
        }
    }

    public void BackBtn()
    {
        gameObject.SetActive(false);
    }
}
