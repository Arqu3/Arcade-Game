using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

	public GameObject hookPrefab;
	public float shotSpeed = 20.0f;
	public float moveToHookSpeed = 400.0f;
    public float cooldown = 1.0f;
    float coolDownTimer = 0.0f;

	Vector3 relativePlayerPos;
	public Vector3 shootDirection;
	public Vector3 moveDirection;
	public bool hasShot = false;
	bool willMove = false;
    bool isOnCd = false;
	GameObject clone;
    GameObject myCamera;

	// Use this for initialization
	void Start ()
	{
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update ()
	{
		relativePlayerPos = Camera.main.WorldToScreenPoint(transform.position);

		shootDirection = Input.mousePosition - relativePlayerPos;

		if (willMove)
		{
			moveDirection = clone.gameObject.transform.position - transform.position;
		}
		if (Input.GetMouseButtonDown(0) && !hasShot && !isOnCd)
		{
			Shoot();
		}

        if (isOnCd)
        {
            coolDownTimer += Time.deltaTime;
            if (coolDownTimer >= cooldown)
            {
                coolDownTimer = 0.0f;
                ToggleCd();
            }
        }

		//Only check collision between player and hook when player moves towards it
		if (willMove)
		{
			Physics2D.IgnoreLayerCollision(9, 10, false);
			GetComponent<Rigidbody2D>().gravityScale = 0;
			MoveToHook();
		}
		else
		{
			Physics2D.IgnoreLayerCollision(9, 10, true);
			GetComponent<Rigidbody2D>().gravityScale = 1;
		}
	}

	void Shoot()
	{
		hasShot = true;
		clone = (GameObject)Instantiate(hookPrefab, transform.position, Quaternion.identity);

		//Ceate unit vector for shot direction
		float x = shootDirection.x;
		float y = shootDirection.y;

		float squareX = x * x;
		float squareY = y * y;

		float added = squareX + squareY;

		added = Mathf.Sqrt(added);

		Vector3 unit = new Vector3((x / added), (y / added), 0);

		clone.GetComponent<Rigidbody2D>().AddForce(unit * shotSpeed);
	}

	void SetHookMovement()
	{
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		willMove = true;
        myCamera.SendMessage("ToggleHookFollow", SendMessageOptions.RequireReceiver);
	}

	void MoveToHook()
	{
		Vector3 temp = moveDirection;
		temp.Normalize();
		transform.position = new Vector2(transform.position.x + temp.x * moveToHookSpeed * Time.deltaTime, 
			transform.position.y + temp.y * moveToHookSpeed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D aCollision)
	{
		if (willMove)
		{
			if (aCollision.gameObject.tag == "Hook")
			{
				Destroy(clone);
				hasShot = false;
				willMove = false;

                myCamera.SendMessage("ToggleHookFollow", SendMessageOptions.RequireReceiver);

                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 450.0f));
                ToggleCd();
            }
        }
	}

    void ToggleHasShot()
    {
        hasShot = false;
    }

    void ToggleCd()
    {
        isOnCd = !isOnCd;
    }
}
