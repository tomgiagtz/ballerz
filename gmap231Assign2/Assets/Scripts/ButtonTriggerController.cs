using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerController : MonoBehaviour
{
    ButtonController parent;
    public int buttonId;

    private void Awake() {
        parent = GetComponentInParent<ButtonController>();
        buttonId = parent.buttonId;
    }
    private void OnTriggerEnter(Collider other) {
        GameEvents.current.ButtonTriggerEnter(buttonId);
    }

    private void OnTriggerExit(Collider other) {
        GameEvents.current.ButtonTriggerExit(buttonId);
    }
}
