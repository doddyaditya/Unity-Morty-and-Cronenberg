using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private Text[] username;
    [SerializeField] private Text[] score;
    API highscoreManager;

    void Start()
    {
        highscoreManager = GetComponent<API>();
        StartCoroutine("refreshHighscore");
    }

    public void updateHighscore(JSONData jsonData)
    {
        for (int i = 0; i < username.Length; i++)
        {
            if (highscoreManager.jsonData.dataInfo.Length > i)
            {
                username[i].text = i + 1 + "." + " " + highscoreManager.jsonData.dataInfo[i].username;
                score[i].text = highscoreManager.jsonData.dataInfo[i].score.ToString();
            }
        }
    }

    IEnumerator refreshHighscore()
    {
        highscoreManager.downloadData();
        yield return new WaitForSeconds(5);
    }
}
