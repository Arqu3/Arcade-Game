using UnityEngine;
using System.Collections;

public class HookBehavior : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Hook could not find player");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        if (aCollision.gameObject.tag == "Block")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.SendMessage("SetHookMovement");
        }
    }
}
