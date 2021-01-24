using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public LayerMask coinLayer;
    public LayerMask groundLayer;

    // public PlayerInput playerInput;

    public float speed = 10f;


    public float jumpForce = 10f;
    private const float JUMP_MULITPLIER = 20f;
    private bool willJump = false;
    public bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // playerInput = new PlayerInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
        if (willJump) {
            rb.AddForce(Vector3.up * jumpForce * JUMP_MULITPLIER);
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
        if (!willJump && isGrounded) {
            willJump = true;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GetLayerMaskValue(coinLayer))
        {
            GameEvents.current.CoinPickup(other.gameObject.GetInstanceID());
        }

        if (other.gameObject.layer == GetLayerMaskValue(groundLayer)) {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == GetLayerMaskValue(groundLayer)) {
            isGrounded = false;
        }
    }
    private int GetLayerMaskValue(LayerMask layerMask) {
        return (int) Mathf.Log(layerMask, 2);
    }
}
