using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb2D;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public Rigidbody2D RB {
        get {return rb2D;} set {rb2D = value;}
    }

    public float X {
        get {return transform.position.x;} set {transform.position = new Vector3(value, transform.position.y, transform.position.z);}
    }

    public float Y {
        get {return transform.position.y;} set {transform.position = new Vector3(transform.position.x, value, transform.position.z);}
    }
}
