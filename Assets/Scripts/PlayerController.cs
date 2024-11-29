using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; // reference to the player controller

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 5f;
    public float bounceForce = 8f;
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

    public bool stopMove;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main; // get the main camera
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking && !stopMove) // if the player is not knocking back
        {
            float yStore = moveDirection.y;

            // รับค่าเคลื่อนไหวเฉพาะในแกน X และล็อกแกน Z
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, 0f, 0f);
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }

            // เพิ่มแรงโน้มถ่วง
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            // เคลื่อนที่ Player
            charController.Move(moveDirection * Time.deltaTime);

            // จัดการการหมุนตัวของ Player
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                Quaternion newRotation = Quaternion.LookRotation(Vector3.right * Input.GetAxisRaw("Horizontal")); // หันหน้า Player ตามแกน X
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }

        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = new Vector3(-knockBackPower.x, 0f, 0f); // เคลื่อนที่ในแกน X เท่านั้น
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

        if (stopMove)
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection);
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x)); // ใช้เฉพาะแกน X
        anim.SetBool("Grounded", charController.isGrounded);
    }

    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("Knockback");
        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }
}
