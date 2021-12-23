using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
    public int Ink = 10;
    [Header("Children")]
    public Transform Spawn;
    public Collectible[] Collectibles = new Collectible[1];

    #region Setters and Getters
    public Vector3 SpawnLocation {
        get {return Spawn.position;}
    }
    #endregion
}
