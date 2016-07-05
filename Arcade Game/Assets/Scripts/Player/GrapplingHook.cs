using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrapplingHook : MonoBehaviour {

	public GameObject m_HookPrefab;
	public float m_ShotSpeed = 20.0f;
	public float m_MoveToHookSpeed = 400.0f;
    public float m_Cooldown = 1.0f;
    public float m_BreakDistance = 1.0f;
    public Vector3 m_ShootDirection;
    public Vector3 m_MoveDirection;
    public bool m_HasShot = false;

    float m_CDTimer = 0.0f;
	Vector3 m_RelativePlayerPos;
	bool m_WillMove = false;
    bool m_IsOnCD = false;
	GameObject m_Clone;
    GameObject m_Camera;
    Text m_CDText;
    Rigidbody2D m_RigidBody;

	// Use this for initialization
	void Start ()
	{
        m_CDTimer = m_Cooldown;
        m_CDText = GameObject.Find("HookCDText").GetComponent<Text>();
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera");
        m_RigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_RelativePlayerPos = Camera.main.WorldToScreenPoint(transform.position);

		m_ShootDirection = Input.mousePosition - m_RelativePlayerPos;

		if (Input.GetMouseButtonDown(0) && !m_HasShot && !m_IsOnCD)
		{
			Shoot();
		}

        if (m_IsOnCD)
        {
            m_CDTimer -= Time.deltaTime;
            if (m_CDTimer <= 0.0f)
            {
                m_CDTimer = m_Cooldown;
                ToggleCd();
            }
        }

		//Only check collision between player and hook when player moves towards it
		if (m_WillMove)
		{
            m_MoveDirection = m_Clone.gameObject.transform.position - transform.position;

            if (m_MoveDirection.magnitude <= m_BreakDistance)
            {
                isOnHook();
            }

            m_RigidBody.gravityScale = 0;
			MoveToHook();
		}
        else
        {
            m_RigidBody.gravityScale = 1;
        }

        TextUpdate();
    }

	void Shoot()
	{
		m_HasShot = true;
		m_Clone = (GameObject)Instantiate(m_HookPrefab, transform.position, Quaternion.identity);

		//Ceate unit vector for shot direction
		float x = m_ShootDirection.x;
		float y = m_ShootDirection.y;

		float squareX = x * x;
		float squareY = y * y;

		float added = squareX + squareY;

		added = Mathf.Sqrt(added);

		Vector3 unit = new Vector3((x / added), (y / added), 0);

		m_Clone.GetComponent<Rigidbody2D>().AddForce(unit * m_ShotSpeed);
	}

	void SetHookMovement()
	{
        m_RigidBody.velocity = Vector2.zero;
		m_WillMove = true;
        m_Camera.SendMessage("ToggleHookFollow", SendMessageOptions.RequireReceiver);
	}

	void MoveToHook()
	{
		Vector3 temp = m_MoveDirection;
		temp.Normalize();
		transform.position = new Vector2(transform.position.x + temp.x * m_MoveToHookSpeed * Time.deltaTime, 
			transform.position.y + temp.y * m_MoveToHookSpeed * Time.deltaTime);
	}

    void isOnHook()
    {
        Destroy(m_Clone);
        m_HasShot = false;
        m_WillMove = false;
        GetComponent<CircleCollider2D>().isTrigger = true;

        m_Camera.SendMessage("ToggleHookFollow", SendMessageOptions.RequireReceiver);

        this.gameObject.SendMessage("toggleHooked");

        if (m_RigidBody.velocity.y >= 0)
        {
            m_RigidBody.AddForce(new Vector2(0.0f, 12.0f), ForceMode2D.Impulse);
        }
        ToggleCd();
    }

    void ToggleHasShot()
    {
        m_HasShot = false;
    }

    void ToggleCd()
    {
        m_IsOnCD = !m_IsOnCD;
    }

    void TextUpdate()
    {
        if (m_IsOnCD)
        {
            m_CDText.text = m_CDTimer.ToString("F1");
            m_CDText.color = Color.red;
        }
        else
        {
            m_CDText.text = "Hook Ready";
            m_CDText.color = Color.green;
        }
    }
}
