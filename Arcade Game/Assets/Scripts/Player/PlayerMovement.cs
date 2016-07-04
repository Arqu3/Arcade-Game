using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 10.0f;
	public float jumpVelocity = 700.0f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    bool isOnGround = false;
    bool isInAir = false;
    public bool isHooked = false;

    float cdTimer = 0.0f;
    float cd = 0.25f;
    bool canJump = true;

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

        if (!isHooked)
        {
            cdTimer += Time.deltaTime;
            if (cdTimer >= cd)
            {
                canJump = true;
            }
        }
        else
        {
            cdTimer = 0.0f;
            canJump = false;
        }

        AutoJump();

        MovementRestrictions();
    }

    void AutoJump()
    {
        if (canJump)
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
    }

    void MovementRestrictions()
    {
        //Ignore collision between player and platforms if y-velocity > 0
        if (GetComponent<Rigidbody2D>().velocity.y > 0 || isHooked)
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
            //Physics2D.IgnoreLayerCollision(8, 10, true);
        }
        else if (!isHooked)
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
            //Physics2D.IgnoreLayerCollision(8, 10, false);
        }

        //Move player to other side of screen if outside
        if (transform.position.x >= 16.0f)
        {
            transform.position = new Vector3(-16.0f, transform.position.y, 0);
        }
        else if (transform.position.x <= -16.0f)
        {
            transform.position = new Vector3(16.0f, transform.position.y, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        //if player falls outside screen, reload level
        if (aCollision.gameObject.tag == "MainCamera")
        {
            SceneManager.LoadScene(0);
        }
    }

    void toggleHooked()
    {
        isHooked = !isHooked;
    }
}