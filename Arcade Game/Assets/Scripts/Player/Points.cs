using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Points : MonoBehaviour {

    public GameObject player;
    public Text scoreText;

    float currentY = 0.0f;
    float maxY = 0.0f;
    float dist;

    //How long player has to travel to get a point
    public float distToScore = 100.0f;

    int score = 0;
	// Use this for initialization
	void Start()
    {
	}
	
	// Update is called once per frame
	void Update()
    {
        scoreText.text = "Score: " + score;

        currentY = player.transform.position.y;

        //Only able to gain points when moving up or is hooked
        if (player.GetComponent<Rigidbody2D>().velocity.y > 0 || player.GetComponent<PlayerMovement>().isHooked)
        {
            //If current y position is greater than current known max y, add distance and update max y
            if (currentY > maxY)
            {
                maxY = currentY;

                dist++;
                //If distance is greather than threshold, add score and reset distance
                if (dist >= distToScore)
                {
                    score++;
                    dist = 0.0f;
                }
            }
        }
	}

    void addScore(int num)
    {
        score += num;
    }
}
