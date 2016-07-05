using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Points : MonoBehaviour {

    //How long player has to travel to get a point
    public float distToScore = 100.0f;

    GameObject m_Player;
    Text m_ScoreText;

    float m_CurrentY = 0.0f;
    float m_MaxY = 0.0f;
    float m_Dist = 0.0f;

    int m_Score = 0;

	// Use this for initialization
	void Start()
    {
        m_Player = GameObject.Find("Player");
        m_ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update()
    {
        m_ScoreText.text = "Score: " + m_Score;

        m_CurrentY = m_Player.transform.position.y;

        //Only able to gain points when moving up or is hooked
        if (m_Player.GetComponent<Rigidbody2D>().velocity.y > 0 || m_Player.GetComponent<PlayerMovement>().m_IsHooked)
        {
            //If current y position is greater than current known max y, add distance and update max y
            if (m_CurrentY > m_MaxY)
            {
                m_MaxY = m_CurrentY;

                m_Dist++;
                //If distance is greather than threshold, add score and reset distance
                if (m_Dist >= distToScore)
                {
                    m_Score++;
                    m_Dist = 0.0f;
                }
            }
        }
	}

    void addScore(int num)
    {
        m_Score += num;
    }
}
