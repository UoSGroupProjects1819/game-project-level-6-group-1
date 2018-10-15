using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [Tooltip("Tick this if this is player one.")]
    public bool isPlayerOne;
    [Tooltip("Tick this if this is player two.")]
    public bool isPlayerTwo;

    [SerializeField] [Tooltip("The speed at which the player will walk.")]
    private float walkSpeed;
    [SerializeField] [Tooltip("The speed at which the player will spring. NOT YET IMPLEMENTED.")]
    private float sprintSpeed;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

	void Start ()
    {
        if (!isPlayerOne && !isPlayerTwo)
            Debug.LogError("Missing player number, at: " + gameObject.name);

        if (isPlayerOne && isPlayerTwo)
            Debug.LogError("Player cannot be P1 & P2 at the same time! Error at: " + gameObject.name);

        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        Vector2 moveInput;

        if (isPlayerOne)
        {
            moveInput = new Vector2(Input.GetAxisRaw("POneHor"), Input.GetAxisRaw("POneVer"));
        }
        else
        {
            moveInput = new Vector2(Input.GetAxisRaw("PTwoHor"), Input.GetAxisRaw("PTwoVer"));
        }

        moveVelocity = moveInput.normalized * walkSpeed;
	}

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
    }
}
