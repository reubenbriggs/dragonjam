using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    MainMenu
}

public class GameManager : MonoBehaviour
{
    public delegate void StateChange(GameState newState);
    public event StateChange OnStateChange;

    private static GameManager instance;
    public static GameManager Instance {
        get {
            return instance;
        }
    }

    [SerializeField]
    private GameState currentState;
    private GameState? previousState;

    private static bool gameStarted = false;

    // Use this for initialization
    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        if (!gameStarted)
            currentState = GameState.MainMenu;
        else
            currentState = GameState.Playing;
        previousState = null;
        gameStarted = true;
        ScoreManager.Instance.ResetScore();
    }

    // Update is called once per frame
    void Update() {
        if (currentState != previousState) {
            if (OnStateChange != null)
                OnStateChange(currentState);
        }
    }

    public void SetGameState(int stateIndex) {
        currentState = (GameState)stateIndex;
    }

    public void GameOver() {
        currentState = GameState.GameOver;
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
