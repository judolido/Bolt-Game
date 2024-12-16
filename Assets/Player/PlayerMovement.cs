using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    int runningBoolHash;
    int jumpTriggerHash;
    int slidingTriggerHash;
    int groundedBoolHash;
    int speedFloatHash;
    float horizontalInput;
    float verticalInput;
    public float speed = 5;
    public float jumpForce = 10;
    public Rigidbody rb;
    CapsuleCollider capsule;
    float defaultHeight;
    Vector3 defaultCenter;
    float animationDuration;
    float animationStart = -1;
    public AnimationCurve slideHeightCurve;
    
    bool grounded;
    bool sliding;
    float playerTargetAngle;
    float groundCheckDistance;
    
    public float rotationSpeed = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator) Debug.LogError("Animator is NULL!");
        
        runningBoolHash = Animator.StringToHash("Running");
        jumpTriggerHash = Animator.StringToHash("Jump");
        slidingTriggerHash = Animator.StringToHash("Slide");
        groundedBoolHash = Animator.StringToHash("Grounded");
        speedFloatHash = Animator.StringToHash("Speed");
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        defaultHeight = capsule.height;
        defaultCenter = capsule.center;
    }

    private void FixedUpdate ()
    {
        Vector3 forwardMove = transform.forward * (speed * Time.fixedDeltaTime);
        Vector3 horizontalMove = transform.right * (horizontalInput * speed * Time.fixedDeltaTime);
        // Vector3 verticalMove = transform.up * speed * Time.fixedDeltaTime * verticalInput;

        rb.MovePosition(rb.position + forwardMove); // + horizontalMove);
    }

    // Update is called once per frame
    private void Update()
    {
        groundCheckDistance = capsule.height/2 + 0.02f; // add small offset to account for error
        
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && grounded && !sliding)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger(jumpTriggerHash);
        }
        else if (Input.GetKeyDown(KeyCode.S) && grounded)
        {
            animator.SetTrigger(slidingTriggerHash);
        }
        
        // Collision capsule size adjusting
        if ((int)animationStart != -1)
        {
            float curvePos = (Time.time - animationStart) / animationDuration;
            float newRatio = slideHeightCurve.Evaluate(curvePos);
            
            capsule.center = defaultCenter*newRatio;
            capsule.height = defaultHeight*newRatio;
        }
        
        // Smoothly rotate character
        playerTargetAngle = Mathf.Atan2(horizontalInput * speed, speed) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.y;
        float newAngle = Mathf.LerpAngle(currentAngle, playerTargetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, newAngle, 0);
        
        RaycastHit hit;
        grounded = Physics.Raycast(rb.position + capsule.center, -transform.up, out hit, groundCheckDistance);
        
        animator.SetBool(groundedBoolHash, grounded);
        animator.SetFloat(speedFloatHash, speed * 0.2f);
        animator.SetBool(runningBoolHash, (speed > 0.001f));
    }

    public Vector3 GetVelocity()
    {
        return new Vector3(horizontalInput * speed, rb.linearVelocity.y, speed); // linearVelocity will be 0 because of rb.MovePosition
    }
    
    public void StartSliding(float duration)
    {
        if (duration == 0)
        {
            Debug.LogWarning("Duration is zero");
            return;
        }
        animationDuration = duration;
        animationStart = Time.time;
        sliding = true;
    }
    
    public void FinishSliding()
    {
        sliding = false;
        animationStart = -1;
    }
}
