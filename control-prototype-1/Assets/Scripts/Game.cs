using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates {
    Start,
    Active,
    Paused,
    Finish
}

public class Game : MonoBehaviour {
    [Header("Objects")]
    public Player Player;
    public Ink Ink;
    [Header("Variables")]
    public GameStates GameState;
    [Header("Splash Screens")]
    public GameObject OverSplash;

    public Stage[] Stages = new Stage[1];
    public GameController UI;

    private Stage currentStage;
    private float finish_Timer;
    private Vector2 finish_Velocity;
    private Vector2 last_Velocity;
    private int collectiblesCollected;

    #region Setters and Getters
    public Stage Stage {
        get {return currentStage;}
    }

    public int CollectiblesCollected {
        set {collectiblesCollected = value;} get {return collectiblesCollected;}
    }
    #endregion

    private void InitializeGame(int stage) {
        // loads the selected stage passed through; loading 0 will load testing stage
        currentStage = Stages[stage];
        foreach(Stage s in Stages)
            s.gameObject.SetActive(false);
        currentStage.gameObject.SetActive(true);

        foreach (Collectible collectible in currentStage.Collectibles)
            collectible.Reset();
        collectiblesCollected = 0;

        ResetPlayer();

        GameState = SwitchGameState(GameStates.Start);
    }

    private void Start() {
        InitializeGame(Level.Selected);

        Ink.Amount = currentStage.Ink;
        Ink.Refill();
    }

    public void Goal() {
        // if the player has all of the collectibles they are able to finish
        if (collectiblesCollected == currentStage.Collectibles.Length) {
            finish_Timer = 0f;
            finish_Velocity = Player.RB.velocity * 0.01f;

            GameState = SwitchGameState(GameStates.Finish);
        }
        // if the player doesn't have all of the collectibles; reset player and collectibles; use UI function to correct buttons
        else UI.Stop();
    }

    private void ResetPlayer() {
        // teleport player back to stage spawn location; reset velocity and rotation
        Player.RB.velocity = new Vector2(0f, 0f);
        Player.transform.rotation = new Quaternion(0f, 0f, 0f, Player.transform.rotation.w);

        Player.transform.position = currentStage.SpawnLocation;
    }

    public void ResetGame() {
        ResetPlayer();
        GameState = SwitchGameState(GameStates.Start);
        // collectibles reset
        collectiblesCollected = 0;
        foreach (Collectible collectible in currentStage.Collectibles)
            collectible.Reset();
    }

    public GameStates SwitchGameState(GameStates state) {
        // set particular variables; return select state
        OverSplash.SetActive(false);

        switch (state) {
            case GameStates.Start:
                Player.RB.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
            case GameStates.Active:
                Player.RB.constraints = RigidbodyConstraints2D.None;
                if (GameState == GameStates.Paused) // resume velocity
                    Player.RB.velocity = last_Velocity;
                break;
            case GameStates.Paused:
                last_Velocity = Player.RB.velocity;
                Player.RB.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
            case GameStates.Finish:
                OverSplash.SetActive(true);
                Player.RB.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
        }

        return state;
    }

    private void Update() {
        // pressing escape at any time returns player to menu screen
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menus");
        if (GameState == GameStates.Finish) {
            if (Input.anyKey)
                SceneManager.LoadScene("Menus");
        }
    }

    private void FixedUpdate() {
        // locked to 50fps; timer stuff here
        switch (GameState) {
            case GameStates.Finish:
                // finish slow-mo
                if (finish_Timer < .6f) {
                    Vector2 vel = new Vector2(finish_Velocity.x * (1 - (finish_Timer / .6f)), finish_Velocity.y * (1 - (finish_Timer / .6f)));
                    Player.X = Player.X + vel.x;
                    Player.Y = Player.Y + vel.y;
                    finish_Timer += 0.02f;
                }
                break;
        }
    }
}
