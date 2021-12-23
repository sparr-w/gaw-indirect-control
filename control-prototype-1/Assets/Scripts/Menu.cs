using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public MenuButton Selected;

    public void Select(MenuButton stage) {
        if (Selected != null) {
            Selected.Selected = false;
            Selected.GetComponent<Outline>().effectColor = Selected.OutlineDefault;
        }
        Selected = stage;
        Selected.Selected = true;
        Selected.GetComponent<Outline>().effectColor = Selected.OutlineSelected;
    }

    public void StartLevel() {
        if (Selected != null) {
            Level.Selected = Selected.Stage;
            SceneManager.LoadScene("Game");
        }
    }

    public void Quit() {
        Application.Quit();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
