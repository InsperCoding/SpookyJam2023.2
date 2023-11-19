using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
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

    private float standingHeight;
    public float crouchOffset;
    private float timeToCrouch;
    private bool duringCrouchAnimation;
    public Transform cameraPosition;


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

    public List<AudioClip> RunningSounds;
    public AudioSource runAudioSource;
    private int pos;
    private Animator anim;

  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim=  GetComponent<Animator>();
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

        //Crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //StartCoroutine(CrouchStand());
            float targetHeight = Input.GetKey(KeyCode.LeftControl) ? standingHeight : standingHeight - crouchOffset;

            cameraPosition.position = new Vector3(cameraPosition.position.x,
                                                  cameraPosition.position.y - crouchOffset,
                                                  cameraPosition.position.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            cameraPosition.position = new Vector3(cameraPosition.position.x,
                                                  cameraPosition.position.y + crouchOffset,
                                                  cameraPosition.position.z);
        }

    }

    IEnumerator CrouchStand()
    {
        duringCrouchAnimation = true;
        float timeElapsed = 0f;
        float targetHeight = Input.GetKey(KeyCode.LeftControl) ? standingHeight : standingHeight - crouchOffset;
        Debug.Log("Crouch");
        while (timeElapsed < timeToCrouch) {
            Debug.Log("loop time");
            cameraPosition.position = new Vector3(cameraPosition.position.x ,
                Mathf.Lerp(cameraPosition.position.y, cameraPosition.position.y - standingHeight + targetHeight, timeElapsed),
                cameraPosition.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        duringCrouchAnimation = false;
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            playRunningSound();
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10f, ForceMode.Force);
        }

        SpeedControl();
        if (rb.velocity != Vector3.zero)
        {
            anim.SetBool("isWalking", true);
            Vector3 p = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            p.Normalize();
            Quaternion toRotation = Quaternion.LookRotation(p , Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,  1);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

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

    public void playRunningSound(){
        if ((horizontalInput != 0 || verticalInput != 0) && !runAudioSource.isPlaying) {
                pos = (int)Mathf.Floor(Random.Range(0, RunningSounds.Count));
                runAudioSource.PlayOneShot(RunningSounds[pos]);
        }
    }
}