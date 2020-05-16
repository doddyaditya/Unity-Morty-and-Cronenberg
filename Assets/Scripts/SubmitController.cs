using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubmitController : MonoBehaviour
{
    public string username;
    public int score;
    public GameObject inputFieldUsername;
    public GameObject inputForm;
    API scoreManager;

    public void Enable()
    {
        inputForm.SetActive(true);
    }
    
    public void Disable()
    {
        inputForm.SetActive(false);
    }

    public void Cancel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void uploadScore()
    {
        username = inputFieldUsername.GetComponent<Text>().text;
        score = ScoreScript.scoreValue;
        scoreManager = GetComponent<API>();
        StartCoroutine(postScore(username, score));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator postScore(string username, int score)
    {
        scoreManager.uploadData(username, score);
        yield return new WaitForSeconds(5);
    }
}
