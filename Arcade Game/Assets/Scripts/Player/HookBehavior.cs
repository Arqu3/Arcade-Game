using UnityEngine;
using System.Collections;

public class HookBehavior : MonoBehaviour {

    GameObject player;
    bool isAttached = false;
    float timer = 0.0f;
    //Time (in seconds) before hook is destroyed
    public float thresholdValue = 3.0f;

	// Use this for initialization
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Hook could not find player gameobject");
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        //Destroy hook if outside screen
        if (transform.position.x <= -14.5f || transform.position.x >= 14.5f)
        {
            player.gameObject.SendMessage("ToggleHasShot");
            Destroy(this.gameObject);
        }

        //If block isn't attached to anything and has lived for X amount of time
        if (!isAttached)
        {
            timer += Time.deltaTime;
            if (timer >= thresholdValue)
            {
                player.gameObject.SendMessage("ToggleHasShot");
                Destroy(this.gameObject);
            }
        }
        else
        {
            timer = 0.0f;
        }
	}

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        if (aCollision.gameObject.tag == "Block")
        {
            isAttached = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.SendMessage("SetHookMovement");
            player.SendMessage("toggleHooked");
        }

        if (aCollision.gameObject.tag == "Player")
        {
            player.SendMessage("toggleHooked");
        }
    }
}
