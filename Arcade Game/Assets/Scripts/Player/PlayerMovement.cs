using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public Transform m_GroundCheck;
    public LayerMask m_WhatIsGround;
    public float m_MaxSpeed = 10.0f;
	public float m_JumpVelocity = 700.0f;
    public bool m_IsHooked = false;
    public float m_ScreenBorderX = 16.0f;

    bool m_IsOnGround = false;
    bool m_IsInAir = false;

    float m_CDTimer = 0.0f;
    float m_JumpCD = 0.25f;
    bool m_CanJump = true;

    Rigidbody2D m_Rigidbody;
    Collider2D m_Collider;

	// Use this for initialization
	void Start()
	{
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<CircleCollider2D>();
	}
    //Regular update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        //Non-physics-based movement
        if (!m_IsHooked)
        {
            float hSpeed = Input.GetAxis("Horizontal");
            float x = hSpeed * m_MaxSpeed * Time.deltaTime;
            transform.position = transform.position + new Vector3(x, 0, 0);
        }

        m_IsOnGround = Physics2D.OverlapCircle(m_GroundCheck.position, 0.1f, m_WhatIsGround);

        if (!m_IsHooked)
        {
            m_CDTimer += Time.deltaTime;
            if (m_CDTimer >= m_JumpCD)
            {
                m_CanJump = true;
            }
        }
        else
        {
            m_CDTimer = 0.0f;
            m_CanJump = false;
        }

        AutoJump();

        MovementRestrictions();
    }

    void AutoJump()
    {
        if (m_CanJump)
        {
            //Check if player is standing on something
            if (!m_IsOnGround)
            {
                m_IsInAir = false;
            }
            //If player is standing on something, jump
            else if (m_IsOnGround && m_Rigidbody.velocity.y <= 0.0f && !m_IsInAir)
            {
                if (!m_IsHooked)
                {
                    m_Rigidbody.velocity = Vector2.zero;
                    m_IsInAir = true;
                    m_Rigidbody.AddForce(new Vector2(0.0f, m_JumpVelocity), ForceMode2D.Impulse);
                }
            }
        }
    }

    void MovementRestrictions()
    {
        //Ignore collision between player and platforms if y-velocity > 0
        if (m_Rigidbody.velocity.y > 0 || m_IsHooked)
        {
            m_Collider.isTrigger = true;
        }
        else if (!m_IsHooked)
        {
            m_Collider.isTrigger = false;
        }

        //Move player to other side of screen if outside
        if (transform.position.x >= m_ScreenBorderX)
        {
            transform.position = new Vector3(-m_ScreenBorderX, transform.position.y, 0);
        }
        else if (transform.position.x <= -m_ScreenBorderX)
        {
            transform.position = new Vector3(m_ScreenBorderX, transform.position.y, 0);
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
        m_IsHooked = !m_IsHooked;
    }
}