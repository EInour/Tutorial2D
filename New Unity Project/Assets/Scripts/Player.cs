using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public float speed;
    public float jumpPower;

    public bool grounded;

    private Rigidbody2D rbPlayer;
	void Start ()
    {
        speed = 50f;
        jumpPower = 150f;

        rbPlayer = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        float h = Input.GetAxis("horizontal");

        rbPlayer.AddForce((Vector2.right * speed) * h);
    }
}
