using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public LayerMask whatIsInteractable;

    bool grounded;

    [Header("Interaction Check")]
    public float interactRange;


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    // Update is called once per frame
    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jump
        if (Input.GetKey(KeyCode.Space) && canJump && grounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //Interact
        if (Input.GetKey(KeyCode.E))
        {
            Interact();
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10f, ForceMode.Force);
        }

        SpeedControl();

     }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        canJump = true;
    }
    void Interact()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray) 
        { 
            //O OBJETO QUE O JOGADOR ESTA INTERAGINDO PRECISA TER O COMPONENTE ITERAGIVEL
            if (collider.TryGetComponent(out InteractableObj intr)){
               intr.InteractWith();
            }
        }
    }
}
