using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ink : MonoBehaviour {
    public float Amount;
    public RectTransform ProgressBar;
    public Text PercentageText;

    private float remaining;
    private float progressBarLength;
    private float percentage;

    #region Setters and Getters
    public float Percentage {
        get {return percentage;}
    }

    public float Remaining {
        get {return remaining;} set {remaining = value;}
    }
    #endregion

    public void SetActive(bool value) {
        gameObject.SetActive(value);
    }

    private void Start() {
        progressBarLength = ProgressBar.sizeDelta.x;
    }

    public void Refill() {
        remaining = Amount;
    }

    private void Update() {
        percentage = (remaining / Amount) * 100f;
        PercentageText.text = Mathf.CeilToInt(percentage) + "%";
        ProgressBar.sizeDelta = new Vector2(progressBarLength * (percentage/100f), ProgressBar.rect.size.y);
    }
}
