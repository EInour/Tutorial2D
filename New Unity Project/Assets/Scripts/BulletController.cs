using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public float speed;
    public Player player;
    public Player2 player2;

	// Use this for initialization
	void Start ()
    {
        player = FindObjectOfType<Player>();
        player2 = FindObjectOfType<Player2>();

        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
	}
    
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == ("Player2"))
        {
            player2.knockedBack = true;
            player2.autoAimed = true;
            Destroy(gameObject);
        }

        if (otherCollider.tag == ("Player1"))
        {
            player.knockedBack = true;
            player.autoAimed = true;
            Destroy(gameObject);
        }
    }


}
