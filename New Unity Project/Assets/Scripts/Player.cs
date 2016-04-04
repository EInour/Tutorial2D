using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float maxSpeed;
    public float speed;
    public float jumpPower;

    public bool grounded;
    private bool jumpState;
    private bool oldJumpState;
    private bool canDoubleJump;

    private Rigidbody2D rbPlayer;
    private Animator animator;

	void Start ()
    {
        maxSpeed = 3f;
        speed = 100;
        jumpPower = 250f;

        rbPlayer = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
	}
	
	void Update ()
    {

        Vector3 easeVelocity = rbPlayer.velocity;
        easeVelocity.y = rbPlayer.velocity.y;
        easeVelocity.z = 20f;
        easeVelocity.x *= 0.75f;


        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));


        //Fake friction

        if (grounded)
        {
            rbPlayer.velocity = easeVelocity;
            canDoubleJump = true;
        }


        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump");

        if (jumpState && !oldJumpState)
        {
            if(grounded)
            {
                rbPlayer.AddForce(Vector2.up * jumpPower);
                
            }
            else 
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
                    rbPlayer.AddForce(Vector2.up * jumpPower / 1.75f);
                }
            }
        }
	}

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        rbPlayer.AddForce((Vector2.right * speed) * h);

        if (rbPlayer.velocity.x > maxSpeed)
        {
            rbPlayer.velocity = new Vector2(maxSpeed, rbPlayer.velocity.y);
        }

        else if (rbPlayer.velocity.x < -maxSpeed)
        {
            rbPlayer.velocity = new Vector2(-maxSpeed, rbPlayer.velocity.y);
        }
    }
}
