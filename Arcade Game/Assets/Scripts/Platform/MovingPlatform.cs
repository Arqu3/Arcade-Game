using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    public float maxX = 4.0f;
    public float speed = 2.0f;
    public bool isOffset = true;

    float minX;
    Vector3 curDir = Vector3.zero;
    Vector3 initialPosition;

    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    public Direction currentDirection = Direction.Right;

	// Use this for initialization
	void Start ()
    {
        minX = maxX * -1;
        initialPosition = transform.position;

        //Set position if maxX is outside screen
        if (initialPosition.x + maxX >= 8.6f)
        {
            transform.position = new Vector3(8.6f - maxX, transform.position.y);
            initialPosition = transform.position;
        }
        else if (initialPosition.x + minX <= -8.6f)
        {
            transform.position = new Vector3(-8.6f - minX, transform.position.y);
            initialPosition = transform.position;
        }

        //Random starting direction
        if (Random.Range(0, 2) == 1)
        {
            currentDirection = Direction.Right;
        }
        else
        {
            currentDirection = Direction.Left;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //If offset position is active
        if (isOffset)
        {
            if (transform.position.x > maxX + initialPosition.x)
            {
                SetDirection(Direction.Left);
            }
            else if (transform.position.x < minX + initialPosition.x)
            {
                SetDirection(Direction.Right);
            }
        }
        else
        {
            if (transform.position.x > maxX)
            {
                SetDirection(Direction.Left);
            }
            else if (transform.position.x < minX)
            {
                SetDirection(Direction.Right);
            }
        }

        //What happens during different states
        switch(currentDirection)
        {
            case Direction.Right:
                curDir = new Vector2(1, 0);
                transform.position += curDir * speed * Time.deltaTime;
                break;

            case Direction.Left:
                curDir = new Vector2(-1, 0);
                transform.position += curDir * speed * Time.deltaTime;
                break;

            case Direction.Up:
                curDir = new Vector2(0, 1);
                transform.position += curDir * speed * Time.deltaTime;
                break;

            case Direction.Down:
                curDir = new Vector2(0, -1);
                transform.position += curDir * speed * Time.deltaTime;
                break;
        }
	}

    void SetDirection(Direction dir)
    {
        currentDirection = dir;
    }

    void SetSpeed(float temp)
    {
        speed = temp;
    }
}
