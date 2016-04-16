using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

    public GameObject platformPrefab;
    public GameObject player;
    public float xIncrease = 3.0f;
    public float yIncrease = 5.0f;
    GameObject clone;
    List<Vector3> posList = new List<Vector3>();

    int totalPlatformsCreated = 1;

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

        //Add first base platform
        posList.Add(new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, 0.0f));
        clone = (GameObject)Instantiate(platformPrefab, posList[0], Quaternion.identity);

        GenerateBasePlatforms(10);
    }

    // Update is called once per frame
    void Update()
    {

	}

    void GenerateBasePlatforms(int num)
    {
        int temp = totalPlatformsCreated + num;

        for (int i = totalPlatformsCreated; i < temp; i++)
        {
            totalPlatformsCreated++;

            //Add base position
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


            //Reroll position if last was at edge
            if (posList[i - 1].x >= 10.0f)
            {
                posList[i] = new Vector3(Random.Range(10.0f - xIncrease, 8.0f), posList[i].y, 0.0f);
            }
            else if (posList[i - 1].x <= -10.0f)
            {
                posList[i] = new Vector3(Random.Range(-10.0f + xIncrease, -8.0f), posList[i].y, 0.0f);
            }

            clone = (GameObject)Instantiate(platformPrefab, posList[i], Quaternion.identity);
        }
    }

    void RemoveLastPosition()
    {
        //Removes last item in postlist
        totalPlatformsCreated--;
        posList.RemoveAt(totalPlatformsCreated);
    }
}
