using UnityEngine;
using System.Collections;

public class PlatformBehavior : MonoBehaviour
{
    Generator m_Generator;

	// Use this for initialization
	void Start ()
    {
        m_Generator = GameObject.Find("SceneHandler").GetComponent<Generator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        //Remove platform if outside screen
        if (aCollision.gameObject.tag == "MainCamera")
        {
            m_Generator.SendMessage("RemoveLastPosition");
            Destroy(this.gameObject);
        }
    }
}
