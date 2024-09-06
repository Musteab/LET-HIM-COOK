using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {  get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGammeUnPaused;
    public event EventHandler OnStatechanged;
    private enum State
    {
        WaitingTostart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 100f;
    private bool isGamePaused = false;



    private void Awake()
    {
        Instance = this;
        state = State.WaitingTostart;
    }

    private void Start()
    {
        inputscript.Instance.OnPauseAction += Input_OnPauseAction;
        inputscript.Instance.OnInteract += Input_OnInteract;
    }

    private void Input_OnInteract(object sender, EventArgs e)
    {
        if (state == State.WaitingTostart)
        {
            state = State.CountdownToStart;
            OnStatechanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Input_OnPauseAction(object sender, EventArgs e)
    {
        TooglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingTostart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStatechanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStatechanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }
    
    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;

    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float NormalizedGameTimer()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
    public void TooglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        { 
        Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
    
        }
        else
        {
            Time.timeScale = 1f;
            OnGammeUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
