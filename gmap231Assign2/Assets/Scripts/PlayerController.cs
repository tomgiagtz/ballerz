using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // public PlayerInput playerInput;

    public float speed = 10f;


    public float jumpForce = 10f;
    private const float JUMP_MULITPLIER = 20f;
    private bool willJump = false;
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
        Debug.Log("Jump");
        if (!willJump) {
            willJump = true;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            // int id = other.gameObject.GetInstanceID();
            GameEvents.current.CoinPickup(other.gameObject.GetInstanceID());
            // other.gameObject.SetActive(false);
        }
    }
}
