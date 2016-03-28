using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 10.0f;
	public float jumpVelocity = 700.0f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    bool isOnGround = false;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
        float hSpeed = Input.GetAxis("Horizontal");

        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

        //Physics-based movement
        //GetComponent<Rigidbody2D>().velocity = new Vector2(hSpeed * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        //Non-physics-based movement
        float x = hSpeed * maxSpeed;
        transform.position = new Vector2(transform.position.x + x, transform.position.y);

        //Reload level if outside screen
        if (transform.position.y < -10.0f)
        {
            SceneManager.LoadScene(0);
        }
	}

    void Update()
    {
        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpVelocity));
        }
    }
}
