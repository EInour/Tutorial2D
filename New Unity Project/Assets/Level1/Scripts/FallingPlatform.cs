﻿using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;

	public float fallDelay;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Invoke ("Fall", fallDelay);
		}
	}

	void Fall()
	{
		rb2d.isKinematic = false;
	}
}
