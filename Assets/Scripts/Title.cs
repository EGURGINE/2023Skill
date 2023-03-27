using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject RankingWnd;
    [SerializeField] private GameObject HowToPlayWnd;
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
}
