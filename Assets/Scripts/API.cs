using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    private string webURL;
    [SerializeField] private string username;
    [SerializeField] private int score;
    public JSONData jsonData;
    ScoreTable highscoreDisplay;

    void Awake()
    {
        highscoreDisplay = GetComponent<ScoreTable>();
    }

    public void downloadData()
    {
        StartCoroutine(getData());
    }

    public void uploadData(string username, int score)
    {
        StartCoroutine(postData(username, score));
    }

    IEnumerator getData()
    {
        webURL = "http://134.209.97.218:5051/scoreboards/13517008";
        WWW _www = new WWW(webURL);
        yield return _www;
        if (string.IsNullOrEmpty(_www.error))
        {
            processJsonData("{\"dataInfo\":" + _www.text + "}");
        } else
        {
            Debug.Log("Something went wrong " + _www.error);
        }
    }

    IEnumerator postData(string username, int score)
    {
        webURL = "http://134.209.97.218:5051/scoreboards/13517008";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", score);
        WWW _www = new WWW(webURL, form);
        yield return _www;
        if (string.IsNullOrEmpty(_www.error))
        {
            Debug.Log("Post successful");
        }
        else
        {
            Debug.Log("Something went wrong " + _www.error);
        }
    }

    private void processJsonData(string _url)
    {
        jsonData = JsonUtility.FromJson<JSONData>(_url);
        highscoreDisplay.updateHighscore(jsonData);
    }
}