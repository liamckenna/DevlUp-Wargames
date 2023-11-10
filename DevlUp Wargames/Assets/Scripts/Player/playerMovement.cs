using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float walkSpeed;
    public bool sprinting;
    public float sprintSpeed;
    public bool canSprint;
    public float thrust;
    public float aerialSprintSpeed;
    public float aerialWalkSpeed;
    public float influenceMultiplier;
    public Transform orientation;
    public Camera camera;
    public Gravity grav;
    public GameObject marker;
    public float gravity = -1;
    float horizontalInput;
    float verticalInput;
    Vector3 movementDirection;
    Vector3 slideDirection;
    Rigidbody rb;
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    bool wasGrounded;
    public bool crouching;
    public bool walking;
    public float crouchSpeed;
    public bool crouchBuffer;
    public bool sliding;
    public float maxSlideSpeed;
    public float slideDeceleration;
    float slideTime;
    public float maxSlideTime;
    float slideSpeed; 
    public bool canSlide;
    public float slideCooldown;
    bool buffering;
    bool phantomGrounded;
    bool onStairs;
    float defaultThrust;
    float ledgeGrabHeight = 0;
    public float bufferTime;
    public float friction;
    public float slidingFriction;
    void Start()
    {    
        defaultThrust = thrust;
        grounded = true;
        rb = GetComponent<Rigidbody>();
        grav = GetComponent<Gravity>();
        rb.freezeRotation = true;
        sprinting = false;
        onStairs = false;
        canSlide = true;
    }

    // Update is called once per frame
    void Update()
    {
        wasGrounded = grounded;
        if (!wasGrounded && grounded) {
            //Land this frame
        } else if (wasGrounded && !grounded) {
            //Leave ground this frame
            if (rb.velocity.y <= 0) {
                StartCoroutine(PhantomState());
            }
        }
        PlayerInput();
        if (grounded && !sliding) rb.drag = friction;
        else if (sliding) rb.drag = slidingFriction;
        else rb.drag = 0;
        if (rb.velocity.magnitude < 5) sliding = false;


        

    }

    void FixedUpdate() {
        PlayerMove();
        VelocityLimiter();
        LedgeGrabCheck();
        //if (sliding && crouching) rb.AddForce(slideDirection * slideSpeed * 10f, ForceMode.Force);
        //slideSpeed -= slideDeceleration;
        
    }

    private void PlayerInput() {
        //walk and sprint
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (grounded && Input.GetButtonDown("Sprint")) {
            if (!sprinting && !crouching) {
            sprinting = true;
            } else sprinting = false;
        } else if (grounded && sprinting )
        if (!grounded || Input.GetAxisRaw("Vertical") < 1) {
            sprinting = false;
        }
        
        //jump
        if (Input.GetButtonDown("Jump") || buffering) {
            
            if (sliding) {
            crouchBuffer = true;
            buffering = true;
            return;
            } else if (grounded || LedgeGrabCheck()) {
                if (!buffering && crouching) {
                    Crouch();
                }
                else Jump();
            }
            else if (!buffering) StartCoroutine(JumpBuffer());
        }
        //crouch
        if (Input.GetButtonDown("Change Stance") || crouchBuffer) {
            if (!sliding && !sprinting && grounded || (sprinting && !canSlide && grounded)) {
                Crouch();
                sprinting = false;
                if (crouchBuffer) {
                    crouchBuffer = false;
                    if (!crouching) sprinting = true;
                }
            }
            else if (sprinting && grounded && canSlide) {
                Crouch();
                StartCoroutine(Slide());
            } else crouchBuffer = true;
        }
        if (grounded && !sprinting && !crouching && rb.velocity.magnitude > 0.5f) {
            walking = true;
        } else walking = false;
    }

    private void PlayerMove() {
        movementDirection = orientation.forward * verticalInput;
        movementDirection += orientation.right * horizontalInput;
        if (grounded && !sliding) influenceMultiplier = 1;
        else if (sliding) influenceMultiplier = 0;
        else influenceMultiplier = 0.25f;
        if (!sprinting && !sliding) {
            rb.AddForce(movementDirection.normalized * walkSpeed * 10f * influenceMultiplier, ForceMode.Force);
        } else if (sprinting) {
            rb.AddForce(movementDirection.normalized * sprintSpeed * 10f * influenceMultiplier, ForceMode.Force);
        }
    }

    private void VelocityLimiter() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (grounded && !sprinting && !crouching && flatVel.magnitude > walkSpeed) {
            Vector3 limitedVelocity = flatVel.normalized * walkSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
        else if (grounded && sprinting && flatVel.magnitude > sprintSpeed) {
            Vector3 limitedVelocity = flatVel.normalized * sprintSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        } else if (!grounded && sprinting && flatVel.magnitude > aerialSprintSpeed) {
            Vector3 limitedVelocity = flatVel.normalized * aerialSprintSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        } else if (!grounded && !sprinting && flatVel.magnitude > aerialWalkSpeed) {
            Vector3 limitedVelocity = flatVel.normalized * aerialWalkSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        } else if (grounded && !sliding && crouching && flatVel.magnitude > crouchSpeed) {
            Vector3 limitedVelocity = flatVel.normalized * crouchSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump() {
        if (LedgeGrabCheck() && !grounded && !phantomGrounded) {
            thrust = 10 + 6*(0.5f - ledgeGrabHeight);
        }
        Debug.Log(thrust);
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (crouchBuffer) thrust *= 1.35f;
        rb.AddForce(transform.up * thrust, ForceMode.Impulse);
        phantomGrounded = false;
        buffering = false;
        if (LedgeGrabCheck() && !grounded && !phantomGrounded ) {
            thrust = defaultThrust;
        }
        if (crouchBuffer) thrust = defaultThrust;
    }
    
    private IEnumerator Slide() {

        sliding = true;
        slideDirection = camera.transform.forward;
        slideSpeed = maxSlideSpeed;
        rb.AddForce(slideDirection * slideSpeed, ForceMode.Force);
        sprinting = false;
        yield return new WaitForSeconds(maxSlideTime);
        //sliding = false;
        //StartCoroutine(SlideCooldown());
    }
    private IEnumerator SlideCooldown() {
        canSlide = false;
        yield return new WaitForSeconds (slideCooldown);
        canSlide = true;
    }
    private void Crouch() {
        crouching = !crouching;
            if (crouching) {
                transform.localScale = new Vector3(1, 0.5f, 1);
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                camera.transform.parent.transform.position = new Vector3(camera.transform.parent.transform.position.x, camera.transform.parent.transform.position.y - .35f, camera.transform.parent.transform.position.z);
            }
            if (!crouching) {
                transform.localScale = new Vector3(1, 1, 1);
                camera.transform.parent.transform.position = new Vector3(camera.transform.parent.transform.position.x, camera.transform.parent.transform.position.y + .35f, camera.transform.parent.transform.position.z);
            }
    }

    private IEnumerator JumpBuffer() {
        buffering = true;
        yield return new WaitForSeconds (bufferTime);
        buffering = false;
    }

    private IEnumerator PhantomState() {
        phantomGrounded = true;
        yield return new WaitForSeconds (0.1f);
        phantomGrounded = false;
    }

    private bool LedgeGrabCheck() {
        RaycastHit topLedge;
        Vector3 topRay = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        Vector3 rayForward = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        Physics.Raycast(topRay, rayForward, out topLedge, 1f);
        Debug.DrawRay(topRay, rayForward, Color.red);
        RaycastHit bottomLedge;
        Vector3 bottomRay = new Vector3(camera.transform.position.x, camera.transform.position.y - .5f, camera.transform.position.z);
        Physics.Raycast(bottomRay, rayForward, out bottomLedge, 1f);
        Debug.DrawRay(bottomRay, rayForward, Color.green);
        Vector3 horizontalRay = new Vector3(bottomLedge.point.x + 0.01f, bottomLedge.point.y + 0.5f, bottomLedge.point.z + 0.01f);
        RaycastHit downwardsRay;
        Physics.Raycast(horizontalRay, new Vector3(0, -1, 0), out downwardsRay, 0.5f);
        Debug.DrawRay(horizontalRay, new Vector3(0, -1, 0), Color.yellow);

        if (topLedge.collider == null && bottomLedge.collider != null) {
            marker.SetActive(true);
            marker.transform.position = downwardsRay.point;
        } else marker.SetActive(false);


        if (downwardsRay.collider != null && marker.transform.position != new Vector3(0.01f, 0.5f, 0.01f) && topLedge.collider == null && bottomLedge.collider != null) {
            ledgeGrabHeight = downwardsRay.distance;
        } else ledgeGrabHeight = 0;
        if (topLedge.collider == null && bottomLedge.collider != null) return true;
        else return false;
    }
    

    private void Gravity() {
        Vector3 gravityVector = gravity * Vector3.up;
        if (!grounded) rb.AddForce(gravityVector);
    }
    

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Stairs") {
            onStairs = true;
        }
    }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Stairs" && transform.position.y - transform.localScale.y < other.transform.position.y + other.transform.localScale.y/2) {
            rb.position = new Vector3 (rb.position.x, other.transform.position.y + other.transform.localScale.y/2 + transform.localScale.y, rb.position.z);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Stairs") {
            onStairs = false;
        }
    }
}
