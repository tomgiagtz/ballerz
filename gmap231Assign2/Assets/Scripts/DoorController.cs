using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int doorId;
    public float openHeight = 1.5f;
    public float openTime = 1f;

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        //save initial position
        startPosition = transform.position;

        GameEvents.current.onButtonTriggerEnter += handleDoorOpen;
        GameEvents.current.onButtonTriggerExit += handleDoorClose;
    }

    void handleDoorOpen(int id) {
        if (id == doorId) {
            LeanTween.moveLocalY(gameObject, startPosition.y + openHeight, openTime).setEaseOutQuad();
        }
    }

    void handleDoorClose(int id) {
        if (id == doorId) {
            LeanTween.moveLocalY(gameObject, startPosition.y, openTime).setEaseInQuad();
        }
    }
}
