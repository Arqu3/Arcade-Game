using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 10.0f;
	public float jumpVelocity = 700.0f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    bool isOnGround = false;
    bool hasJumped = false;

	// Use this for initialization
	void Start()
	{
	}
	
	//Fixed update
	void FixedUpdate()
	{
        float hSpeed = Input.GetAxis("Horizontal");

        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.05f, whatIsGround);

        //Non-physics-based movement
        float x = hSpeed * maxSpeed * Time.deltaTime;
        //transform.position = new Vector2(transform.position.x + x, transform.position.y);

        transform.position = transform.position + new Vector3(x, 0, 0);

        //Reload level if outside screen
        if (transform.position.y < -10.0f)
        {
            SceneManager.LoadScene(0);
        }
	}

    //Regular update
    void Update()
    {
        if (isOnGround && GetComponent<Rigidbody2D>().velocity.y <= 0 && !hasJumped)
        {
            hasJumped = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpVelocity));
        }
        else if (!isOnGround)
        {
            hasJumped = false;
        }

        //Ignore collision between player and platforms if y-velocity > 0
        if (GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(8, 10, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(8, 10, false);
        }

        if (transform.position.x >= 14.15f)
        {
            transform.position = new Vector3(-14.0f, transform.position.y, 0);
        }
        else if(transform.position.x <= -14.15f)
        {
            transform.position = new Vector3(14.0f, transform.position.y, 0);
        }
    }
}
