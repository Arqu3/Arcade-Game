using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject movingPlatformPrefab;
    public float xIncrease = 3.0f;
    public float yIncrease = 5.0f;
    public int initialNumber = 10;

    GameObject clone;
    List<Vector3> posList = new List<Vector3>();

    int totalPlatformsCreated = 1;
    int curPlatNum = 1;

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
        posList.Add(new Vector3(Random.Range(-8.6f, 8.6f), 0.0f, 0.0f));
        clone = (GameObject)Instantiate(platformPrefab, posList[0], Quaternion.identity);

        GeneratePlatforms(initialNumber);
    }

    // Update is called once per frame
    void Update()
    {
        //Generate more platforms if current number is low
        if (curPlatNum <= 4)
        {
            GeneratePlatforms(5);
        }
	}

    void GeneratePlatforms(int num)
    {
        int temp = totalPlatformsCreated + num;

        for (int i = totalPlatformsCreated; i < temp; i++)
        {
            totalPlatformsCreated++;
            curPlatNum++;

            //Add base position
            posList.Add(new Vector3(Random.Range(posList[i - 1].x - xIncrease, posList[i - 1].x + xIncrease), yIncrease * i, 0.0f));

            //Set position if x is lesser or greater than max values
            if (posList[i].x > 8.6f)
            {
                posList[i] = new Vector3(8.6f, posList[i].y, 0.0f);
            }
            else if (posList[i].x < -8.6f)
            {
                posList[i] = new Vector3(-8.6f, posList[i].y, 0.0f);
            }


            //Reroll position if last was at edge
            if (posList[i - 1].x >= 8.6f)
            {
                posList[i] = new Vector3(Random.Range(8.6f - xIncrease, 8.0f), posList[i].y, 0.0f);
            }
            else if (posList[i - 1].x <= -8.6f)
            {
                posList[i] = new Vector3(Random.Range(-8.6f + xIncrease, -8.0f), posList[i].y, 0.0f);
            }

            //Start generating moving platforms if total created is 20 or above
            if (totalPlatformsCreated >= 20)
            {
                //Can return 1 and 2, not 0
                if (Random.Range(0, 2) == 1)
                {
                    clone = (GameObject)Instantiate(platformPrefab, posList[i], Quaternion.identity);
                }
                else
                {
                    clone = (GameObject)Instantiate(movingPlatformPrefab, posList[i], Quaternion.identity);
                }
            }
            else
            {
                clone = (GameObject)Instantiate(platformPrefab, posList[i], Quaternion.identity);
            }
        }
    }

    void RemoveLastPosition()
    {
        curPlatNum--;
    }
}
