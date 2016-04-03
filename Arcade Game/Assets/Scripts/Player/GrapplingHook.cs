using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

    public GameObject hookPrefab;
    public float shotSpeed = 20.0f;
    public float moveToHookSpeed = 400.0f;

    Vector3 relativePlayerPos;
    public Vector3 direction;
    public bool hasShot = false;
    bool willMove = false;
    GameObject clone;

    // Use this for initialization
    void Start () {
	}

    void FixedUpdate()
    {
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
	
	// Update is called once per frame
	void Update () {
        relativePlayerPos = Camera.main.WorldToScreenPoint(transform.position);

        direction = Input.mousePosition - relativePlayerPos;

        if (Input.GetMouseButtonDown(0) && !hasShot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        hasShot = true;
        clone = (GameObject)Instantiate(hookPrefab, transform.position, Quaternion.identity);

        //Ceate unit vector for shot direction
        float x = direction.x;
        float y = direction.y;

        float squareX = x * x;
        float squareY = y * y;

        float added = squareX + squareY;

        Mathf.Sqrt(added);

        Vector3 unit = new Vector3((x / added), (y / added), 0);

        clone.GetComponent<Rigidbody2D>().AddForce(unit * shotSpeed);
    }

    void SetHookMovement()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        willMove = true;
    }

    void MoveToHook()
    {
        float x = direction.x;
        float y = direction.y;

        float squareX = x * x;
        float squareY = y * y;

        float added = squareX + squareY;

        Mathf.Sqrt(added);

        Vector3 unit = new Vector3((x / added), (y / added), 0);

        Vector3 param = unit * moveToHookSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + param.x, transform.position.y + param.y);
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
            }
        }
    }
}
