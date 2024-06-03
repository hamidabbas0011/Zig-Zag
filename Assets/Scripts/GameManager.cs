using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int highScore;
    [SerializeField] private Text scoreUI;
    [SerializeField] private Text highScoreUI;
    public bool gameStarted;
    [SerializeField] private GameObject canvas;    // Start is called before the first frame update
    [SerializeField] private GameObject img;    // Start is called before the first frame update
    [SerializeField] private GameObject scoreCanvas;    // Start is called before the first frame update

    private void Start()
    {
        scoreCanvas.SetActive(false);
    }

    private void Awake()
    {
        highScoreUI.text = "Highscore: " + GetHighScore().ToString();
    }
    
    // Update is called once per frame
    void Update()
    {

        Debug.Log(highScore);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            scoreCanvas.SetActive(true);
            img.SetActive(false);
            canvas.SetActive(false);
            StartGame();
        }   
    }
    
    void StartGame()
    {
        gameStarted = true;
        FindObjectOfType<Road>().StartBuilding();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        score++;
        scoreUI.text = "Score: " + score;

        if (score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScoreUI.text = "Highscore: " + score.ToString();
        }
    }

    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }

    
    
    
}
