using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Game Game;
    public Ink Ink;
    public GameObject PathContainer;
    [Header("Buttons")]
    public GameObject PlayButton;
    public GameObject PauseButton;

    public void StartPause() {
        switch (Game.GameState) {
            case GameStates.Start: case GameStates.Paused:
                // set game to active and change button to pause
                Game.GameState = Game.SwitchGameState(GameStates.Active);
                
                PlayButton.SetActive(false);
                foreach (Transform child in PlayButton.transform) // hide label also, clicking it disables parent and keeps children active
                    child.gameObject.SetActive(false);
                PauseButton.SetActive(true);
                break;
            case GameStates.Active:
                // set game to paused and change button to play
                Game.GameState = Game.SwitchGameState(GameStates.Paused);

                PlayButton.SetActive(true);
                PauseButton.SetActive(false);
                foreach (Transform child in PauseButton.transform) // hide label also, clicking it disables parent and keeps children active
                    child.gameObject.SetActive(false);
                break;
        }
    }

    public void Reset() {
        // reset game and player; refill ink; delete previous drawing
        Game.ResetGame();
        Ink.Refill();
        
        foreach (Transform child in PathContainer.transform)
            Destroy(child.gameObject);

        // the player is able to reset the game whilst it is running; assure play button is then shown
        PlayButton.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void Stop() {
        // reset player and game state
        Game.ResetGame();

        PlayButton.SetActive(true);
        PauseButton.SetActive(false);
    }
}
