using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private AudioSource audio;
    private Rigidbody2D rb;
    private int groundsInTrigger = 0;
    private bool blockHorizontalMovement = false;

    [SerializeField] private int movementSpeed = 2;
    [SerializeField] private int jumpForce = 7;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void blockMovement()
    {
        movementSpeed = 0;
        jumpForce = 0;
    }

    private void FixedUpdate()
    {
        buildMovement();
        buildJump();
    }

    private void buildMovement()
    {
        if (!blockHorizontalMovement)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal != 0)
            {
                rb.velocityX = horizontal * movementSpeed;
            }
        }
    }

    private void buildJump()
    {
        if (groundsInTrigger != 0 && Input.GetAxisRaw("Jump") != 0)
        {
            audio.Play();
            rb.velocityY = jumpForce;
        }


        /*print("velocity " + rb.velocityY);

        if (Input.GetButtonDown("Jump"))
        {
            currentJumpForce = jumpForce;
        }

        if (Input.GetAxisRaw("Jump") !=0)
        {
            if (currentJumpForce < 7)
            {
                print("current : " + currentJumpForce);
                currentJumpForce += 4*Time.deltaTime;
            }
            else
            {
                audio.Play();
                rb.velocityY = currentJumpForce;
            }
        }
        else
        {
            currentJumpForce = 0;
        }

        if (Input.GetAxisRaw("Jump") == 0)
        {
            print("jump for : " + currentJumpForce);

            audio.Play();
            rb.velocityY = currentJumpForce;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HardBlock" || collision.tag == "Platform")
        {
            groundsInTrigger++;
            blockHorizontalMovement = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "HardBlock" || collision.tag == "Platform")
        {
            groundsInTrigger--;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (groundsInTrigger == 0)
            {
                blockHorizontalMovement = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        blockHorizontalMovement = false;
    }
}
