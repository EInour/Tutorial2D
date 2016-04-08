using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float maxSpeed;
    public float speed;
    public float jumpPower;
    public float speedBoostTimer = 0;

    public bool grounded;
    public int doubleJump;
    private bool HasDoubleJumped;
    private bool jumpState;
    private bool oldJumpState;
    public bool knockedBack;
    public bool autoAimed;

    private Rigidbody2D rbPlayer;
    public Player2 player2;
    private Animator animator;


    public Transform firePoint;
    public GameObject laserBullet;


    void Start()
    {
        maxSpeed = 3f;
        speed = 50f;
        jumpPower = 250f;

        doubleJump = 0;

        rbPlayer = gameObject.GetComponent<Rigidbody2D>();

        animator = gameObject.GetComponent<Animator>();

        player2 = player2.GetComponent<Player2>();
    }

    void Update()
    {

        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            maxSpeed = 10f;
        }

        if (speedBoostTimer <= 0)
        {
            maxSpeed = 6f;
        }

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

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


        //DoubleJump
        if (jumpState && !oldJumpState && !grounded && (doubleJump > 0) && !HasDoubleJumped)
        {
            HasDoubleJumped = true;

            rbPlayer.AddForce(Vector2.up * jumpPower);

            doubleJump -= 1;
        }


        //SingleJump
        if (jumpState && !oldJumpState && grounded)
        {
            HasDoubleJumped = false;

            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);

            rbPlayer.AddForce(Vector2.up * jumpPower);
        }

        //Shoot
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(laserBullet, firePoint.position, firePoint.rotation);
        }

        if (player2.autoAimed)
        {
            Instantiate(laserBullet, firePoint.position, firePoint.rotation);
            player2.autoAimed = false;
        }
    }

    void FixedUpdate()
    {
        if (knockedBack)
        {
            rbPlayer.AddForce(Vector2.left * speed);
            knockedBack = false;
        }

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DoubleJump"))
        {
            other.gameObject.SetActive(false);
            doubleJump += 1;
        }

        else if (other.gameObject.CompareTag("SpeedBoost"))
        {
            other.gameObject.SetActive(false);
            speedBoostTimer = 2;
        }
    }

}
