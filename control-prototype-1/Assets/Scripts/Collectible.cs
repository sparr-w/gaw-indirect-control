using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public Game Game;

    private void OnTriggerEnter2D(Collider2D obj) {
        if (obj.gameObject.layer == 7) { // player's layer is 7
            // add count to game; set collectible to inactive
            Game.CollectiblesCollected += 1;
            gameObject.SetActive(false);
        }
    }

    public void Reset() {
        gameObject.SetActive(true);
    }
}
