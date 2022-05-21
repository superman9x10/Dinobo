using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float movingSpeed;
    public float jumpForce;
    public bool canDoubleJump;
    public bool isTurnRight = true;

    private bool isOnGround;

    public Transform groundCheckPoint;
    public Animator animator;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    private Vector2 moveDir;
    private Vector2 mousePos;

    public Vector2 getMousePos()
    {
        return this.mousePos;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 300;
    }
    private void Update()
    {
        playerControl();

    }

    void playerControl()
    {

        getInput();
        checkGround();
        lookProcess();
        
    }

    public void getInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        moveDir = new Vector2(h, 0);

  
        walking(moveDir);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }
    void jump()
    {
        if (isOnGround)
        {
            rb.velocity = Vector2.up * jumpForce;
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            rb.velocity = Vector2.up * jumpForce;
            
        }

    }

    void lookProcess()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && isTurnRight)
        {
            flip(180);
        }
        else if(mousePos.x > transform.position.x && !isTurnRight)
        {
            flip(0);
        }
    }

    public void flip(float deg)
    {
        isTurnRight = !isTurnRight;
        transform.eulerAngles = new Vector3(transform.rotation.x, deg, transform.rotation.z);
    }
    void walking(Vector2 moveDir)
    {
        checkWalking(moveDir.x);
        
        rb.velocity = new Vector2(moveDir.x * movingSpeed, rb.velocity.y);
    }

    void checkWalking(float dir)
    {
        if (dir != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void checkGround()
    {
        if (Physics2D.OverlapCircle(groundCheckPoint.position, 0.15f, groundLayer)) {
            isOnGround = true;
        } else
        {
            isOnGround = false;
        }
    }

}
