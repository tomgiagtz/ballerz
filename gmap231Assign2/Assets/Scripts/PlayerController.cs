using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;


    //layers set in inspector
    public LayerMask coinLayer;
    public LayerMask groundLayer;


    //speed var
    public float speed = 10f;

    public float jumpForce = 10f;
    // gives jumps some extra oompf, keeps values lower
    private const float JUMP_MULITPLIER = 20f;

    //should jump in fixed update
    private bool willJump = false;
    // prevents infinite jumps
    private bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        //apply jump force vertically
        if (willJump) {
            rb.AddForce(Vector3.up * jumpForce * JUMP_MULITPLIER);
            //revert will jump after jumping
            willJump = false;
        }
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue jumpValue) {
        //if grounded, and hasnt already jumped, jump in fixed update
        if (!willJump && isGrounded) {
            willJump = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //handle coin layer pickups
        if (other.gameObject.layer == GetLayerMaskValue(coinLayer))
        {
            //send Coin Pickup event to event manager
            GameEvents.current.CoinPickup(other.gameObject.GetInstanceID());
        }

        // handle ground detection
        if (other.gameObject.layer == GetLayerMaskValue(groundLayer)) {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        //handle leaving ground
        if (other.gameObject.layer == GetLayerMaskValue(groundLayer)) {
            isGrounded = false;
        }
    }

    //convert layer mask value to int seen in Layer selector
    private int GetLayerMaskValue(LayerMask layerMask) {
        //layer mask value comes in as 2 ^ layerValue, use log to reduce to layer value
        return (int) Mathf.Log(layerMask, 2);
    }
}
