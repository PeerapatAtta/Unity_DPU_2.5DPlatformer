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
    private Camera theCam;
    public GameObject playerModel;
    public float rotateSpeed = 15f;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main; // get the main camera
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y; // store the y value of the move direction
        // moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));// get the input from the player
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal")); // get the input from the player
        moveDirection.Normalize(); // normalize the input that is the player can't move faster diagonally
        moveDirection = moveDirection * moveSpeed; // multiply the input by the move speed
        moveDirection.y = yStore; // set the y value of the move direction to the stored y value

        if (charController.isGrounded) // if the player is on the ground
        {
            // moveDirection.y = 0f; // set the y value of the move direction to 0
            if (Input.GetButtonDown("Jump")) // if the player presses the jump button
            {
                moveDirection.y = jumpForce; // jump
            }
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale; // apply gravity to the player

        // transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed); // move the player
        charController.Move(moveDirection * Time.deltaTime); // move the player

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // if the player is moving
        {
            transform.rotation = Quaternion.Euler(0f, theCam.transform.eulerAngles.y, 0f); // rotate the player to face the direction of the camera
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z)); // for the player to face the direction of the movement
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime); // rotate the player model
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z)); // set the speed of the player model
        anim.SetBool("Grounded", charController.isGrounded); // set the grounded state of the player model for the jump animation
    }
}
