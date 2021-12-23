using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
    public Ink Ink;
    public GameObject PathContainer;
    public GameObject PathPrefab;

    private SpriteRenderer spriteRenderer;
    private Vector2 startPos;
    private Vector2 endPos;
    private float holdTime;

    private void DrawPath(Vector2 start, Vector2 end) {
        float xdiff = end.x - start.x;
        float ydiff = end.y - start.y;
        // angle = atan2(y, x) // y = By - Ay // x = Bx - Ax // returns in radians, convert to degree
        float angle = (180 / Mathf.PI) * Mathf.Atan2(ydiff, xdiff);
        // distance pythagoras a^2 + b^2 = c^2 // a = Ax - Bx // b = Ay - By
        float dist = Mathf.Sqrt((xdiff * xdiff) + (ydiff * ydiff));
        // limit the amount able to draw by remaining amount of ink; take away ink when drawing
        if (dist >= Ink.Remaining) {
            xdiff = xdiff * (Ink.Remaining / dist);
            ydiff = ydiff * (Ink.Remaining / dist);
            dist = Ink.Remaining;
        }
        Ink.Remaining = Ink.Remaining - dist;
        // position needs to be middle point of both start and end
        Vector3 pos = new Vector3(start.x + xdiff/2, start.y + ydiff/2, 0f);
        // draw line; only if the distance is more than 0f to prevent spriterenderer error
        if (dist > 0f) {
            GameObject newLine = Instantiate(PathPrefab, pos, Quaternion.Euler(0f, 0f, angle));
            // move all lines under a container so they can be removed later a lot more easily
            newLine.transform.parent = PathContainer.transform;
            spriteRenderer = newLine.GetComponent<SpriteRenderer>();
            // distance of the line, multiplied by 1/size of prefab to get correct size, add a buffer to connect the lines
            spriteRenderer.size = new Vector2(dist * (1f / PathPrefab.transform.localScale.x) + 1f, 0.2f);
        }
    }

    private void Update() {
        // add time whilst mouse button is held down
        if (Input.GetMouseButton(0)) 
            holdTime += Time.deltaTime;
        // reset time held when button is pressed
        if (Input.GetMouseButtonDown(0)) {
            holdTime = 0f;
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        // draw floor part every fifth of a second; set new start point to end of last line; reset hold time
        if (holdTime >= 0.2f) {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawPath(startPos, endPos);
            startPos = endPos;
            holdTime = 0f;
        }
        // draw last part of line when mouse button released
        if (Input.GetMouseButtonUp(0)) {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawPath(startPos, endPos);
        }
    }
}
