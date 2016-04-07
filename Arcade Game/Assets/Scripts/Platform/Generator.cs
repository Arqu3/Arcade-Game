using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

    public GameObject platformPrefab;
    public float xIncrease = 3.0f;
    public float yIncrease = 5.0f;
    GameObject clone = null;
    List<Vector3> posList = new List<Vector3>();

	// Use this for initialization
	void Start ()
    {
        //Safety check for increase values
        if (xIncrease < 0)
        {
            xIncrease *= -1;
        }
        if (yIncrease < 0)
        {
            yIncrease *= -1;
        }

	    for (int i = 0; i < 100; i++)
        {
            //First platform is random x, set y
            if (i == 0)
            {
                posList.Add(new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, 0.0f));
            }
            //Start randoming based on last platform
            else
            {
                posList.Add(new Vector3(Random.Range(posList[i - 1].x - xIncrease, posList[i - 1].x + xIncrease), yIncrease * i, 0.0f));
                //Set position if x is lesser or greater than max values
                if (posList[i].x > 10.0f)
                {
                    posList[i] = new Vector3(10.0f, posList[i].y, 0.0f);
                }
                else if (posList[i].x < -10.0f)
                {
                    posList[i] = new Vector3(-10.0f, posList[i].y, 0.0f);
                }
            }

            //Reroll position if last was at edge
            if (i != 0)
            {
                if (posList[i - 1].x >= 10.0f)
                {
                    posList[i] = new Vector3(Random.Range(10.0f - xIncrease, 8.0f), posList[i].y, 0.0f);
                }
                else if (posList[i - 1].x <= -10.0f)
                {
                    posList[i] = new Vector3(Random.Range(-10.0f + xIncrease, -8.0f), posList[i].y, 0.0f);
                }
            }

            clone = (GameObject)Instantiate(platformPrefab, posList[i], Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
}
