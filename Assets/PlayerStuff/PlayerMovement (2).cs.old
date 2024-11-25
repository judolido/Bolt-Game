using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public float speed = 5;
    public float jumpForce = 10;
    public Rigidbody rb;

    float horizontalInput;
    float verticalInput;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }





    private void FixedUpdate () {

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
        // Vector3 verticalMove = transform.up * speed * Time.fixedDeltaTime * verticalInput;
        
        rb.MovePosition(rb.position +  forwardMove + horizontalMove);
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed");

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (animator != null)
            {
                animator.Play("Jumping");
                animator.SetTrigger("Jump");
                Debug.Log("Jump Trigger Set");
            }
            else
            {
                Debug.LogError("Animator is NULL!");
            }
        }

    }


}
