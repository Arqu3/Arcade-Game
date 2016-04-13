﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 10.0f;
	public float jumpVelocity = 700.0f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    bool isOnGround = false;
    bool isInAir = false;
    bool isHooked = false;

	// Use this for initialization
	void Start()
	{
	}
    //Regular update
    void Update()
    {
        //Non-physics-based movement
        if (!isHooked)
        {
            float hSpeed = Input.GetAxis("Horizontal");
            float x = hSpeed * maxSpeed * Time.deltaTime;
            transform.position = transform.position + new Vector3(x, 0, 0);
        }

        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);

        AutoJump();

        MovementRestrictions();
    }

    void AutoJump()
    {
        //Check if player is standing on something
        if (!isOnGround)
        {
            isInAir = false;
        }
        //If player is standing on something, jump
        else if (isOnGround && GetComponent<Rigidbody2D>().velocity.y <= 0.0f && !isInAir)
        {
            if (!isHooked)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                isInAir = true;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpVelocity));
            }
        }
    }

    void MovementRestrictions()
    {
        //Ignore collision between player and platforms if y-velocity > 0
        if (GetComponent<Rigidbody2D>().velocity.y > 0 || isHooked)
        {
            Physics2D.IgnoreLayerCollision(8, 10, true);
        }
        else if (!isHooked)
        {
            Physics2D.IgnoreLayerCollision(8, 10, false);
        }

        //Move player to other side of screen if outside
        if (transform.position.x >= 14.15f)
        {
            transform.position = new Vector3(-14.0f, transform.position.y, 0);
        }
        else if (transform.position.x <= -14.15f)
        {
            transform.position = new Vector3(14.0f, transform.position.y, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        //if player falls outside screen, reload level
        if (aCollision.gameObject.tag == "CameraBorderDown")
        {
            SceneManager.LoadScene(0);
        }
    }

    void toggleHooked()
    {
        isHooked = !isHooked;
    }
}