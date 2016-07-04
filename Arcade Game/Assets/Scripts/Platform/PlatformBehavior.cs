using UnityEngine;
using System.Collections;

public class PlatformBehavior : MonoBehaviour
{
    Generator generator;

	// Use this for initialization
	void Start ()
    {
        generator = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<Generator>();
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
            generator.SendMessage("RemoveLastPosition");
            Destroy(this.gameObject);
        }
    }
}
