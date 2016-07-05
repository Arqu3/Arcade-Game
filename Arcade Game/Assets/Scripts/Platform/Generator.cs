using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
    public GameObject m_PlatformPrefab;
    public GameObject m_MovingPlatformPrefab;
    public float m_XIncrease = 3.0f;
    public float m_YIncrease = 5.0f;
    public int m_InitNumber = 10;

    GameObject m_Clone;
    List<Vector3> m_Poslist = new List<Vector3>();

    int m_TotalPlatNum = 1;
    int m_CurPlatNum = 1;

	// Use this for initialization
	void Start ()
    {
        //Safety check for increase values
        if (m_XIncrease < 0)
        {
            m_XIncrease *= -1;
        }
        if (m_YIncrease < 0)
        {
            m_YIncrease *= -1;
        }

        //Add first base platform
        m_Poslist.Add(new Vector3(Random.Range(-8.6f, 8.6f), 0.0f, 0.0f));
        m_Clone = (GameObject)Instantiate(m_PlatformPrefab, m_Poslist[0], Quaternion.identity);

        GeneratePlatforms(m_InitNumber);
    }

    // Update is called once per frame
    void Update()
    {
        //Generate more platforms if current number is low
        if (m_CurPlatNum <= 4)
        {
            GeneratePlatforms(5);
        }
	}

    void GeneratePlatforms(int num)
    {
        int temp = m_TotalPlatNum + num;

        for (int i = m_TotalPlatNum; i < temp; i++)
        {
            m_TotalPlatNum++;
            m_CurPlatNum++;

            //Add base position
            m_Poslist.Add(new Vector3(Random.Range(m_Poslist[i - 1].x - m_XIncrease, m_Poslist[i - 1].x + m_XIncrease), m_Poslist[i - 1].y + m_YIncrease, 0.0f));

            //Set position if x is lesser or greater than max values
            if (m_Poslist[i].x > 8.6f)
            {
                m_Poslist[i] = new Vector3(8.6f, m_Poslist[i].y, 0.0f);
            }
            else if (m_Poslist[i].x < -8.6f)
            {
                m_Poslist[i] = new Vector3(-8.6f, m_Poslist[i].y, 0.0f);
            }


            //Reroll position if last was at edge
            if (m_Poslist[i - 1].x >= 8.6f)
            {
                m_Poslist[i] = new Vector3(Random.Range(8.6f - m_XIncrease, 8.0f), m_Poslist[i].y, 0.0f);
            }
            else if (m_Poslist[i - 1].x <= -8.6f)
            {
                m_Poslist[i] = new Vector3(Random.Range(-8.6f + m_XIncrease, -8.0f), m_Poslist[i].y, 0.0f);
            }

            //Start generating moving platforms if total created is 20 or above
            if (m_TotalPlatNum >= 20)
            {
                //Can return 1 and 2, not 0
                if (Random.Range(0, 2) == 1)
                {
                    m_Clone = (GameObject)Instantiate(m_PlatformPrefab, m_Poslist[i], Quaternion.identity);
                }
                else
                {
                    m_Clone = (GameObject)Instantiate(m_MovingPlatformPrefab, m_Poslist[i], Quaternion.identity);
                }
            }
            else
            {
                m_Clone = (GameObject)Instantiate(m_PlatformPrefab, m_Poslist[i], Quaternion.identity);
            }
        }
    }

    void RemoveLastPosition()
    {
        m_CurPlatNum--;
    }
}
