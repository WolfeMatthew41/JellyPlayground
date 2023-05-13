using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsWidget : MonoBehaviour
{
    [SerializeField] private string textToDisplay;
    [SerializeField] private GameObject box;

    private RectTransform boxRectTf;
    private TMPro.TMP_Text displayText;

    private void Start() {
        displayText = box.GetComponent<TMPro.TMP_Text>();
        displayText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag.Equals("Player"))
            displayText.text = textToDisplay;
    }

}
