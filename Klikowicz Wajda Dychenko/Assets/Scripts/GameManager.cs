using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public enum GameState { GS_PASEMENU, GS_GAME, GS_LEVELCOMPLETED, GS_GAME_OVER}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.GS_GAME;

    public static GameManager instance ;

    public Canvas inGameCanvas;

    public TMP_Text scoreText;

    public TMP_Text timerText;

    public TMP_Text preyCounterText;

    private int score = 0;

    public Image[] keysTab = new Image [3];

    public Image[] liveTab = new Image[3];

    private float timer = 0;

    private int keysFound = 0;

    private int keysNumber = 3;


    public int preyCounter = 0;

    public int maxLives = 3;

    public int lives = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause())
            UpdateTimer();

        ColorLives();
        UpdatePreyCounter();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GS_PASEMENU) InGame();
            else if (currentGameState == GameState.GS_GAME) PauseMenu();
        }
        
    }

    void Awake()
    {
        instance = this;
        //AddPoints(0);
        keysTab[0].color = Color.gray;
        keysTab[1].color = Color.gray;
        keysTab[2].color = Color.gray;
    }

    public void AddKeys()
    {
        keysTab[keysFound].color = Color.white;
        keysFound++;
    }

    public void AddLive()
    {
        lives++;
    }

    public void RemoveLive()
    {
        lives--;
    }

    private void UpdatePreyCounter()
    {
        preyCounterText.text = preyCounter.ToString();
    }

    private void ColorLives()
    {
        for(int i = 0; i < maxLives; i++)
        {
            liveTab[i].color = lives >= i + 1 ? Color.white : Color.gray;
        }
    }

    public bool CollectedAllKeys()
    {
        return keysFound == keysNumber;
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        int seconds = (int)(timer);
        timerText.text = string.Format("{0:00}:{1:00}", seconds / 60, seconds % 60);
    }

    public void AddPoints(int points)
    {
        score=score+points;
        scoreText.text = score.ToString();

    } 

    public bool isPause()
    {
        return currentGameState == GameState.GS_PASEMENU;
    }

    void setGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        if (newGameState == GameState.GS_GAME) inGameCanvas.enabled=true;
        if (newGameState == GameState.GS_PASEMENU) inGameCanvas.enabled = false;
    }
    public void PauseMenu()
    {
        setGameState(GameState.GS_PASEMENU);
    }
    public void InGame()
    {
        setGameState(GameState.GS_GAME);
    }
    public void LevelCompleted()
    {
        setGameState(GameState.GS_LEVELCOMPLETED);
    }
    public void GameOver()
    {
        setGameState(GameState.GS_GAME_OVER);
    }



}
