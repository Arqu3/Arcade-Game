using UnityEngine;
using System.Collections;

public class HookBehavior : MonoBehaviour {

    //Time (in seconds) before hook is destroyed
    public float m_LifeTime = 3.0f;

    GameObject m_Player;
    bool m_IsAttached = false;
    float m_Timer = 0.0f;

	// Use this for initialization
	void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
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
            m_Player.gameObject.SendMessage("ToggleHasShot");
            Destroy(this.gameObject);
        }

        //If block isn't attached to anything and has lived for X amount of time
        if (!m_IsAttached)
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_LifeTime)
            {
                m_Player.gameObject.SendMessage("ToggleHasShot");
                Destroy(this.gameObject);
            }
        }
        else
        {
            m_Timer = 0.0f;
        }
	}

    void OnTriggerEnter2D(Collider2D aCollision)
    {
        if (aCollision.gameObject.tag == "Player")
        {
            transform.parent = null;
        }

        if (aCollision.gameObject.tag == "Platform")
        {
            m_IsAttached = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            m_Player.SendMessage("SetHookMovement");
            m_Player.SendMessage("toggleHooked");

            transform.parent = aCollision.transform;
        }
    }
}
