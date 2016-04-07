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

        if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            if (currentY > maxY)
            {
                maxY = currentY;

                dist++;
                if (dist >= distToScore)
                {
                    score++;
                    dist = 0.0f;
                }
            }
        }
	}
}
