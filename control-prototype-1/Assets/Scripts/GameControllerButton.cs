using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameControllerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject Label;

    public void OnPointerEnter(PointerEventData eventData) {
        Label.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Label.SetActive(false);
    }

    private void Update() {
        // grow when hovered over
        // shrink when no longer hovered over
    }
}
