﻿using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour
{

    public float maxSpeed;
    public float speed;
    public float jumpPower;

    public bool grounded;
    private bool jumpState;
    private bool oldJumpState;

    private Rigidbody2D rbPlayer;
    private Animator animator;

    void Start()
    {
        maxSpeed = 3f;
        speed = 50f;
        jumpPower = 250f;

        rbPlayer = gameObject.GetComponent<Rigidbody2D>();

        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal2")));

        if (Input.GetAxis("Horizontal2") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal2") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump2");

        if (jumpState && !oldJumpState && grounded)
        {
            rbPlayer.AddForce(Vector2.up * jumpPower);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal2");

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