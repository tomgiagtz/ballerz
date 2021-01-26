using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerController : MonoBehaviour
{
    //parent ref to get id
    ButtonController parent;
    public int buttonId;

    private void Awake() {
        //get id from parent
        parent = GetComponentInParent<ButtonController>();
        buttonId = parent.buttonId;
    }
    private void OnTriggerEnter(Collider other) {
        // send trigger to game event manager
        GameEvents.current.ButtonTriggerEnter(buttonId);
    }

    private void OnTriggerExit(Collider other) {
        // send trigger to game event manager
        GameEvents.current.ButtonTriggerExit(buttonId);
    }
}
