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

    private int score = 0;

    public Image[] keysTab = new Image [3];

    private int keysFound = 0;

    private int keysNumber = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
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

    public bool CollectedAllKeys()
    {
        return keysFound == keysNumber;
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
