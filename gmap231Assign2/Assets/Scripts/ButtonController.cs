using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //master button pressed bool
    private bool isPressed;
    //delta time factor
    public float pressSpeed = 4f;
    public int buttonId = 1;

    //get mesh rendered from component to allow for blend shape control
    private SkinnedMeshRenderer meshRenderer;
   

    // Start is called before the first frame update
    private void Start() {
        //get components from gameobject
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        //subscribe to events
        GameEvents.current.onButtonTriggerEnter += handleButtonPressed;
        GameEvents.current.onButtonTriggerExit += handleButtonUnPressed;
    }



    private void OnDisable() {
        //unsubscribe from events on gO disable
        GameEvents.current.onButtonTriggerEnter += handleButtonPressed;
        GameEvents.current.onButtonTriggerExit += handleButtonUnPressed;
    }

    private void handleButtonPressed(int id) {
        if (id == buttonId) {
            isPressed = true;
        }
    }

    private void handleButtonUnPressed(int id) {
        if (id == buttonId) {
            isPressed = false;
        }
    }


    // Update is called once per frame
    
    void Update()
    {
        //lerp from current blendShape to final based on onPressed
        float from = meshRenderer.GetBlendShapeWeight(0);
        float to = isPressed ? 100f : 0f;
        //set blend value on meshRenderer
        meshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(from, to, Time.deltaTime * pressSpeed));
    }
}
