using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private DateTime _sessionStartTime;
    private DateTime _sessionEndTime;

    private void Start()
    {
        _sessionStartTime = DateTime.Now;
        Debug.Log("Game session start at: " + _sessionStartTime);
    }

    private void OnApplicationQuit()
    {
        _sessionEndTime = DateTime.Now;
        TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);

        Debug.Log("Game session ended at: " + DateTime.Now);
        Debug.Log("Game session lasted for: " + timeDifference);
    }

    public void NexRound()
    {
        int total = SceneManager.sceneCountInBuildSettings;
        int current = SceneManager.GetActiveScene().buildIndex;
        int loading;

        if(current == total - 1)
        {
            loading = 0;
        }
        else
        {
            loading = current + 1;
        }
        FindObjectOfType<RoundManager>().EndRound();
        SceneManager.LoadSceneAsync(loading);

    }

    public void RestartRound()
    {
        FindObjectOfType<RoundManager>().EndRound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}