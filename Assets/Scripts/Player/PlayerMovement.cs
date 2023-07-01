using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float rollSpeed = 1.3f;
    [SerializeField] private float jumpCooldown = 0.4f;     // increase to limit jumping
    [SerializeField] private float rollCooldown = 0.4f;     // increase to limit rolling
    [SerializeField] private float airMultiplier = 0.4f;


    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode rollKey = KeyCode.LeftShift;


    [Header("Weapon Reference")]
    [SerializeField] public CapsuleCollider weapon;


    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 0.0255f; // 0.0255 for orc  |  0.62 for knight
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;


    [Header("Inputs")]
    private float sidewaysInput;
    private float frontbackInput;
    private bool jumpInput;
    private bool rollInput;


    [Header("Movement")]
    private Vector3 moveDirection;
    bool readyToJump;
    bool readyToRoll;
    int jumpStamina = 20;
    int rollStamina = 15;


    [Header("References")]
    public Transform playerObj;
    public ThirdPersonCam cam;
    [SerializeField] private Transform orientation;
    private Rigidbody rb;
    private CapsuleCollider cc;
    private Animator playerAnim;
    private PlayerLife playerLife;
    private PlayerCombat playerCombat;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        playerAnim = GetComponent<Animator>();
        playerLife = GetComponent<PlayerLife>();
        playerCombat = GetComponent<PlayerCombat>();
        rb.freezeRotation = true;
        readyToJump = true;
        readyToRoll = true;
    }

    private void Update()
    {
        GroundCheck();
        MyInput();
        MovePlayer();
    }

    private void MyInput()
    {
        // getting input from keyboard
        sidewaysInput = Input.GetAxisRaw("Horizontal");
        frontbackInput = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetKeyDown(jumpKey);
        rollInput = Input.GetKeyDown(rollKey);

        if (
            jumpInput
            && readyToJump
            && isGrounded
            && !playerAnim.GetBool("roll")
            && !playerAnim.GetBool("jump")
            && !playerAnim.GetBool("Attack2")
            && playerLife.GetStaminaValue() >= jumpStamina
            && !playerLife.isDead
            )
        {
            // jumping
            readyToJump = false;
            playerAnim.SetBool("jump", true);
            playerLife.ConsumeStamina(jumpStamina);
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (
            rollInput
            && readyToRoll
            && isGrounded
            && !playerAnim.GetBool("jump")
            && !playerAnim.GetBool("roll")
            && !playerAnim.GetBool("Attack2")
            && playerAnim.GetInteger("state") != 0
            && playerLife.GetStaminaValue() >= rollStamina
            && !playerLife.isDead
            )
        {
            // rolling
            readyToRoll = false;
            playerAnim.SetBool("roll", true);
            playerLife.ConsumeStamina(rollStamina);
            cam.lockRotation = true;
            Invoke(nameof(ResetRoll), rollCooldown);
        }

        // Debug.Log(playerAnim.GetBool("jump") + " " + playerAnim.GetBool("roll") + " " + sidewaysInput + " " + frontbackInput + " " + playerAnim.GetInteger("state"));

        int state;
        if (!isGrounded && playerLife.isDead)
            return;
        if (sidewaysInput == 0 && frontbackInput == 0)
        {
            // idle
            state = 0;
        }
        else
        {
            // running
            state = 1;
        }
        playerAnim.SetInteger("state", state);
    }

    private void MovePlayer()
    {
        // add movement to rigidbody 
        if (!playerAnim.GetBool("roll") && !playerCombat.attack2)
            moveDirection = orientation.forward * frontbackInput + orientation.right * sidewaysInput;
        moveDirection.y = 0f;

        if (moveDirection != Vector3.zero && !playerAnim.GetBool("roll") && !playerCombat.attack2)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, moveDirection.normalized, Time.deltaTime * rotationSpeed);
        }
        if (!playerCombat.attack2 && playerAnim.GetBool("Attack2"))
        {
            rb.velocity = new Vector3(0, 0, 0);
            return;
        }
        if (playerAnim.GetBool("roll") || playerCombat.attack2)
        {
            Vector3 dir = moveDirection.normalized * moveSpeed * rollSpeed;
            rb.velocity = new Vector3(dir.x, dir.y, dir.z);
        }
        else
        {
            if (isGrounded)
            {
                Vector3 dir = moveDirection.normalized * moveSpeed;
                rb.velocity = new Vector3(dir.x, dir.y, dir.z);
            }
            else
            {
                Vector3 dir = moveDirection.normalized * moveSpeed * airMultiplier;
                rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);
            }
        }
    }

    private void GroundCheck()
    {
        float offset = 0.001f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + offset, whatIsGround);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    private void ResetRoll()
    {
        readyToRoll = true;
    }

    public void rollEnd()
    {
        playerAnim.SetBool("roll", false);
        weapon.enabled = true;
        cam.lockRotation = false;
    }

    public void jumpEnd()
    {
        playerAnim.SetBool("jump", false);
    }
}
