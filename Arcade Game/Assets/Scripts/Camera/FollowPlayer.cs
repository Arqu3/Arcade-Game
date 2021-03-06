﻿using UnityEngine;
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
        OnlyYUp,
        Still
    }
    float highestY = 0.0f;
    float currentY = 0.0f;
    public Mode currentMode = Mode.Instant;
    bool hookFollow = false;

	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(0, 0, -10.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(currentMode)
        {
            //Follow player slowly
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

            //Follow player instantly
            case Mode.Instant:
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
                break;

            //Follow player only on Y axis
            case Mode.OnlyY:
                transform.position = new Vector3(0, player.transform.position.y, -10);
                break;

            //Follow player only on Y axis when moving up
            case Mode.OnlyYUp:
                currentY = player.transform.position.y;
                if (player.GetComponent<Rigidbody2D>().velocity.y > 0 || hookFollow)
                {
                    if (currentY > highestY)
                    {
                        highestY = currentY;
                        transform.position = new Vector3(0, player.transform.position.y, -10);
                    }
                }
                break;

            //Camera still
            case Mode.Still:
                transform.position = new Vector3(0, 0, -10);
                break;
        }
	}

    //Help function to let camera follow player when using grappling hook
    void ToggleHookFollow()
    {
        hookFollow = !hookFollow;
    }
}
