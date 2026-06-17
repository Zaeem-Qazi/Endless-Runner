using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreUI;
    GameManager gm;
    [SerializeField] private GameObject StartMenuUI;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private TextMeshProUGUI gameoverScoreUI;
    [SerializeField] private TextMeshProUGUI gameoverHighscoreUI;
    private void Start()
    {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverUI);
    }

    public void PlayBtnHandler()
    {
        gm.GameStart();
        
    }

    public void ActivateGameOverUI()
    {

        
        GameOverUI.SetActive(true);
        gameoverScoreUI.text = " Score: " + gm.PrettyScore();
        gameoverHighscoreUI.text = " Highscore: "+  gm.PrettyHighscore();
    
    }
    public void OnGUI()
    {
        ScoreUI.text = gm.PrettyScore();
    }
}
