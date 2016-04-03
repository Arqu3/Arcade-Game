using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    public float dampTime = 0.15f;
    Vector3 vel = Vector3.zero;

    public enum Mode
    {
        Damp,
        Instant,
        OnlyY,
        Still
    }
    public Mode currentMode = Mode.Instant;

	// Use this for initialization
	void Start ()
    {
        transform.position = player.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(currentMode)
        {
            case Mode.Damp:
                if (player)
                {
                    Vector3 point = Camera.main.WorldToViewportPoint(player.transform.position);
                    Vector3 delta = player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                    Vector3 destination = transform.position + delta;
                    destination.z = -10;
                    transform.position = Vector3.SmoothDamp(transform.position, destination, ref vel, dampTime);
                }
                break;

            case Mode.Instant:
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
                break;

            case Mode.OnlyY:
                transform.position = new Vector3(0, player.transform.position.y, -10);
                break;

            case Mode.Still:
                transform.position = new Vector3(0, 0, -10);
                break;
        }
	}
}
