using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllRankingWnd : MonoBehaviour
{
    [SerializeField] private Text stageNum;
    [SerializeField] private Text[] rankerNames;
    [SerializeField] private Text[] rankerScores;

    private int stage = 1;

    private void Awake()
    {
        for (int i = 1; i < 4; i++)
        {
            if (PlayerPrefs.HasKey(PlayerPrefs.GetString($"{i}StageRankerName{0}")) == true)
            {
                CreatePlayerPrefs(i);
            }
        }
    }

    private void CreatePlayerPrefs(int stage)
    {
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetString($"{stage}StageRankerName{i}","none");
            PlayerPrefs.SetFloat($"{stage}StageRankerScore{i}",0);
        }
    }

    private void OnEnable()
    {
        stage = 1;
        RankingSet();
    }

    private void RankingSet()
    {
        stageNum.text = $"{stage}Stage";

        for (int i = 0; i < 3; i++)
        {
            rankerNames[i].text = $"{i + 1}. {PlayerPrefs.GetString($"{stage}StageRankerName{i}")}";
            rankerScores[i].text = $"{PlayerPrefs.GetFloat($"{stage}StageRankerScore{i}")}";
        }
    }

    public void RankingBoardSet()
    {
        stage++;
        if (stage > 3) stage = 1;

        RankingSet();
    }
}
