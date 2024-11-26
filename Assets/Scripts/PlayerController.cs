using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; // reference to the player controller

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 5f;
    private Vector3 moveDirection;
    public CharacterController charController;
    private Camera theCam;
    public GameObject playerModel;
    public float rotateSpeed = 15f;
    public Animator anim;

    public bool isKnocking;
    public float knockBackLength = 0.5f;
    private float knockBackCounter;
    public Vector2 knockBackPower;

    public GameObject[] playerPieces;

    private void Awake()
    {
        instance = this; // set the instance to this player controller
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main; // get the main camera
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking)
        {
            float yStore = moveDirection.y; // store the y value of the move direction
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal")); // get the input from the player
            moveDirection.Normalize(); // normalize the input that is the player can't move faster diagonally
            moveDirection = moveDirection * moveSpeed; // multiply the input by the move speed
            moveDirection.y = yStore; // set the y value of the move direction to the stored y value

            if (charController.isGrounded) // if the player is on the ground
            {
                // moveDirection.y = 0f;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            // transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed); // move the player
            charController.Move(moveDirection * Time.deltaTime); // move the player

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // if the player is moving
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.eulerAngles.y, 0f); // rotate the player to face the direction of the camera
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z)); // for the player to face the direction of the movement
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime); // rotate the player model
            }
        }

        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockBackPower.x;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f; 
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z)); // set the speed of the player model
        anim.SetBool("Grounded", charController.isGrounded); // set the grounded state of the player model for the jump animation
    }

    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("Knockback");
        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }
}
