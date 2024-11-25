using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //move and jump speed
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 5f;

    private Vector3 moveDirection;

    public CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y; // store the y value of the move direction
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));// get the input from the player
        moveDirection = moveDirection * moveSpeed; // multiply the input by the move speed
        moveDirection.y = yStore; // set the y value of the move direction to the stored y value

        if (Input.GetButtonDown("Jump")) // if the player presses the jump button
        {
            moveDirection.y = jumpForce; // jump
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale; // apply gravity to the player

        // transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed); // move the player
        charController.Move(moveDirection * Time.deltaTime); // move the player
    }
}
