using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public int Stage;
    private Color outlineDefault = new Color(0.151f, 0.151f, 0.151f, 1.000f);
    private Color outlineSelected = new Color(0.184f, 1.000f, 0.000f, 1.000f);
    private Color outlineHighlight = new Color(0.736f, 0.736f, 0.736f, 1.000f);

    private bool selected;

    #region Getters and Setters
    public bool Selected {
        get {return selected;} set {selected = value;}
    }
    public Color OutlineDefault {
        get {return outlineDefault;}
    }
    public Color OutlineSelected {
        get {return outlineSelected;}
    }
    #endregion

    public void OnPointerEnter(PointerEventData eventData) {
        GetComponent<Outline>().effectColor = outlineHighlight;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (selected)
            GetComponent<Outline>().effectColor = outlineSelected;
        else
            GetComponent<Outline>().effectColor = outlineDefault;
    }
}
