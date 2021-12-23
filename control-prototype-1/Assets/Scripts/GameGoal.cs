using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoal : MonoBehaviour {
    public Game Game;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 7) {
            // player's layer is layer 7
            Game.Goal();
        }
    }
}
