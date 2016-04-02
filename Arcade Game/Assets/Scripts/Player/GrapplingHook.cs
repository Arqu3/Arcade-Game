using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

    public GameObject hookPrefab;
    public float speed = 20.0f;
    public float totalVelocity;

    Vector3 relativePlayerPos;
    public Vector3 direction;
    public bool hasShot = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        relativePlayerPos = Camera.main.WorldToScreenPoint(transform.position);

        direction = Input.mousePosition - relativePlayerPos;

        if (Input.GetMouseButtonDown(0) && !hasShot)
        {
            Shoot();
        }

        totalVelocity = hookPrefab.GetComponent<Rigidbody2D>().velocity.magnitude;

    }

    void Shoot()
    {
        hasShot = true;
        GameObject clone;
        clone = (GameObject)Instantiate(hookPrefab, transform.position, Quaternion.identity);

        float x = direction.x;
        float y = direction.y;

        float squareX = x * x;
        float squareY = y * y;

        float added = squareX + squareY;

        Mathf.Sqrt(added);

        Vector3 unit = new Vector3((x / added), (y / added), 0);

        clone.GetComponent<Rigidbody2D>().AddForce(unit * speed);
    }
}
