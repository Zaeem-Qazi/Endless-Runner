using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float CurrentScore = 0;
    public bool isplaying = false;

    public UnityEvent onPlay =  new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();

    public SavingSystem data;
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()

    {
       
        data = new SavingSystem();
        data.HighScore = PlayerPrefs.GetFloat("Highscore", 0);
    }

    private void Update()
    {
        if (isplaying)
        {
            CurrentScore += Time.deltaTime;
        }

    }
    public void GameStart() {
        CurrentScore = 0;
        onPlay.Invoke();
        isplaying=true;
    }
    public string PrettyScore()
    {
        return Mathf.RoundToInt( CurrentScore).ToString();
    }
    public string PrettyHighscore()
    {
        return Mathf.RoundToInt( data.HighScore).ToString();
    }

        public void GameOver()
    {

      if(data.HighScore <= CurrentScore)
        {
            data.HighScore= CurrentScore;
            PlayerPrefs.SetFloat("Highscore", data.HighScore);
            PlayerPrefs.Save();
        }
        onGameOver.Invoke();
        isplaying = false;
    }



}

