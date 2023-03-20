using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    private bool isGameStart;

    [SerializeField] private Image[] fuelImages;
    private float fuel   = 100;
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

            fuelImages[0].fillAmount = fuel / maxFuel;
            fuelImages[1].fillAmount = fuel / maxFuel;
        }
    }
    [SerializeField] private Image[] hpImages;
    private float hp = 3;
    private float maxHP = 3;
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
            if (hp <= 0) {
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


    private void Start()
    {
        StartSET();
    }
    void Update()
    {
        if (isGameStart == false) return; 
        Fuel -= Time.deltaTime;
    }

    private void StartSET()
    {
        hp = maxHP;
        fuel = maxFuel;
        isGameStart = true;
        Score = 0;
    }
    private void Die()
    {
        isGameStart = false;
    }
}
